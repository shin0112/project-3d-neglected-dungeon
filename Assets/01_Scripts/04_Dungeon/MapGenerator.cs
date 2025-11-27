using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지 정보를 받고 맵을 자동으로 생성해주는 클래스
/// </summary>
[System.Serializable]
public class MapGenerator
{
    // Cooridor
    private float _corridorTileSize = 1f;
    private Dictionary<CorridorType, GameObject[]> _corridorsDict = new();

    // Room Generation Simulation
    private RoomInfo[] _roomInfos;
    private float[] _rotatioins = { 0, 90, 180, 270 };

    public MapGenerator(CorridorSetData[] corridors)
    {
        foreach (CorridorSetData corridor in corridors)
        {
            _corridorsDict[corridor.CorridorType] = corridor.CorridorPrefabs;
        }
    }

    #region [public] 방 생성하기
    /// <summary>
    /// [public] 스테이지 정보를 받고 방 랜덤 생성 후, 첫 번째 방을 스테이지의 루트로 설정
    /// </summary>
    /// <param name="stage"></param>
    /// <returns></returns>
    public GameObject Generate(Transform root, StageData stage)
    {
        int count = stage.RoomCount;
        _roomInfos = new RoomInfo[count];

        bool success = false;
        int attempts = 30;

        while (!success && attempts-- > 0)
        {
            for (int i = 0; i < count; i++)
            {
                int random = Random.Range(0, stage.MapPrefabs.Length);
                _roomInfos[i] = CreateRoomInfo(stage.MapPrefabs[random], random);
            }

            success = TrySimulateRooms(stage);  // 시뮬레이션
        }

        if (!success)
        {
            Logger.Log("던전 방 생성 실패");
            return null;
        }

        return CreateRooms(root, stage);
    }
    #endregion

    #region 방 랜덤 배치 시뮬레이션
    /// <summary>
    /// 시뮬레이션에 사용할 스트럭트
    /// </summary>
    public struct RoomInfo
    {
        public int OriginalPrefabIndex;

        public Vector3 Position;                // 월드 위치
        public float RotationY;                 // 0/90/180/270

        public Bounds LocalBounds;              // prefab 기준 bounds

        public Vector3[] DoorLocalPositions;    // 문 로컬 위치 (배열)
        public Vector3[] DoorLocalForwards;     // 문 로컬 forward (배열)

        public bool[] DoorConnected;            // 문 사용되었는지 확인
        public int ChosenDoor;                  // 선택된 문 인덱스
    }

