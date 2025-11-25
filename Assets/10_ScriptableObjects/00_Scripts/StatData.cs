using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entity의 스탯과 값을 저장하기 위한 SO
/// </summary>
public enum StatType
{
    Health,
    Stamina,
    AttackDistance,
    DetectDistance,
}

[CreateAssetMenu(fileName = "New PlayerStatData", menuName = "Stats/Player Stats")]
public class PlayerStatData : ScriptableObject
{
    [field: SerializeField] public string PlayerName { get; private set; }
    [field: SerializeField] public List<StatEntry> Stats { get; private set; }
}

[CreateAssetMenu(fileName = "New MonsterStatData", menuName = "Stats/Monster Stats")]
public class MonsterStatData : ScriptableObject
{
    [field: SerializeField] public string MonsterName { get; private set; }
    [field: SerializeField] public List<StatEntry> Stats { get; private set; }
}

[System.Serializable]
public class StatEntry
{
    [field: SerializeField] public StatType StatType { get; private set; }
    [field: SerializeField] public float BaseValue { get; private set; }
}