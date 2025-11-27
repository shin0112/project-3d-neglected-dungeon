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

    #region 스테미나 사용
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
}
