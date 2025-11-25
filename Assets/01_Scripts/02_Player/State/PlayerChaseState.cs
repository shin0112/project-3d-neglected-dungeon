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

        // todo: target 리셋 로직 추가 (몬스터 or 플레이어 사망)
        if (target == null)
        {
            Logger.Log("타겟 없음");
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}
