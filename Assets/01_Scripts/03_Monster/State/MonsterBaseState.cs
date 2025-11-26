public class MonsterBaseState : IState
{
    protected MonsterStateMachine stateMachine;
    protected MonsterAnimationData animationData;

    public MonsterBaseState(MonsterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.animationData = this.stateMachine.Monster.AnimationData;
    }

    #region 애니메이션 관리
    /// <summary>
    /// 애니메이션 시작
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Monster.Animator.SetBool(animationHash, true);
    }

    /// <summary>
    /// 애니메이션 정지
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Monster.Animator.SetBool(animationHash, false);
    }
    #endregion

    #region 인터페이스 구현
    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
    }
    #endregion
}
