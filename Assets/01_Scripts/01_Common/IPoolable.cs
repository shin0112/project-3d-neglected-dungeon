using System;
using UnityEngine;

public interface IPoolable
{
    public void Initialize(Action<GameObject> returnAction);
    public void OnSpawn();
    public void ReturnToPool();
}
