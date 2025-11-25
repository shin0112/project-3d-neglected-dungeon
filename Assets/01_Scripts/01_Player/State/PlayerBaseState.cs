using UnityEngine;

/// <summary>
/// 플레이어 상태 기본 스크립트
/// </summary>
public class PlayerBaseState : IState
{
    #region Fields
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.groundData = stateMachine.Player.State.GroundData;
    }
    #endregion

    #region 애니메이션 관리
    /// <summary>
    /// 애니메이션 시작
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    /// <summary>
    /// 애니메이션 정지
    /// </summary>
    /// <param name="animationHash"></param>
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
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
        stateMachine.Player.Input.Move(Vector3.zero);
    }
    #endregion
}
