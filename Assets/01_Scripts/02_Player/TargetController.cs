using System;
using UnityEngine;

[Serializable]
public class TargetingController
{
    private readonly Player _player;
    private float _timer;

    // 몬스터 탐지
    [field: SerializeField] public Monster CurTarget { get; private set; }
    [SerializeField] private float _squreDetectDistance;

    public TargetingController(Player player)
    {
        this._player = player;
        _player.StatDict.TryGetValue(StatType.DetectDistance, out float detectDistance);
        _squreDetectDistance = detectDistance * detectDistance;
    }

    public void Update(float deltaTime)
    {
        _timer += deltaTime;
        if (_timer < 0.2f) return;
        Logger.Log("타겟 업데이트");
        _timer = 0f;

        CurTarget = GetClosestTarget();
    }

    private Monster GetClosestTarget()
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

        return closest;
    }
}
