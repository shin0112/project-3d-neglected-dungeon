using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    private readonly Dictionary<EquipmentType, EquipmentSlot> _equipmentSlots = new();
    private readonly Dictionary<StatType, float> _equipmentValues = new();

    public EquipmentSlot this[EquipmentType type] => _equipmentSlots[type];
    public float this[StatType type] => _equipmentValues[type];

    // 이벤트
    public event Action<StatType, float> OnEquipmentSlotChanged;

    #region 초기화 & 파괴
    private void Awake()
    {
        var equipments = GetComponentsInChildren<EquipmentSlot>(true);

        foreach (EquipmentSlot equipment in equipments)
        {
            _equipmentSlots[equipment.Type] = equipment;
        }

        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            _equipmentValues[type] = 0f;
        }
    }

    private void Start()
    {
        foreach (EquipmentSlot slot in _equipmentSlots.Values)
        {
            if (slot.Data != null)
            {
                Equip(slot.Type, slot.Data);
            }
        }
    }

    private void OnDestroy()
    {
        OnEquipmentSlotChanged = null;
    }
    #endregion

    #region [public] 장비 착용
    /// <summary>
    /// [pupblic] 장비 아이템 장착
    /// 이전 아이템 데이터 제거, 장착 아이템 데이터 갱신
    /// </summary>
    /// <param name="type"></param>
    /// <param name="data"></param>
    public void Equip(EquipmentType type, ItemData data)
    {
        ItemData prev = _equipmentSlots[type].Data;
        if (prev != null)
        {
            foreach (EquipmentItemData equipment in prev.Equipments)    // 이전 장비 값 제거
            {
                _equipmentValues[equipment.Stat] -= equipment.Value;
            }
        }

        _equipmentSlots[type].Equipment(data);

        foreach (EquipmentItemData equipment in data.Equipments)        // 현재 장비 값 추가
        {
            _equipmentValues[equipment.Stat] += equipment.Value;
            OnEquipmentSlotChanged?.Invoke(equipment.Stat, _equipmentValues[equipment.Stat]);
        }
    }
    #endregion
}
