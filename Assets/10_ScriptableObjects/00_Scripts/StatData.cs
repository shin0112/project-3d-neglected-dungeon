using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Stamina,
    AttackDistance,
    DetectDistance,
}

[CreateAssetMenu(fileName = "New StatData", menuName = "Stats/Entity Stats")]
public class StatData : ScriptableObject
{
    [field: SerializeField] public string PlayerName { get; private set; }
    [field: SerializeField] public List<StatEntry> Stats { get; private set; }
}

[System.Serializable]
public class StatEntry
{
    [field: SerializeField] public StatType StatType { get; private set; }
    [field: SerializeField] public float BaseValue { get; private set; }
}