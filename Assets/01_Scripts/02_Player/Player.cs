using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 관련된 데이터를 컨트롤하기 위한 일종의 컨테이너
/// </summary>
public class Player : MonoBehaviour
{
    #region 필드
    [field: Header("Component")]
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    public Transform MainCameraTransform { get; set; }

    [field: Header("Data")]
    [field: SerializeField] public PlayerStateData State { get; private set; }
    [field: SerializeField] public PlayerStatData Stat { get; private set; }
    public Dictionary<StatType, float> StatDict { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    private PlayerStateMachine _stateMachine;

    [field: Header("AI Nav")]
    [field: SerializeField] public PlayerController MovementController { get; private set; }
    [field: SerializeField] public TargetingController Targeting { get; private set; }
    #endregion

    #region 초기화
    private void Reset()
    {
        Animator = transform.FindChild<Animator>("Model");
        Controller = transform.FindChild<CharacterController>("Player");
    }

    private void Awake()
    {
        // Component
        GetComponents();

        // Data
        ConvertStatListToDict();

        // Animation
        AnimationData.Initialize();
        _stateMachine = new PlayerStateMachine(this);
        _stateMachine.ChangeState(_stateMachine.IdleState);

        // Ai nav
        MovementController = new(this);
        Targeting = new(this);
    }

    private void GetComponents()
    {
        if (Animator == null) Animator = GetComponent<Animator>();
        if (Controller == null) Controller = GetComponent<CharacterController>();

        PlayerAnimationEventRelay relay = GetComponentInChildren<PlayerAnimationEventRelay>();
        relay.Player = this;
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
    }
    #endregion

    private void Update()
    {
        Targeting.Update();
        _stateMachine.Update();

        if (Targeting.CurTarget != null)
        {
            MovementController.Move(Targeting.CurTarget);
        }
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }

    #region 공격 이벤트
    public void OnAttackHit()
    {
        AttackInfoData attackInfoData = State.AttackData.AttackInfoDatas[0];

        foreach (Collider col in Physics.OverlapSphere(
            transform.position,
            attackInfoData.AttackRadius))
        {
            if (col.TryGetComponent(out IAttackable attackable))
            {
                if (Targeting.CheckTargetInAttackRange(attackable))
                {
                    attackable.TakeDamage(attackInfoData.Damage);
                }
            }
        }
    }
    #endregion
}
