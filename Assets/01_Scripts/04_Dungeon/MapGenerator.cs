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
    public GameObject Generate(StageData stage)
    {
        // 시뮬레이션
        bool success = false;
        int safety = 30;

        while (!success && safety-- > 0)
        {
            success = TrySimulateRooms(stage);
        }

        if (!success)
        {
            Logger.Log("던전 방 생성 실패");
            return null;
        }

        return InstantiateAllRooms(stage);
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

        public int ChosenDoor;                  // 선택된 문 인덱스
    }

    private bool TrySimulateRooms(StageData stage)
    {
        int count = stage.RoomCount;
        _roomInfos = new RoomInfo[count];
        GameObject[] prefabs = stage.MapPrefabs;

        for (int i = 0; i < count; i++)
        {
            _roomInfos[i] = CreateRoomInfo(prefabs.Random());

            if (i == 0)
            {
                PlaceFirstRoom(ref _roomInfos[i]);
                continue;
            }

            if (!TryPlaceRoom(i))
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
    /// <returns></returns>
    private RoomInfo CreateRoomInfo(GameObject prefab)
    {
        RoomInfo info = new();

        info.OriginalPrefabIndex = prefab.transform.GetSiblingIndex();

        // Bounds
        Bounds bounds = CalcLocalBounds(prefab);
        info.LocalBounds = bounds;

        // Door
        var connectors = prefab.GetComponentInChildren<RoomConnectors>();
        int count = connectors.DoorPoints.Length;

        info.DoorLocalPositions = new Vector3[count];
        info.DoorLocalForwards = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            Transform door = connectors.DoorPoints[i];

            Vector3 localPos = door.position - prefab.transform.position;
            info.DoorLocalPositions[i] = localPos;
            info.DoorLocalForwards[i] = door.forward;
        }

        return info;
    }

    /// <summary>
    /// 첫 번째 방 배치
    /// </summary>
    /// <param name="roomInfo"></param>
    private void PlaceFirstRoom(ref RoomInfo roomInfo)
    {
        roomInfo.Position = RandomInsideCircle(80f);
        roomInfo.RotationY = _rotatioins.Random();
        roomInfo.ChosenDoor = 0;
    }

    /// <summary>
    /// index 번째 방 배치
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private bool TryPlaceRoom(int index)
    {
        int attempts = 20;
        RoomInfo roomInfo = _roomInfos[index];

        while (attempts-- > 0)
        {
            Vector3 trialPos = RandomInsideCircle(80f);

            // 회전 테스트
            float bestDistance = float.MaxValue;
            float bestRotation = 0;
            int bestDoor = -1;

            // 회전해서 가장 좋은 위치 탐색
            for (int r = 0; r < _rotatioins.Length; r++)
            {
                float rotation = _rotatioins[r];

                // 회전 적용
                roomInfo.RotationY = rotation;
                roomInfo.Position = trialPos;

                // 겹칠 경우 스킵
                if (IsOverlap(index))
                {
                    continue;
                }

                // 이전 방의 Door와 거리 비교
                RoomInfo prev = _roomInfos[index - 1];
                for (int i = 0; i < roomInfo.DoorLocalPositions.Length; i++)
                {
                    Vector3 doorA = DoorWorldPos(ref roomInfo, i);
                    Vector3 doorB = DoorWorldPos(ref prev, prev.ChosenDoor);

                    float distance = Vector3.Distance(doorA, doorB);

                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestRotation = _rotatioins[r];
                        bestDoor = i;
                    }
                }
            }

            // door 번호가 지정될 경우 적용
            if (bestDoor >= 0)
            {
                roomInfo.RotationY = bestRotation;
                roomInfo.ChosenDoor = bestDoor;
                _roomInfos[index] = roomInfo;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// index 번째 room 충돌 여부 확인
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private bool IsOverlap(int index)
    {
        Bounds curBounds = WorldBounds(ref _roomInfos[index]);

        for (int i = 0; i < index; i++)
        {
            if (curBounds.Intersects(WorldBounds(ref _roomInfos[i])))
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
    private GameObject InstantiateAllRooms(StageData stage)
    {
        GameObject root = null;

        for (int i = 0; i < _roomInfos.Length; i++)
        {
            RoomInfo info = _roomInfos[i];
            GameObject prefab = stage.MapPrefabs[info.OriginalPrefabIndex];

            GameObject room = Object.Instantiate(prefab);
            room.transform.position = info.Position;
            room.transform.rotation = Quaternion.Euler(0, info.RotationY, 0);

            if (i == 0)
            {
                root = room;
            }
        }

        for (int i = 0; i < _roomInfos.Length - 1; i++)
        {
            var fromInfo = _roomInfos[i];
            var toInfo = _roomInfos[i + 1];

            Vector3 from = DoorWorldPos(ref fromInfo, fromInfo.ChosenDoor);
            Vector3 to = DoorWorldPos(ref toInfo, toInfo.ChosenDoor);

            CreateCorridor(from, to);
        }

        return root;
    }
    #endregion

    #region 통로 연결
    /// <summary>
    /// From에서 To로 corridor 연결하기
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    private void CreateCorridor(Vector3 from, Vector3 to)
    {
        // L자 방향 결정 (랜덤)
        bool horizontalFirst = Random.value > 0.5f;

        if (horizontalFirst)
        {
            // X → Z
            Vector3 mid = new Vector3(to.x, from.y, from.z);
            SpawnCorridorStraight(from, mid);
            SpawnCorridorStraight(mid, to);
        }
        else
        {
            // Z → X
            Vector3 mid = new Vector3(from.x, from.y, to.z);
            SpawnCorridorStraight(from, mid);
            SpawnCorridorStraight(mid, to);
        }
    }

    private void SpawnCorridorStraight(Vector3 start, Vector3 end)
    {
        Vector3 dir = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        int count = Mathf.CeilToInt(distance / _corridorTileSize);

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = start + dir * (i * _corridorTileSize);

            Object.Instantiate(
                GetCorridorPrefab(CorridorType.Straight),
                pos,
                Quaternion.LookRotation(dir)
            );
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

    private Bounds WorldBounds(ref RoomInfo roomInfo)
    {
        Bounds bounds = roomInfo.LocalBounds;
        bounds.center = roomInfo.Position;
        return bounds;
    }

    private Vector3 DoorWorldPos(ref RoomInfo roomInfo, int doorIndex)
    {
        return roomInfo.Position +
            Quaternion.Euler(0, roomInfo.RotationY, 0) * roomInfo.DoorLocalPositions[doorIndex];
    }

    /// <summary>
    /// 반지름 Radius인 원 안에서 랜덤으로 좌표 가져오기
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    private Vector3 RandomInsideCircle(float radius)
    {
        float angle = Random.Range(0, Mathf.PI * 2f);
        float r = Random.Range(0f, radius);
        return new Vector3(Mathf.Cos(angle) * r, 0, Mathf.Sin(angle) * r);
    }
    #endregion
}
