public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.MovementController.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        TargetingController targeting = stateMachine.Player.Targeting;

        // 타겟 범위에 몬스터가 있는지 확인
        if (!targeting.CheckTargetInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
            return;
        }
    }
}
