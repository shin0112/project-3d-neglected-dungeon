using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터 풀을 관리하는 스포너
/// </summary>
public class MonsterSpawner
{
    #region 필드
    private StageData _stageData;

    // 현재 몬스터 상태
    private List<Monster> _alives = new();
    private int _killCount = 0;
    private bool _bossSpawned = false;
    private bool _bossDead = false;

    // 코루틴
    private Coroutine _spawnCoroutine;
    private WaitForSeconds _spawnDelay = new WaitForSeconds(Define.RespawnDelay);

    public IReadOnlyList<Monster> AliveMonsters => _alives;
    public bool StageClear => _bossSpawned && _bossDead;
    #endregion

    #region 초기화
    public void Initialize(StageData stageData)
    {
        _stageData = stageData;

        _alives.Clear();
        _killCount = 0;
        _bossSpawned = false;
        _bossDead = false;

        for (int i = 0; i < _stageData.MaxEnemyCount; i++)
        {
            SpawnOneEnemy();
        }
    }
    #endregion

    #region 몬스터 관리
    /// <summary>
    /// 일반 몬스터 단일 스폰
    /// </summary>
    private void SpawnOneEnemy()
    {
        MonsterData data = _stageData
            .MonsterPool[Random.Range(0, _stageData.MonsterPool.Length)]
            .MonsterData;

        // todo: 스폰 포인트 만들어서 지정
        Vector3 position = Vector3.right * -10f;

        GameObject obj = Managers.Instance.ObjectPool.GetObject(data, data.Prefab, position, Quaternion.identity);

        Monster monster = obj.GetComponent<Monster>();

        monster.OnDead += OnMonsterDead;

        _alives.Add(monster);
    }

    /// <summary>
    /// 보스 몬스터 스폰
    /// </summary>
    private void SpawnBoss()
    {
        // todo: 보스 스폰 위치 지정
        Vector3 position = Vector3.zero;
        GameObject bossPrefab = _stageData.BossData.Prefab;

        GameObject obj = Object.Instantiate(bossPrefab, position, Quaternion.identity);

        Monster boss = obj.GetComponent<Monster>();
        boss.OnDead += OnBossMonsterDead;
    }

    /// <summary>
    /// 몬스터 처치 시 실행
    /// monster.OnDead에 등록
    /// </summary>
    /// <param name="monster"></param>
    private void OnMonsterDead(Monster monster)
    {
        _alives.Remove(monster);
        _killCount++;

        Managers.Instance.Player.Wallet[WalletType.Gold].Add(monster.Data.DropGold);

        monster.OnDead -= OnMonsterDead;
        monster.ReturnToPool();

        if (_bossSpawned) return;

        // todo: 버튼 클릭으로 보스 스폰하게 지정 -> 플레이어 앞에 보스 생성
        if (_killCount >= Define.KillCountForMidBossSpawn)
        {
            SpawnBoss();
            _bossSpawned = true;
        }
    }

    /// <summary>
    /// 보스 몬스터 처치 시 실행
    /// boss.OnDead에 등록
    /// </summary>
    /// <param name="boss"></param>
    private void OnBossMonsterDead(Monster boss)
    {
        Managers.Instance.Player.Wallet[WalletType.Gold].Add(boss.Data.DropGold);

        boss.OnDead -= OnBossMonsterDead;
        boss.ReturnToPool();

        _bossDead = true;
        // todo: 보스 처지 시 클리어 로직
    }

    /// <summary>
    /// 스테이지에 소환될 수 있는 최대 숫자가 될 때까지 리스폰
    /// </summary>
    /// <returns></returns>
    private IEnumerator RespawnCoroutine()
    {
        yield return _spawnDelay;

        if (_alives.Count < _stageData.MaxEnemyCount)
        {
            SpawnOneEnemy();
        }
    }
    #endregion
}
