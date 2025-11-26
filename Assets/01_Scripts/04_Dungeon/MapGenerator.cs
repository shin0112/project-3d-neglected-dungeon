using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapGenerator
{
    [Header("Corridor")]
    [SerializeField] private GameObject _corridorStraightPrefab;
    [SerializeField] private GameObject _corridorCornerPrefab;
    [SerializeField] private float _corridorTileSize = 1f;

    public MapGenerator(CorridorSetData corridorData)
    {

    }

    /// <summary>
    /// 스테이지 정보를 받고 방 랜덤 생성 후, 첫 번째 방을 스테이지의 루트로 설정
    /// </summary>
    /// <param name="stage"></param>
    /// <returns></returns>
    public GameObject CreateRandomMap(StageData stage)
    {
        // 1) 방 랜덤 생성
        List<GameObject> rooms = new();

        for (int i = 0; i < stage.RoomCount; i++)
        {
            GameObject room = Object.Instantiate(stage.MapPrefabs.Random());
            // todo: 랜덤 배치
            room.transform.position = new Vector3(0, 0, i * 50f);
            rooms.Add(room);
        }

        // 2) 방 연결
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            RoomConnectors a = rooms[i].GetComponent<RoomConnectors>();
            RoomConnectors b = rooms[i + 1].GetComponent<RoomConnectors>();

            Transform doorA = a.DoorPoints.Random();
            Transform doorB = b.DoorPoints.Random();

            CreateCorridor(doorA, doorB);
        }

        return rooms[0];
    }

    /// <summary>
    /// From에서 To로 corridor 연결하기
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    private void CreateCorridor(Transform from, Transform to)
    {
        Vector3 direction = (to.position - from.position).normalized;       // 방향 계산
        float distance = Vector3.Distance(from.position, to.position);      // 거리 계산

        int count = Mathf.CeilToInt(distance / _corridorTileSize);          // 타일 개수 계산

        for (int i = 0; i < count; i++)
        {
            // todo: 일직선이 아닌 코너로 배치
            Vector3 position = from.position + direction * (i * _corridorTileSize);
            Object.Instantiate(_corridorStraightPrefab, position, Quaternion.LookRotation(direction));
        }
    }
}
