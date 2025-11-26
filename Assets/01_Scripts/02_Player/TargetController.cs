using System;
using UnityEngine;

[Serializable]
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
        _player.StatDict.TryGetValue(StatType.DetectDistance, out float detectDistance);
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
    /// [public] 타겟이 공격 범위 내에 있는지 확인
    /// </summary>
    /// <returns></returns>
    public bool CheckTargetInAttackRange()
    {
        float sqrDist = GetDistanceFromTarget();
        float attackRadius = _player.State.AttackData.AttackInfoDatas[0].AttackRadius;
        if (sqrDist <= attackRadius * attackRadius)
        {
            Logger.Log("공격 범위 내 타겟 존재");
            return false;
        }

        return true;
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

        foreach (var m in Managers.Instance.Monster.Monsters)
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

    /// <summary>
    /// 타겟과 플레이어 사이의 거리 반환
    /// </summary>
    /// <returns></returns>
    private float GetDistanceFromTarget()
    {
        return (_player.transform.position - CurTarget.transform.position).sqrMagnitude;
    }
    #endregion
}
