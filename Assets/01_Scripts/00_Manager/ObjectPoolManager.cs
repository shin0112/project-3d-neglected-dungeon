using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트 풀 매니저
/// </summary>
public partial class Managers
{
    public class ObjectPoolManager
    {
        internal ObjectPoolManager() { }

        private GameObject[] _objs;
        private Dictionary<int, Queue<GameObject>> _pools = new();

        public void Initialize(GameObject[] objs)
        {
            _objs = objs;

            for (int i = 0; i < objs.Length; i++)
            {
                _pools[i] = new Queue<GameObject>();
            }
        }

        public GameObject GetObject(int prefabIndex, Vector3 position, Quaternion roataion)
        {
            if (!_pools.TryGetValue(prefabIndex, out Queue<GameObject> pool))
            {
                Logger.Log($"프리팹 인덱스 {prefabIndex} 풀 없음");
                return null;
            }

            GameObject obj;
            if (pool.Count > 0)
            {
                obj = pool.Dequeue();
            }
            else
            {
                obj = Object.Instantiate(_objs[prefabIndex]);
                obj.GetComponent<IPoolable>()?.Initialize(o => ReturnObject(prefabIndex, o));
            }

            obj.transform.SetPositionAndRotation(position, roataion);
            obj.SetActive(true);
            obj.GetComponent<IPoolable>()?.OnSpawn();
            return obj;
        }

        private void ReturnObject(int prefabIndex, GameObject obj)
        {
            if (!_pools.TryGetValue(prefabIndex, out Queue<GameObject> pool))
            {
                Object.Destroy(obj);
                return;
            }

            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
