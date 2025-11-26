/// <summary>
/// 던전 정보 및 흐름을 관리하는 매니저
/// </summary>
public partial class Managers
{
    public class DungeonManager
    {
        #region 필드
        internal DungeonManager() { }

        // Data
        private DungeonData _dungeonData;
        private int _curStageIndex = 0;

        // Pool 관리
        public MonsterSpawner Spawner { get; private set; }

        // todo: 맵 자동 생성
        private MapGenerator _mapGenerator;

        #endregion

        #region 초기화
        public void Initialize(CorridorSetData[] corridors)
        {
            Spawner = new();
            _mapGenerator = new(corridors);
        }
        #endregion

        #region [public] 던전 시작
        /// <summary>
        /// 던전 데이터를 받아 해당 던전 0번째 스테이지 시작
        /// </summary>
        /// <param name="dungeon"></param>
        public void StartDungeon(DungeonData dungeon)
        {
            _dungeonData = dungeon;
            _curStageIndex = 0;
            StartStage(_curStageIndex);
        }
        #endregion

        #region 스테이지 관리
        /// <summary>
        /// 선택한 스테이지 시작
        /// </summary>
        /// <param name="stageIndex"></param>
        private void StartStage(int stageIndex)
        {
            StageData stage = _dungeonData.stages[stageIndex];

            // todo: 맵
            // 1) 맵 자동 생성
            _mapGenerator.CreateRandomMap(stage);

            // 2) Nav Bake

            RegisterStagePools(stage);       // stage go pool에 등록
            Spawner.Initialize(stage);                 // monster pool 초기화
        }

        private void NextStage()
        {
            _curStageIndex++;

            if (_curStageIndex >= _dungeonData.stages.Count)
            {
                DungeonClear();
                return;
            }

            StartStage(_curStageIndex);
        }

        private void DungeonClear()
        {
            Logger.Log("던전 클리어");
        }

        /// <summary>
        /// StageData를 받아 필요한 pool key 자동 등록
        /// Stage 시작 시 호출됨
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="prewarm"></param>
        private void RegisterStagePools(StageData stage, int prewarm = 0)
        {
            ObjectPoolManager pool = Managers.Instance.ObjectPool;

            // 일반 몬스터 등록
            foreach (MonsterSpawnData spawn in stage.MonsterPool)
            {
                MonsterData data = spawn.MonsterData;
                pool.Register(data, data.Prefab, prewarm);
            }

            // 보스 몬스터 등록
            if (stage.BossData != null)
            {
                pool.Register(stage.BossData, stage.BossData.Prefab, 1);
            }

            // 장애물 등록
            if (stage.ObstacleDatas != null)
            {
                foreach (ObstacleData data in stage.ObstacleDatas)
                {
                    pool.Register(data, data.Prefab);
                }
            }

            Logger.Log("스테이지 풀 등록 완료");
        }
        #endregion
    }
}
