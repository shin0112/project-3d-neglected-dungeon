using System;
using Unity.AI.Navigation;
using UnityEngine;

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

        // Dungeon 생성
        private MapGenerator _mapGenerator;
        private Transform _dungeonRoot;
        private NavMeshSurface _surface;

        // 이벤트
        public Action<string> OnDungeonNameFixed;
        #endregion

        #region 초기화 & 파괴
        public void Initialize(CorridorSetData[] corridors)
        {
            // Dungeon 생성
            GameObject dungeonObj = new GameObject("DungeonRoot");
            _dungeonRoot = dungeonObj.transform;
            SetNavMeshSurface(dungeonObj);

            Spawner = new(_dungeonRoot);
            _mapGenerator = new(corridors);
        }

        private void SetNavMeshSurface(GameObject dungeonObj)
        {
            _surface = dungeonObj.AddComponent<NavMeshSurface>();

            // settings
            _surface.collectObjects = CollectObjects.Children;
            _surface.layerMask = Define.FloorLayer;
            _surface.useGeometry = UnityEngine.AI.NavMeshCollectGeometry.PhysicsColliders;
            _surface.buildHeightMesh = true;
        }

        public void OnDestroy()
        {
            OnDungeonNameFixed = null;
            Spawner.OnDestroy();
        }
        #endregion

        #region [public] 던전 시작
        /// <summary>
        /// [public] 던전 데이터를 받아 해당 던전 0번째 스테이지 시작
        /// </summary>
        /// <param name="dungeon"></param>
        public void StartDungeon(DungeonData dungeon)
        {
            ResetDungeon();

            _dungeonData = dungeon;
            _curStageIndex = 0;
            StartStage(_curStageIndex);

            OnDungeonNameFixed?.Invoke(_dungeonData.dungeonName);
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

            // 1) 맵 자동 생성
            _mapGenerator.Generate(_dungeonRoot, stage);

            // 2) Nav Bake
            _surface.RemoveData();
            _surface.BuildNavMesh();

            RegisterStagePools(stage);                  // stage go pool에 등록
            Spawner.Initialize(stage);                  // monster pool 초기화
        }

        private void NextStage()
        {
            _curStageIndex++;

            if (_curStageIndex >= _dungeonData.stages.Length)
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
        /// 던전 정보 리셋하기
        /// </summary>
        private void ResetDungeon()
        {
            // 맵 삭제
            if (_dungeonRoot != null)
            {
                for (int i = _dungeonRoot.childCount - 1; i >= 0; i--)
                {
                    UnityEngine.Object.Destroy(_dungeonRoot.GetChild(i).gameObject);
                }
            }

            if (_surface)
            {
                _surface.RemoveData();
            }

            if (Spawner != null)
            {
                Spawner.OnDestroy();
                Spawner = new MonsterSpawner(_dungeonRoot);
            }
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
