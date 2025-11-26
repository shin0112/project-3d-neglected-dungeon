using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour, IAttackable, IPoolable
{
    [field: Header("Data")]
    [field: SerializeField] public MonsterData Data { get; private set; }
    public Dictionary<StatType, float> StatDict { get; private set; }

    [field: Header("Animation")]
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public MonsterAnimationData AnimationData { get; private set; }
    protected MonsterStateMachine monsterStateMachine;

    private float _hp;
    private event Action<GameObject> _returnAction;
    public bool IsAlive => _hp > 0;

    public event Action<Monster> OnDead;

    // IAttackable
    public Transform Transform => transform;

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

        foreach (StatEntry statEntry in Data.Stat.Stats)
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
        Logger.Log($"{damage}의 데미지");
        _hp -= damage;
        if (_hp <= 0) Die();
    }

    private void Die()
    {
        Logger.Log("몬스터 사망");
        OnDead?.Invoke(this);
        ReturnToPool();
    }
    #endregion

    #region IPoolable 구현
    public void Initialize(Action<GameObject> returnAction)
    {
        _returnAction = returnAction;
    }

    public void OnSpawn()
    {
        _hp = StatDict[StatType.Health];
    }

    public void ReturnToPool()
    {
        _returnAction?.Invoke(gameObject);
    }
    #endregion
}
