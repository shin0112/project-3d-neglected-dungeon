using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public MonsterAnimationData AnimationData { get; private set; }
    protected MonsterStateMachine monsterStateMachine;

    [field: SerializeField] public bool IsAlive { get; private set; } = true;

    #region 초기화
    protected void Awake()
    {
        GetComponents();

        AnimationData.Initialize();
        this.monsterStateMachine = new(this);
        this.monsterStateMachine.ChangeState(monsterStateMachine.IdleState);
    }

    private void GetComponents()
    {
        Animator = GetComponent<Animator>();
    }
    #endregion
}
