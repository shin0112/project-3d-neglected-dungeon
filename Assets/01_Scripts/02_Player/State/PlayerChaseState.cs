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

        if (CheckTargetInAttackRadius(targeting))
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
        else
        {
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

    /// <summary>
    /// 타겟이 공격 범위 내에 있는지 확인
    /// </summary>
    /// <param name="targeting"></param>
    /// <returns></returns>
    private bool CheckTargetInAttackRadius(TargetingController targeting)
    {
        float sqrDist = targeting.GetDistanceFromTarget();
        float attackRadius = stateMachine.Player.State.AttackData.AttackInfoDatas[0].AttackRadius;
        if (sqrDist <= attackRadius * attackRadius)
        {
            Logger.Log("공격 범위 내 타겟 존재");
            return false;
        }

        return true;
    }
}
