/// <summary>
/// 인벤토리 관리를 위한 매니저
/// 장비/소모품 보유 상태 및 변경 이벤트 관리
/// </summary>
public partial class Managers
{
    public class InventoryManager
    {
        internal InventoryManager() { }

        private PlayerCondition _condition;
        private EquipmentController _equipment;
        public EquipmentController Equipment => _equipment;

        #region 초기화
        public void Initialize(PlayerCondition condition, EquipmentController equipment)
        {
            _condition = condition;
            _equipment = equipment;
        }
        #endregion

        /// <summary>
        /// [public] 플레이어의 장비가 변경되었을 경우 view에 표시하기 위한 이벤트 구독
        /// todo: 초기화 시점 확인 후 연동 시도
        /// </summary>
        public void InitEquipView()
        {
            _equipment.OnEquipmentSlotChanged += _condition.OnEquipmentChanged;
        }

        public void Equip(EquipmentType type, ItemData data)
        {
            _equipment.Equip(type, data);
        }
    }
}
