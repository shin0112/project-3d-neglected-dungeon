using UnityEngine;

/// <summary>
/// 플레이어와 관련된 데이터를 컨트롤하기 위한 일종의 컨테이너
/// </summary>
public class Player : MonoBehaviour
{
    #region 필드
    [field: Header("Component")]
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: Header("Condition")]
    public PlayerCondition Condition { get; private set; }
    public PlayerWallet Wallet { get; private set; }

    [field: Header("SO Data")]
    [field: SerializeField] public PlayerStateData State { get; private set; }
    [field: SerializeField] private PlayerStatData Stat { get; set; }

    [field: Header("Animation")]
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
    }

    private void Awake()
    {
        // Component
        GetComponents();

        // Condition
        // 스텟 데이터 사용해 초기화하는 경우 예외 처리
        try
        {
            Condition = new PlayerCondition(Stat);
            Targeting = new TargetingController(this);
        }
        catch (StatDataException ex)
        {
            Logger.LogError(ex.Message);
            throw;
        }
        Wallet = new();

        // Animation
        AnimationData.Initialize();
        _stateMachine = new PlayerStateMachine(this);
        _stateMachine.ChangeState(_stateMachine.IdleState);

        // Ai nav
        if (!TryGetComponent<CharacterController>(out var controller))
        {
            Logger.Log("CharacterController is null");
        }
        MovementController = new(this, controller);
    }

    private void GetComponents()
    {
        if (Animator == null) Animator = GetComponent<Animator>();

        PlayerAnimationEventRelay relay = GetComponentInChildren<PlayerAnimationEventRelay>();
        relay.Player = this;
    }
    #endregion

    #region Unity API
    private void Update()
    {
        Targeting.Update();
        _stateMachine.Update();

        if (Targeting.CurTarget != null)
        {
            MovementController.Move(Targeting.CurTarget);
        }

        //Condition.TryUseStamina(1f * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }

    private void OnDestroy()
    {
        Condition.OnDestroy();
        Wallet.OnDestroy();
    }
    #endregion

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

    /// <summary>
    /// 공격 범위 및 각도 확인하기
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        AttackInfoData attackInfoData = State.AttackData.AttackInfoDatas[0];

        float radius = attackInfoData.AttackRadius;
        float angle = attackInfoData.AttackAngle;

        Vector3 pos = transform.position;
        Vector3 left = Quaternion.Euler(0, -angle / 2f, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, angle / 2f, 0) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pos, pos + left * radius);
        Gizmos.DrawLine(pos, pos + right * radius);
        Gizmos.DrawWireSphere(pos, radius);
    }
    #endregion
}
