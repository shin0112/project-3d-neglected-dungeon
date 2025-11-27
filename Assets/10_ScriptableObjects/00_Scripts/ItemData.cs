using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/Item/ItemData")]
public class ItemData : ScriptableObject
{
    [field: Header("Info")]
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public ItemType Type { get; private set; }
    [field: SerializeField] public ItemClass ItemClass { get; private set; }
    [field: SerializeField] public Image Icon { get; private set; }

    [field: Header("Equipment")]
    [field: SerializeField] public EquipmentType EquipmentType { get; private set; }
    [field: SerializeField] public EquipmentStats[] EquipmentStats { get; private set; }
    [field: SerializeField] public int[] EquipmentStatsValues { get; private set; }

    [field: Header("Consumable")]
    [field: SerializeField] public ConsumableType[] ConsumableTypes { get; private set; }
    [field: SerializeField] public ConsumableStats[] ConsumableStats { get; private set; }
    [field: SerializeField] public int[] ConsumableStatsValues { get; private set; }
}
