using UnityEngine;

/// <summary>
/// 플레이어가 공격할 타겟을 스캔 및 계산해 지정하는 클래스
/// </summary>
[System.Serializable]
public class TargetingController
{
    private readonly Player _player;
    private float _timer;

    // 몬스터 탐지
    [field: SerializeField] public Monster CurTarget { get; private set; }
    [field: SerializeField] public Monster ScanTarget { get; private set; }
    [SerializeField] private float _squreDetectDistance;

    public TargetingController(Player player)
    {
        this._player = player;
        float detectDistance = _player.Condition[StatType.DetectDistance].Value;
        _squreDetectDistance = detectDistance * detectDistance;
    }

    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < 0.5f) return;
        //Logger.Log("타겟 탐색");
        _timer = 0f;

        ScanNearestTarget();
    }

    #region [public] 현재 타겟 관리
    /// <summary>
    /// [public] 현재 타겟을 스캔한 몬스터로 지정
    /// </summary>
    public void FixCurrentTarget()
    {
        CurTarget = ScanTarget;
    }

    /// <summary>
    /// [public] 현재 타겟 리셋
    /// </summary>
    public void ClearCurrentTarget()
    {
        CurTarget = null;
    }

    /// <summary>
    /// [public] 현재 타겟이 공격 범위 내에 있는지 확인
    /// </summary>
    /// <returns></returns>
    public bool CheckTargetInAttackRange()
    {
        if (CurTarget == null) return false;

        AttackInfoData defaultAttackInfo = _player.State.AttackData.AttackInfoDatas[0];
        return IsInFanArea(
            _player.transform,
            CurTarget.transform,
            defaultAttackInfo.AttackRadius,
            defaultAttackInfo.AttackAngle);
    }

    /// <summary>
    /// [public] 공격 타겟이 공격 범위 내에 있는지 확인
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool CheckTargetInAttackRange(IAttackable target)
    {
        AttackInfoData defaultAttackInfo = _player.State.AttackData.AttackInfoDatas[0];
        return IsInFanArea(
            _player.transform,
            target.Transform,
            defaultAttackInfo.AttackRadius,
            defaultAttackInfo.AttackAngle);
    }
    #endregion

    #region 타겟 탐색 내부 로직
    /// <summary>
    /// 근처의 타겟 몬스터를 탐색해서 반환
    /// </summary>
    /// <returns></returns>
    private void ScanNearestTarget()
    {
        Monster closest = null;
        float bestDist = float.MaxValue;

        foreach (var m in Managers.Instance.Dungeon.Spawner.AliveMonsters)
        {
            if (!m.IsAlive) continue;

            float dist = (m.transform.position - _player.transform.position).sqrMagnitude;

            // 탐지 반경 밖이면 건너뛰기
            if (dist > _squreDetectDistance)
                continue;

            // 더 가까운 몬스터 선택
            if (dist < bestDist)
            {
                bestDist = dist;
                closest = m;
            }
        }

        ScanTarget = closest;
    }

    bool IsInFanArea(Transform attacker, Transform target, float radius, float angle)
    {
        // 1) 거리 체크
        Vector3 toTarget = target.position - attacker.position;
        float sqrDist = toTarget.sqrMagnitude;
        if (sqrDist > radius * radius)
            return false;

        // 2) 각도 체크 
        Vector3 forward = attacker.forward;
        Vector3 direction = toTarget.normalized;

        float dot = Vector3.Dot(forward, direction);
        float cos = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);

        return dot >= cos;
    }
    #endregion
}
