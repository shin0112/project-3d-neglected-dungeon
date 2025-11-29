using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장비 장착을 관리하는 슬롯 컨테이너
/// </summary>
public class EquipmentController : MonoBehaviour
{
    // 장비 슬롯
    private readonly Dictionary<EquipmentType, EquipmentSlot> _equipmentSlots = new();
    public EquipmentSlot this[EquipmentType type] => _equipmentSlots[type];

    // 총 장비로 얻은 스텟 값
    private readonly Dictionary<StatType, float> _equipmentValues = new();

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

        EquipDefault();
    }

    /// <summary>
    /// 기존에 장비가 있으면 해당 장비 착용하기
    /// </summary>
    private void EquipDefault()
    {
        foreach (EquipmentSlot slot in _equipmentSlots.Values)
        {
            if (slot.Data != null)
            {
                slot.Equip(slot.Data);

                foreach (var equipment in slot.Data.Equipments)
                {
                    _equipmentValues[equipment.Stat] += equipment.Value;
                }

                foreach (var keyValue in _equipmentValues)
                {
                    OnEquipmentSlotChanged?.Invoke(keyValue.Key, keyValue.Value);
                }
            }
        }
    }

    private void OnDestroy()
    {
        OnEquipmentSlotChanged = null;
    }
    #endregion

    #region [public] 초기화 - View 용
    public void InitProfileView()
    {
        foreach (var value in _equipmentSlots.Values)
        {
            EquipmentItemData[] equipments = value.Data.Equipments;
            foreach (var equipment in equipments)
            {
                OnEquipmentSlotChanged?.Invoke(equipment.Stat, equipment.Value);
            }
        }
    }

    public void InitEquipmentItemView()
    {
        foreach (var Value in _equipmentSlots.Values)
        {
            Value.InitEquipmentItemView();
        }
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

        if (prev == data)
        {
            Logger.Log("동일한 아이템");
            return;
        }

        if (prev != null)
        {
            foreach (EquipmentItemData equipment in prev.Equipments)    // 이전 장비 값 제거
            {
                _equipmentValues[equipment.Stat] -= equipment.Value;
            }
        }

        _equipmentSlots[type].Equip(data);

        foreach (EquipmentItemData equipment in data.Equipments)        // 현재 장비 값 추가
        {
            _equipmentValues[equipment.Stat] += equipment.Value;
            OnEquipmentSlotChanged?.Invoke(equipment.Stat, _equipmentValues[equipment.Stat]);
        }
    }
    #endregion
}
