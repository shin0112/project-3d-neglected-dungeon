using UnityEngine;

/// <summary>
/// 플레이어 애니메이션에 적용할 데이터
/// </summary>
[System.Serializable]
public class PlayerAnimationData
{
    [Header("Ground")]
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _walkParameterName = "Walk";
    [SerializeField] private string _runParameterName = "Run";

    [Header("Air")]
    [SerializeField] private string _airParameterName = "@Air";
    [SerializeField] private string _jumpParameterName = "Jump";
    [SerializeField] private string _fallParameterName = "Fall";

    [Header("Attack")]
    [SerializeField] private string _attackParameterName = "@Attack";
    [SerializeField] private string _comboAttackParameterName = "ComboAttack";

    [Header("Result")]
    [SerializeField] private string _resultParameterName = "@Result";
    [SerializeField] private string _victoryParameterName = "Victory";
    [SerializeField] private string _defeatParameterName = "Defeat";

    // Ground
    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    // Air
    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }

    // Attack
    public int AttackParameterHash { get; private set; }
    public int ComboAttackParameterHash { get; private set; }

    // Result
    public int ResultParameterHash { get; private set; }
    public int VictoryParameterHash { get; private set; }
    public int DefeatParameterHash { get; private set; }

    public void Initialize()
    {
        // Ground
        GroundParameterHash = Animator.StringToHash(_groundParameterName);
        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        WalkParameterHash = Animator.StringToHash(_walkParameterName);
        RunParameterHash = Animator.StringToHash(_runParameterName);

        // Air
        AirParameterHash = Animator.StringToHash(_airParameterName);
        JumpParameterHash = Animator.StringToHash(_jumpParameterName);
        FallParameterHash = Animator.StringToHash(_fallParameterName);

        // Attack
        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        ComboAttackParameterHash = Animator.StringToHash(_comboAttackParameterName);

        // Result
        ResultParameterHash = Animator.StringToHash(_resultParameterName);
        VictoryParameterHash = Animator.StringToHash(_victoryParameterName);
        DefeatParameterHash = Animator.StringToHash(_defeatParameterName);
    }
}
