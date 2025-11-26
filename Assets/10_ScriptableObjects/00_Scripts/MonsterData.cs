using UnityEngine;

[CreateAssetMenu(fileName = "new Monster Data", menuName = "Scriptable Object/Entity/Monster")]
public class MonsterData : ScriptableObject
{
    [field: SerializeField] public MonsterType Type { get; private set; }
    [field: SerializeField] public MonsterVariant Variant { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public MonsterStatData Stat { get; private set; }
}
