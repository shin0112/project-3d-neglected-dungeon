/// <summary>
/// 장비 슬롯에 대한 정보 연결하는 Presenter
/// </summary>
public class EquipmentItemPresenter
{
    IEquipmentItemView _view;

    public EquipmentItemPresenter(IEquipmentItemView view)
    {
        _view = view;

        EquipmentController equipments = Managers.Instance.Inventory.Equipment;

        // todo: 추후 슬롯 확장 시 전부 반영
        equipments[EquipmentType.Weapon].OnClickEquipmentSlot += OnClickEquipmentSlot;
        equipments[EquipmentType.Armor].OnClickEquipmentSlot += OnClickEquipmentSlot;

        equipments.InitEquipmentItemView();
    }

    public void OnClickEquipmentSlot(ItemData data)
    {
        _view.UpdateEquipmentItemText(data);
    }
}
