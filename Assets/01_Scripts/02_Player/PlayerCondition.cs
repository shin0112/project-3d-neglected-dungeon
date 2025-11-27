using System.Collections.Generic;

public class PlayerCondition
{
    public Dictionary<StatType, float> StatDict { get; private set; }

    public float Hp => StatDict[StatType.Health];
    public float Stmina => StatDict[StatType.Stamina];

    public PlayerCondition(PlayerStatData data)
    {
        ConvertStatListToDict(data.Stats);
    }

    /// <summary>
    /// 스텟 타입과 값을 딕셔너리로 관리하기 위해 초기화
    /// Health와 Stamina는 무조건 존재
    /// </summary>
    private void ConvertStatListToDict(List<StatEntry> stats)
    {
        StatDict = new();

        foreach (StatEntry statEntry in stats)
        {
            StatDict[statEntry.StatType] = statEntry.BaseValue;
        }

        if (!StatDict.ContainsKey(StatType.Health))
        {
            StatDict[StatType.Health] = Define.DefaultHealth;
        }

        if (!StatDict.ContainsKey(StatType.Stamina))
        {
            StatDict[StatType.Stamina] = Define.DefaultStamina;
        }
    }
}
