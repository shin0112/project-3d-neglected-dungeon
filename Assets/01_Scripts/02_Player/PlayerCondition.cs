using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 상태를 관리하기 위한 컨테이너
/// </summary>
[System.Serializable]
public class PlayerCondition
{
    #region 필드
    public string Name { get; private set; }

    // 레벨
    public int Level { get; private set; }
    public float CurrentExp { get; private set; }
    public float RequiredExp { get; private set; }

    // 스탯
    private Dictionary<StatType, Stat> _statDict;
    public Stat this[StatType type] => _statDict[type];

    public float CurrentHealth => _statDict[StatType.Health].Value;
    public float CurrentStamina => _statDict[StatType.Stamina].Value;

    // 이벤트
    public event Action<int> OnLevelChanged;
    public event Action<float> OnExpChanged;
    #endregion

    #region 초기화 & 파괴
    public PlayerCondition(PlayerStatData data)
    {
        Name = data.PlayerName;

        ConvertStatListToDict(data.Stats);

        // todo: 데이터 연동
        Level = 1;
        CurrentExp = 0f;
        RequiredExp = GetRequiredExp(Level);
    }

    /// <summary>
    /// 스텟 타입과 값을 딕셔너리로 관리하기 위해 초기화
    /// 반드시 사용하는 값일 경우 기본값 추가
    /// </summary>
    /// <param name="stats"></param>
    /// <exception cref="StatNegativeValueException"></exception>
    private void ConvertStatListToDict(List<StatEntry> stats)
    {
        _statDict = new();

        foreach (StatEntry entry in stats)
        {
            if (entry.BaseValue < 0)
            {
                throw new StatNegativeValueException(entry.StatType, entry.BaseValue);
            }

            _statDict[entry.StatType] = new Stat(entry.BaseValue, entry.StatType);
        }

        // 기본값 초기화
        CheckRequiredStat(StatType.Health);
        CheckRequiredStat(StatType.Stamina);
        CheckRequiredStat(StatType.Attack);
        CheckRequiredStat(StatType.Defense);
    }

    /// <summary>
    /// 필수 스탯 값이 데이터에 존재하는지 확인
    /// </summary>
    /// <param name="type"></param>
    /// <exception cref="StatMissingKeyException"></exception>
    private void CheckRequiredStat(StatType type)
    {
        if (!_statDict.ContainsKey(type))
        {
            throw new StatMissingKeyException(type);
        }
    }

    /// <summary>
    /// Player가 파괴될 때 수행
    /// </summary>
    public void OnDestroy()
    {
        // 이벤트 초기화
        OnLevelChanged = null;
        OnExpChanged = null;

        foreach (Stat stat in _statDict.Values)
        {
            stat.OnDestroy();
        }
    }
    #endregion

    #region [public] 초기화 - View 용
    /// <summary>
    /// Header View에서 초기화할 때 사용
    /// </summary>
    public void InitHeaderView()
    {
        OnLevelChanged?.Invoke(Level);
        OnExpChanged?.Invoke(CurrentExp);
    }

    /// <summary>
    /// Stat View에서 초기화할 때 사용
    /// </summary>
    public void InitStatView()
    {
        _statDict[StatType.Stamina].SyncView();
    }

    /// <summary>
    /// Profile View에서 초기화할 때 사용
    /// </summary>
    public void InitProfileView()
    {
        _statDict[StatType.Attack].SyncView();
        _statDict[StatType.Defense].SyncView();
        _statDict[StatType.Health].SyncView();
    }
    #endregion

    #region [public] 스텟 관리
    /// <summary>
    /// StatType의 값을 amount 만큼 사용하기
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool TryUse(StatType type, float amount) => _statDict[type].TryUse(amount);

    public void AddExp(int exp)
    {
        CurrentExp += exp;
        while (CurrentExp > RequiredExp)
        {
            LevelUp();
        }
        OnExpChanged?.Invoke(CurrentExp / RequiredExp);
    }

    /// <summary>
    /// [public] 장비 장착 시 호출
    /// 장비 아이템에서 변경하는 StatType의 value만큼을 적용
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public void OnEquipmentChanged(StatType type, float value)
    {
        _statDict[type].UpdateEquipmentValue(value);
    }
    #endregion

    #region 레벨 관리
    /// <summary>
    /// 레벨업 경험치 받아오기
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    private float GetRequiredExp(int level)
    {
        float baseExp = 100f;
        float quad = 20f * level * level;
        float expo = Mathf.Pow(1.05f, level);

        return (baseExp + quad) * expo;
    }

    private void LevelUp()
    {
        CurrentExp -= RequiredExp;
        Level++;
        OnLevelChanged?.Invoke(Level);
        RequiredExp = GetRequiredExp(Level);
    }
    #endregion
}

public class Stat
{
    // 필드
    public float BaseValue { get; private set; }
    public float EquipmentValue { get; private set; }
    public float BuffValue { get; private set; }

    public float TotalValue => BaseValue + EquipmentValue + BuffValue;
    public float Value { get; private set; }

    private StatType _type;

    // 이벤트
    public event Action<float> OnValueChanged;
    public event Action<float> OnMaxValueChanged;

    public Stat(float value, StatType type)
    {
        BaseValue = value;
        Value = value;
        _type = type;
    }

    /// <summary>
    /// [public] View 초기화용 함수
    /// </summary>
    public void SyncView()
    {
        OnValueChanged?.Invoke(Value);
        OnMaxValueChanged?.Invoke(TotalValue);
    }

    /// <summary>
    /// [public] 초기화 함수
    /// </summary>
    public void OnDestroy()
    {
        OnValueChanged = null;
        OnMaxValueChanged = null;
    }

    /// <summary>
    /// [public] 특정 값 만큼 스텟 값 사용 시도. 변경 다음 OnValueChanged 호출
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool TryUse(float amount)
    {
        if (Value < amount)
        {
            Logger.Log($"{_type} 부족");
            return false;
        }

        Value -= amount;
        OnValueChanged?.Invoke(Value);
        return true;
    }

    /// <summary>
    /// [public] 특정 값 만큼 획득
    /// </summary>
    /// <param name="amount"></param>
    public void Add(float amount)
    {
        Value = Mathf.Min(Value + amount, TotalValue);
        OnValueChanged?.Invoke(Value);
    }

    /// <summary>
    /// 장비 아이템 값 갱신
    /// </summary>
    /// <param name="value"></param>
    public void UpdateEquipmentValue(float value)
    {
        EquipmentValue = value;

        Value = Mathf.Min(Value, TotalValue);

        OnValueChanged?.Invoke(Value);
        OnMaxValueChanged?.Invoke(TotalValue);
    }
}