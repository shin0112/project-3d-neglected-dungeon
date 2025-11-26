using UnityEngine;

/// <summary>
/// Entity의 스탯과 값을 저장하기 위한 클래스
/// </summary>
[System.Serializable]
public class StatEntry
{
    [field: SerializeField] public StatType StatType { get; private set; }
    [field: SerializeField] public float BaseValue { get; private set; }
}