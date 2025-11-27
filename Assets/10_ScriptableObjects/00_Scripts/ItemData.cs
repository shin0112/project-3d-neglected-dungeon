using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/Item/ItemData")]
public class ItemData : ScriptableObject
{
    [field: Header("Info")]
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public ItemType Type { get; private set; }
    [field: SerializeField] public ItemClass ItemClass { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    [field: Header("Equipment")]
    [field: SerializeField] public int EquipmentLevel { get; private set; }
    [field: SerializeField] public EquipmentType EquipmentType { get; private set; }
    [field: SerializeField] public EquipmentItemData[] Equipments { get; private set; }

    [field: Header("Consumable")]
    [field: SerializeField] public ConsumableItemData[] Consumables { get; private set; }
}

[System.Serializable]
public class EquipmentItemData
{
    [field: SerializeField] public StatType Stat { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
}

[System.Serializable]
public class ConsumableItemData
{
    [field: SerializeField] public ConsumableType Type { get; private set; }
    [field: SerializeField] public StatType Stat { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
}