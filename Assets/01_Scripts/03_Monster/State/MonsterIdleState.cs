public class MonsterIdleState : MonsterBaseState
{
    public MonsterIdleState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(animationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(animationData.IdleParameterHash);
    }
}
