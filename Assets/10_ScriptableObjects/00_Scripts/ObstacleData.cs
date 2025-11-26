using UnityEngine;

[CreateAssetMenu(fileName = "new Obstacle Data", menuName = "Scriptable Object/Entity/Obstacle")]
public class ObstacleData : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
}
