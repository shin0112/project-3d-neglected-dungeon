/// <summary>
/// 플레이어 움직임 상태 관리 정보를 저장하는 컨테이너 스크립트
/// </summary>
public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    // Animation State
    // Ground
    public PlayerIdleState IdleState { get; private set; }
    public PlayerChaseState ChaseState { get; private set; }

    // Attack
    public PlayerAttackState AttackState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        // Ground State
        this.IdleState = new PlayerIdleState(this);
        this.ChaseState = new PlayerChaseState(this);

        // Attack State
        this.AttackState = new PlayerAttackState(this);
    }
}
