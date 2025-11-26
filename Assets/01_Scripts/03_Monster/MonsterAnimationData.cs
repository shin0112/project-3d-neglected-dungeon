using System;
using UnityEngine;

/// <summary>
/// 몬스터 애니메이션 컨트롤 시 필요한 Parameter 및 Hash 관리
/// </summary>
[Serializable]
public class MonsterAnimationData
{
    [Header("Ground")]
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _walkParameterName = "Walk";
    [SerializeField] private string _runParameterName = "Run";

    [Header("Battle")]
    [SerializeField] private string _battleParameterName = "@Battle";
    [SerializeField] private string _attackParameterName = "Attack";
    [SerializeField] private string _hitParameterName = "Hit";
    [SerializeField] private string _dieParameterName = "Die";

    // Ground
    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    // Attack
    public int BattleParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }

    public void Initialize()
    {
        // Ground
        GroundParameterHash = Animator.StringToHash(_groundParameterName);
        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        WalkParameterHash = Animator.StringToHash(_walkParameterName);
        RunParameterHash = Animator.StringToHash(_runParameterName);

        // Attack
        BattleParameterHash = Animator.StringToHash(_battleParameterName);
        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        HitParameterHash = Animator.StringToHash(_hitParameterName);
        DieParameterHash = Animator.StringToHash(_dieParameterName);
    }
}
