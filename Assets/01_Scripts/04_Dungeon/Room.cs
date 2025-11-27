using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Transform[] _monsterSpawnPoints;

    public Transform GetRandomMonsterSpawnPoint()
    {
        if (_monsterSpawnPoints.Length == 0) return null;
        return _monsterSpawnPoints.Random();
    }
}