    /// <summary>
    /// room 위치, 회전, 연결 정보 생성
    /// </summary>
    /// <param name="stage"></param>
    /// <returns></returns>
    private bool TrySimulateRooms(StageData stage)
    {
        for (int i = 0; i < stage.RoomCount; i++)
        {
            if (i == 0)
            {
                PlaceFirstRoom(ref _roomInfos[i]);
                continue;
            }

            if (!TryPlaceNextRoom(i))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// prefab을 바탕으로 RoomInfo 생성하기
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    private RoomInfo CreateRoomInfo(GameObject prefab, int index)
    {
        RoomInfo info = new()
        {
            OriginalPrefabIndex = index,
            LocalBounds = CalcLocalBounds(prefab)
        };

        // Door
        var connectors = prefab.GetComponentInChildren<RoomConnectors>();
        int count = connectors.DoorPoints.Length;

        info.DoorLocalPositions = new Vector3[count];
        info.DoorLocalForwards = new Vector3[count];
        info.DoorConnected = new bool[count];

        for (int i = 0; i < count; i++)
        {
            Transform door = connectors.DoorPoints[i];

            info.DoorLocalPositions[i] = door.localPosition;
            Vector3 localForward = door.localRotation * Vector3.forward;
            localForward.y = 0;
            info.DoorLocalForwards[i] = localForward.normalized;
        }

        return info;
    }

    /// <summary>
    /// 첫 번째 방 배치
    /// </summary>
    /// <param name="info"></param>
    private void PlaceFirstRoom(ref RoomInfo info)
    {
        info.Position = Vector3.zero;
        info.RotationY = _rotatioins.Random();
        info.ChosenDoor = 0;
    }

    /// <summary>
    /// index 번째 방 배치
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private bool TryPlaceNextRoom(int index)
    {
        RoomInfo prev = _roomInfos[index - 1];
        RoomInfo next = _roomInfos[index];

        int attempts = 20;

        while (attempts-- > 0)
        {
            // 1) (index - 1) 번째 방에서 연결 가능한 문 탐색
            List<int> availableDoors = new();
            for (int i = 0; i < prev.DoorConnected.Length; i++)
            {
                if (!prev.DoorConnected[i])
                {
                    availableDoors.Add(i);
                }
            }

            if (availableDoors.Count == 0) { return false; }

            int prevDoorIndex = availableDoors[Random.Range(0, availableDoors.Count)];

            Vector3 prevDoorWPos = DoorWorldPos(prev, prevDoorIndex);       // 이전 방 문 위치 (World)
            Vector3 prevDoorWDir = DoorWorldFoward(prev, prevDoorIndex);    // 이전 방 문 방향 (World)

            // 2) 다음 방에서 사용할 문 선택
            int nextDoorIndex = Random.Range(0, next.DoorLocalPositions.Length);
            Vector3 nextDoorLDir = next.DoorLocalForwards[nextDoorIndex];   // 다음 방 문 방향 (Local)


            // 3) 두 문이 마주보도록 회전 계산
            Vector3 targetDir = -prevDoorWDir;                              // 타겟 방향 -> 이전 문 방향 반대

            // - 목표 좌표와 로컬 좌표를 비교해서 회전해야 하는 각도 구하기
            float targetY = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
            float localY = Mathf.Atan2(nextDoorLDir.x, nextDoorLDir.z) * Mathf.Rad2Deg;
            float rotY = targetY - localY;
            float snappedRotY = Mathf.Round(rotY / 90f) * 90f;              // 90도 단위로 회전하기 위한 스냅

            Quaternion roomRot = Quaternion.Euler(0, snappedRotY, 0);       // 방 회전 목표 각도

            // 4) 다음 방의 문 World 좌표 계산
            float corridorLength = Random.Range(12f, 30f);
            Vector3 nextDoorWPos =
                prevDoorWPos + prevDoorWDir * corridorLength;               // 다음 방 문 위치 (World)

            Vector3 nextDoorLPos = next.DoorLocalPositions[nextDoorIndex];  // 다음 방 문 위치 (Local)
            Vector3 roomPos = nextDoorWPos - (roomRot * nextDoorLPos);
            // - 통로 좌표의 y 위치(-2f) -> 밀리지 않게 room y 좌표 고정
            roomPos.y = 0f;

            // 5) 겹치지 않을 경우 사용
            if (!IsOverlap(roomPos, roomRot, next.LocalBounds, index))
            {
                next.Position = roomPos;
                next.RotationY = snappedRotY;
                next.ChosenDoor = nextDoorIndex;

                prev.DoorConnected[prevDoorIndex] = true;
                next.DoorConnected[nextDoorIndex] = true;

                // struct update
                _roomInfos[index - 1] = prev;
                _roomInfos[index] = next;

                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// index 번째 room 충돌 여부 확인
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    /// <param name="local"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    private bool IsOverlap(Vector3 pos, Quaternion rot, Bounds local, int index)
    {
        Bounds bounds = local;
        bounds.center = pos;

        for (int i = 0; i < index; i++)
        {
            Bounds b = _roomInfos[i].LocalBounds;
            b.center = _roomInfos[i].Position;
            if (bounds.Intersects(b))
            {
                return true;
            }
        }

        return false;
    }
    #endregion

    #region 방 배치 (실제)
    /// <summary>
    /// 시뮬레이션으로 만든 스테이지 정보 사용해서 맵 실제 배치
    /// </summary>
    /// <param name="stage"></param>
    /// <returns></returns>
    private GameObject CreateRooms(Transform root, StageData stage)
    {
        GameObject roomRoot = null;

        for (int i = 0; i < _roomInfos.Length; i++)
        {
            RoomInfo info = _roomInfos[i];
            GameObject prefab = stage.MapPrefabs[info.OriginalPrefabIndex];

            GameObject room = Object.Instantiate(prefab, root);
            room.transform.position = info.Position;
            room.transform.rotation = Quaternion.Euler(0, info.RotationY, 0);

            if (i == 0)
            {
                roomRoot = room;
            }
        }

        for (int i = 0; i < _roomInfos.Length - 1; i++)
        {
            var fromInfo = _roomInfos[i];
            var toInfo = _roomInfos[i + 1];

            Vector3 from = DoorWorldPos(fromInfo, fromInfo.ChosenDoor);
            Vector3 to = DoorWorldPos(toInfo, toInfo.ChosenDoor);

            CreateCorridor(root, from, to);
        }

        return roomRoot;
    }
    #endregion

    #region 통로 연결
    /// <summary>
    /// From에서 To로 corridor 연결하기
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    private void CreateCorridor(Transform root, Vector3 from, Vector3 to)
    {
        Vector3 dir = (to - from).normalized;
        float dist = Vector3.Distance(from, to);

        int count = Mathf.CeilToInt(dist / _corridorTileSize);

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = from + dir * (_corridorTileSize * i);

            Object.Instantiate(
                GetCorridorPrefab(CorridorType.Straight),
                pos,
                Quaternion.LookRotation(dir),
                root);
        }
    }

    /// <summary>
    /// 통로 타입에 해당하는 랜덤 통로 오브젝트 얻기
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private GameObject GetCorridorPrefab(CorridorType type)
    {
        if (!_corridorsDict.TryGetValue(type, out var corridors))
        {
            Logger.Log($"{type} corridor 없음");
        }

        return corridors.Random();
    }
    #endregion

    #region Helper

    /// <summary>
    /// prefab의 로컬 Bounds 값 반환
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    private Bounds CalcLocalBounds(GameObject prefab)
    {
        Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
        Bounds bounds = renderers[0].bounds;

        foreach (var r in renderers)
        {
            bounds.Encapsulate(r.bounds);
        }

        bounds.center -= prefab.transform.position;     // local 기준 변경
        return bounds;
    }

    private Vector3 DoorWorldPos(RoomInfo info, int index)
    {
        return info.Position +
            Quaternion.Euler(0, info.RotationY, 0) * info.DoorLocalPositions[index];
    }

    /// <summary>
    /// Room의 index 번째 문이 바라보는 좌표 구하기
    /// </summary>
    /// <param name="info"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    private Vector3 DoorWorldFoward(RoomInfo info, int index)
    {
        return Quaternion.Euler(0, info.RotationY, 0) * info.DoorLocalForwards[index];
    }
    #endregion
}
