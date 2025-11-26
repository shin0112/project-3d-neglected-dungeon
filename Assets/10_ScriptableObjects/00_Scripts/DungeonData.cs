using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 던전에 포함되는 스테이지와 그 스테이지에 소환될 Entity와 관련된 정보
/// </summary
[CreateAssetMenu(fileName = "new Dungeon Data", menuName = "Scriptable Object/Dungeon/Dungeon Data")]
public class DungeonData : ScriptableObject
{
    public int dungeonKey;
    public string dungeonName;
    public List<StageData> stages;
}

[System.Serializable]
public class StageData
{
    [field: Header("Map")]
    [field: SerializeField] public GameObject[] MapPrefabs { get; private set; }

    [field: Header("Spawn Settings")]
    [field: SerializeField] public MonsterSpawnData[] MonsterPool { get; private set; }
    [field: SerializeField] public int MaxEnemyCount { get; private set; }

    [field: Header("Boss")]
    [field: SerializeField] public MonsterData BossData { get; private set; }
    [field: SerializeField] public bool IsFinalStage { get; private set; }

    [field: Header("Optional")]
    [field: SerializeField] public ObstacleData[] ObstacleDatas { get; private set; }
}

[System.Serializable]
public class MonsterSpawnData
{
    [field: SerializeField] public MonsterData MonsterData { get; private set; }
    [field: SerializeField] public int SpawnCount { get; private set; }
}