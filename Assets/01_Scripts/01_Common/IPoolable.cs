using System;
using UnityEngine;

public interface IPoolable
{
    public void Initialize(Action<GameObject> callback);
    public void OnSpawn();
    public void OnDespwan();
}
