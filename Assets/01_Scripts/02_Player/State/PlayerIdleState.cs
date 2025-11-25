public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.MovementController.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        TargetingController targeting = stateMachine.Player.Targeting;

        if (targeting.ScanTarget != null)
        {
            Logger.Log("타겟 탐색");
            targeting.FixCurrentTarget();
            stateMachine.ChangeState(stateMachine.ChaseState);
            return;
        }
    }
}
