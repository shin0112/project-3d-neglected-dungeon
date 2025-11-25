public class PlayerChaseState : PlayerGroundState
{
    public PlayerChaseState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.MovementController.MovementSpeedModifier = 1f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    public override void Update()
    {
        Monster target = stateMachine.Player.Targeting.CurTarget;

        if (target == null)
        {
            Logger.Log("타겟 없음");
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}
