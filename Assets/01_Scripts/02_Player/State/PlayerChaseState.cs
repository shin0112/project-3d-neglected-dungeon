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
        TargetingController targeting = stateMachine.Player.Targeting;

        // todo: target이 일정 범위 내에 있을 경우 공격상태로 변경
        float sqrDist = targeting.GetDistanceFromTarget();
        float attackRange = stateMachine.Player.State.AttackData.AttackInfoDatas[0].AttackRadius;
        if (sqrDist <= attackRange * attackRange)
        {
            Logger.Log("공격 시작");
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }

        // todo: target 리셋 로직 추가 (몬스터 or 플레이어 사망)
        if (targeting.CurTarget == null)
        {
            Logger.Log("타겟 없음");
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}
