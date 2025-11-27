using System;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    [field: SerializeField] public EquipmentType Type { get; private set; }

    public event Action<ItemData> OnClickEquipmentSlot;

    #region 버튼
    protected override void OnClickButton()
    {
        OnClickEquipmentSlot?.Invoke(data);
    }
    #endregion

    #region [public] 아이템 장착
    public void Equip(ItemData data)
    {
        SetSlot(data, data.EquipmentLevel.ToString());
    }
    #endregion
}
