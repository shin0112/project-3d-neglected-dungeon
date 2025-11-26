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

        // Idle 상태로 변경
        // 1) 타겟 범위에 몬스터가 있는지 확인
        // 2) 타겟팅한 몬스터가 사망했을 경우
        if (!targeting.CheckTargetInAttackRange() || !targeting.CurTarget.IsAlive)
        {
            targeting.ClearCurrentTarget();
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }
}
