public partial class Managers
{
    public class InventoryManager
    {
        internal InventoryManager() { }

        private PlayerCondition _condition;
        private EquipmentController _equipment;

        #region 초기화
        public void Initialize(PlayerCondition condition, EquipmentController equipment)
        {
            _condition = condition;
            _equipment = equipment;

            _equipment.OnEquipmentSlotChanged += _condition.OnEquipmentChanged;
        }
        #endregion

        public void Equip(EquipmentType type, ItemData data)
        {
            _equipment.Equip(type, data);
        }
    }
}
