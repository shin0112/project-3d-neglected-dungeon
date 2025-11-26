
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerStatData", menuName = "Scriptable Object/Stats/Player Stats")]
public class PlayerStatData : ScriptableObject
{
    [field: SerializeField] public string PlayerName { get; private set; }
    [field: SerializeField] public List<StatEntry> Stats { get; private set; }
}
