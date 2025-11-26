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

        private Dictionary<ScriptableObject, Queue<GameObject>> _pools = new();

        public void Register(ScriptableObject key, GameObject prefab, int prewarm = 0)
        {
            if (!_pools.ContainsKey(key))
            {
                _pools[key] = new Queue<GameObject>();
            }

            for (int i = 0; i < prewarm; i++)
            {
                GameObject obj = Object.Instantiate(prefab);
                obj.SetActive(false);

                obj.GetComponent<IPoolable>()?.Initialize(o => ReturnObject(key, o));
                _pools[key].Enqueue(obj);
            }
        }

        public GameObject GetObject(ScriptableObject key, GameObject prefab, Vector3 position, Quaternion roataion)
        {
            if (!_pools.TryGetValue(key, out Queue<GameObject> pool))
            {
                Logger.Log($"프리팹 {key.name} 풀 없음");
                return null;
            }

            GameObject obj;

            if (pool.Count > 0)
            {
                obj = pool.Dequeue();
            }
            else
            {
                obj = Object.Instantiate(prefab);
                obj.GetComponent<IPoolable>()?.Initialize(o => ReturnObject(key, o));
            }

            obj.transform.SetPositionAndRotation(position, roataion);
            obj.SetActive(true);

            obj.GetComponent<IPoolable>()?.OnSpawn();
            return obj;
        }

        public void ReturnObject(ScriptableObject key, GameObject obj)
        {
            if (!_pools.TryGetValue(key, out Queue<GameObject> pool))
            {
                Destroy(obj);
                return;
            }

            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
