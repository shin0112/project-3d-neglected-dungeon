public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; }

    // Animation State
    // Ground
    public MonsterIdleState IdleState { get; private set; }

    public MonsterStateMachine(Monster monster)
    {
        this.Monster = monster;

        // Ground State
        this.IdleState = new MonsterIdleState(this);
    }
}
