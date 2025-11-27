using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerCondition
{
    #region 필드
    // 레벨
    public int Level { get; private set; }
    public float CurrentExp { get; private set; }

    // 스탯
    public Dictionary<StatType, float> StatDict { get; private set; }

    public float CurrentHealth => StatDict[StatType.Health];
    public float CurrentStamina => StatDict[StatType.Stamina];
    public float TotalAttack { get; private set; }
    public float TotalDefense { get; private set; }

    // 이벤트
    public event Action<int> OnLevelChanged;
    public event Action<float> OnExpChanged;
    public event Action<float> OnStaminaChanged;
    #endregion

    #region 초기화
    public PlayerCondition(PlayerStatData data)
    {
        ConvertStatListToDict(data.Stats);

        // todo: 데이터 연동
        Level = 1;
        CurrentExp = 0f;

        // 공격력 / 방어력 초기화
        UpdateTotalAttackPower();
        UpdateTotalDefensePower();
    }

    /// <summary>
    /// 스텟 타입과 값을 딕셔너리로 관리하기 위해 초기화
    /// 반드시 사용하는 값일 경우 기본값 추가
    /// </summary>
    private void ConvertStatListToDict(List<StatEntry> stats)
    {
        StatDict = new();

        foreach (StatEntry entry in stats)
        {
            if (entry.BaseValue < 0)
            {
                throw new StatNegativeValueException(entry.StatType, entry.BaseValue);
            }

            StatDict[entry.StatType] = entry.BaseValue;
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
        if (!StatDict.ContainsKey(type))
        {
            throw new StatMissingKeyException(type);
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
        OnStaminaChanged?.Invoke(CurrentStamina);
    }
    #endregion

    #region Destroy
    /// <summary>
    /// Player가 파괴될 때 수행
    /// </summary>
    public void OnDestroy()
    {
        OnLevelChanged = null;
        OnExpChanged = null;
        OnStaminaChanged = null;
    }
    #endregion

    #region [public] 스텟 사용
    /// <summary>
    /// 스테미나를 사용 가능한지 확인하고, 사용할 경우 OnStaminaChanged 수행
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool TryUseStamina(float amount)
    {
        if (CurrentStamina < amount)
        {
            Logger.Log("스테미나 부족");
            return false;
        }

        StatDict[StatType.Stamina] -= amount;
        OnStaminaChanged?.Invoke(CurrentStamina);

        return true;
    }
    #endregion

    #region 스텟 관리
    /// <summary>
    /// 총합 공격력을 구하는 로직
    /// 장비를 장착 / 해제할 경우 호출
    /// </summary>
    private void UpdateTotalAttackPower()
    {
        TotalAttack = StatDict[StatType.Attack] + GetEquipmentAttackPower();
    }

    /// <summary>
    /// 총합 방어력을 구하는 로직
    /// 장비를 장착 / 해제할 경우 호출
    /// </summary>
    private void UpdateTotalDefensePower()
    {
        TotalDefense = StatDict[StatType.Defense] + GetEquipmentDefensePower();
    }
    #endregion

    #region 장비 계산 // todo: 장비 확장 시 로직 추가
    private float GetEquipmentAttackPower()
    {
        return 0f;
    }

    private float GetEquipmentDefensePower()
    {
        return 0f;
    }
    #endregion
}
