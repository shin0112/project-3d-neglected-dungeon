using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour, IAttackable
{
    [field: Header("Data")]
    [field: SerializeField] public MonsterStatData Stat { get; private set; }
    public Dictionary<StatType, float> StatDict { get; private set; }
    private float _hp;
    public bool IsAlive => _hp > 0;

    [field: Header("Animation")]
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public MonsterAnimationData AnimationData { get; private set; }
    protected MonsterStateMachine monsterStateMachine;

    #region 초기화
    private void Reset()
    {
        Animator = transform.FindChild<Animator>("Skeleton_Warrior");
    }

    protected void Awake()
    {
        // Component
        GetComponents();

        // Data
        ConvertStatListToDict();

        // Animation
        AnimationData.Initialize();
        this.monsterStateMachine = new(this);
        this.monsterStateMachine.ChangeState(monsterStateMachine.IdleState);
    }

    private void GetComponents()
    {
        if (Animator == null) Animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 스텟 타입과 값을 딕셔너리로 관리하기 위해 초기화
    /// </summary>
    private void ConvertStatListToDict()
    {
        StatDict = new();

        foreach (StatEntry statEntry in Stat.Stats)
        {
            StatDict[statEntry.StatType] = statEntry.BaseValue;
        }

        if (!StatDict.TryGetValue(StatType.Health, out _hp))
        {
            Logger.Log("HP 데이터 없음");
        }
    }
    #endregion

    #region IAttackable 구현
    public void TakeDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0) Die();
    }

    private void Die()
    {
        Logger.Log("몬스터 사망");
        // todo: 오브젝트 풀링으로 관리 or destroy
    }
    #endregion
}
