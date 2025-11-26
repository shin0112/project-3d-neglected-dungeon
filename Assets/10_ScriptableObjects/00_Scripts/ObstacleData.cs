using UnityEngine;

[CreateAssetMenu(fileName = "new Obstacle Data", menuName = "Entities/Obstacle")]
public class ObstacleData : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
}
