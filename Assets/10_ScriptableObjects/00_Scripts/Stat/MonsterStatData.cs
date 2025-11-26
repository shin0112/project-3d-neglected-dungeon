using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MonsterStatData", menuName = "Scriptable Object/Stats/Monster Stats")]
public class MonsterStatData : ScriptableObject
{
    [field: SerializeField] public string MonsterName { get; private set; }
    [field: SerializeField] public List<StatEntry> Stats { get; private set; }
}