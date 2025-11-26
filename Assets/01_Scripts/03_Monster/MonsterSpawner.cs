using System.Collections;
using UnityEngine;

/// <summary>
/// 몬스터 풀 관리하기
/// </summary>
public class MonsterSpawner
{
    #region 필드
    private StageData _stageData;
    private int _alive = 0;
    private int _killCount = 0;
    private bool _bossSpawned = false;
    private bool _bossDead = false;
    private Coroutine _spawnCoroutine;
    private WaitForSeconds _spawnDelay = new WaitForSeconds(Define.RespawnDelay);

    public bool StageClear => _bossSpawned && _bossDead;
    #endregion

    #region 초기화
    public void Initialize(StageData stageData)
    {
        _stageData = stageData;

        _alive = 0;
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
        Vector3 position = Vector3.zero;

        GameObject obj = Managers.Instance.ObjectPool.GetObject(data, data.Prefab, position, Quaternion.identity);

        Monster m = obj.GetComponent<Monster>();

        m.OnDead += OnMonsterDead;

        _alive++;
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
        _alive--;
        _killCount++;

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

        if (_alive < _stageData.MaxEnemyCount)
        {
            SpawnOneEnemy();
        }
    }
    #endregion
}
