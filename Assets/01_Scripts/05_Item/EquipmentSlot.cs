using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    [field: SerializeField] public EquipmentType Type { get; private set; }

    #region 버튼
    protected override void OnClickButton()
    {
    }
    #endregion

    #region [public] 아이템 장착
    public void Equipment(ItemData data)
    {
        SetSlot(data, data.EquipmentLevel.ToString());
    }
    #endregion
}
