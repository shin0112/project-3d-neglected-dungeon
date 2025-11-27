using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Grid 기반 절차적 던전 생성기
/// </summary>
[System.Serializable]
public class MapGenerator
{
    // Cooridor
    private float _corridorTileSize = 1f;
    private Dictionary<CorridorType, GameObject[]> _corridorsDict = new();

    // Room Generation Simulation
    private HashSet<Vector2Int> _occupied = new();           // 그리드
    private List<RoomInfo> _rooms = new();
    private int[] _rotatioins = { 0, 90, 180, 270 };

    public MapGenerator(CorridorSetData[] corridors)
    {
        foreach (CorridorSetData corridor in corridors)
        {
            _corridorsDict[corridor.CorridorType] = corridor.CorridorPrefabs;
        }
    }

    #region [public] 방 생성하기
    /// <summary>
    /// [public] 스테이지 정보를 받고 방 랜덤 생성 후, 루트 아래에 맵 생성
    /// </summary>
    /// <param name="root"></param>
    /// <param name="stage"></param>
    /// <returns></returns>
    public GameObject Generate(Transform root, StageData stage)
    {
        _occupied.Clear();
        _rooms.Clear();

        bool success = TrySimulate(stage);  // 시뮬레이션

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
    /// 시뮬레이션에 사용할 클래스
    /// </summary>
    private class RoomInfo
    {
        public RoomData Data;
        public Vector2Int GridPos;
    }

    /// <summary>
    /// room 위치, 회전, 연결 정보 생성
    /// </summary>
    /// <param name="stage"></param>
    /// <returns></returns>
    private bool TrySimulate(StageData stage)
    {
        PlaceFirstRoom(stage.RoomPrefabs);

        for (int i = 1; i < stage.RoomCount; i++)
        {
            if (!TryPlaceNextRoom(stage.RoomPrefabs))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 첫 번째 방 랜덤 선택 후 (0, 0)에 배치
    /// </summary>
    /// <param name="rooms"></param>
    private void PlaceFirstRoom(RoomData[] rooms)
    {
        RoomData rand = rooms.Random();

        RoomInfo info = new RoomInfo()
        {
            Data = rand,
            GridPos = Vector2Int.zero
        };

        _rooms.Add(info);
        MarkFootprint(info);
    }

    /// <summary>
    /// 직전 방에서 4방향 검사 후 겹치지 않는 곳에 배치
    /// </summary>
    /// <param name="rooms"></param>
    /// <returns></returns>
    private bool TryPlaceNextRoom(RoomData[] rooms)
    {
        RoomData rand = rooms.Random();
        RoomInfo prev = _rooms[_rooms.Count - 1];

        Vector2Int[] dirs = {
            new(1,0),
            new (-1,0),
            new (0,1),
            new (0,-1),
        };

        dirs = dirs.Shuffle();

        foreach (Vector2Int dir in dirs)
        {
            Vector2Int offset = dir * (prev.Data.Size.x + 5);
            Vector2Int newPos = prev.GridPos + offset;

            if (!IsFootprintOccupied(newPos, rand.Size))
            {
                RoomInfo info = new()
                {
                    Data = rand,
                    GridPos = newPos
                };

                _rooms.Add(info);
                MarkFootprint(info);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 위치와 크기로 _occupied에 겹치는지 확인
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    private bool IsFootprintOccupied(Vector2Int pos, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int cell = pos + new Vector2Int(x, y);
                if (_occupied.Contains(cell)) { return true; }
            }
        }

        return false;
    }

    /// <summary>
    /// 방이 위치할 grid 좌표 _occupied에 추가
    /// </summary>
    /// <param name="info"></param>
    private void MarkFootprint(RoomInfo info)
    {
        for (int x = 0; x < info.Data.Size.x; x++)
        {
            for (int y = 0; y < info.Data.Size.y; y++)
            {
                _occupied.Add(info.GridPos + new Vector2Int(x, y));
            }
        }
    }
    #endregion

    #region 방 배치 (실제)
    /// <summary>
    /// 시뮬레이션으로 만든 스테이지 정보 사용해서 맵 실제 배치
    /// </summary>
    /// <param name="root"></param>
    /// <param name="stage"></param>
    /// <returns></returns>
    private GameObject CreateRooms(Transform root, StageData stage)
    {
        GameObject roomRoot = null;

        for (int i = 0; i < _rooms.Count; i++)
        {
            RoomInfo info = _rooms[i];
            Vector3 pos = GridToWorld(info.GridPos);

            GameObject room = Object.Instantiate(
                info.Data.Prefab,
                pos,
                Quaternion.identity,
                root);

            if (i == 0)
            {
                roomRoot = room;
            }

            if (i > 0)
            {
                Vector3 a = GridToWorld(_rooms[i - 1].GridPos);
                Vector3 b = GridToWorld(_rooms[i].GridPos);

                CreateCorridor(root, a, b);
                PlaceDoor(root, a, b);
                PlaceDoor(root, b, a);
            }
        }

        return roomRoot;
    }

    /// <summary>
    /// 문 설치
    /// </summary>
    /// <param name="root"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    private void PlaceDoor(Transform root, Vector3 from, Vector3 to)
    {
        // to do: 문 프리팹 받아서 설치하기
    }
    #endregion

    #region 통로 연결
    /// <summary>
    /// root 아래에 From에서 To로 corridor 연결하기
    /// </summary>
    /// <param name="root"></param>
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
            pos.y -= 2f;

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
    /// Gird의 좌표를 World 좌표로 변환
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    Vector3 GridToWorld(Vector2Int cell)
    {
        return new Vector3(cell.x * Define.TileSize, 0, cell.y * Define.TileSize);
    }
    #endregion
}
