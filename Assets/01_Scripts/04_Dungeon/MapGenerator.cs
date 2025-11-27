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
        // 시뮬레이션
        bool success = false;
        int attempts = 30;

        while (!success && attempts-- > 0)
        {
            success = TrySimulateRooms(stage);
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
        int count = stage.RoomCount;
        _roomInfos = new RoomInfo[count];

        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, stage.MapPrefabs.Length);
            _roomInfos[i] = CreateRoomInfo(stage.MapPrefabs[random], random);

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
            List<int> availableDoors = new();
            for (int i = 0; i < prev.DoorConnected.Length; i++)
            {
                if (!prev.DoorConnected[i])
                {
                    availableDoors.Add(i);
                }
            }

            if (availableDoors.Count == 0)
            {
                return false;
            }

            int doorIndex = availableDoors[Random.Range(0, availableDoors.Count)];

            Vector3 doorPos = DoorWorldPos(prev, doorIndex);
            Vector3 doorDir = DoorWorldFoward(prev, doorIndex);

            float corridorLength = Random.Range(12f, 30f);
            Vector3 corridorEnd = doorPos + doorDir * corridorLength;

            foreach (float rotation in _rotatioins)
            {
                Quaternion roomRot = Quaternion.Euler(0, rotation, 0);
                Vector3 roomPos = corridorEnd;
                roomPos.y = 0f;

                if (!IsOverlap(roomPos, roomRot, next.LocalBounds, index))
                {
                    next.Position = roomPos;
                    next.RotationY = rotation;
                    next.ChosenDoor = 0;

                    prev.DoorConnected[doorIndex] = true;

                    _roomInfos[index - 1] = prev;
                    _roomInfos[index] = next;

                    return true;
                }
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

    private Vector3 DoorWorldFoward(RoomInfo info, int index)
    {
        return Quaternion.Euler(0, info.RotationY, 0) * info.DoorLocalForwards[index];
    }
    #endregion
}
