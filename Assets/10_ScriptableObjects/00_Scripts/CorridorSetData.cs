using UnityEngine;

/// <summary>
/// 맵 연결 시 사용할 통로 저장
/// </summary>
[CreateAssetMenu(
    fileName = "new Corridor Set",
    menuName = "Scriptable Object/Dungeon/Corridor Set")]
public class CorridorSetData : ScriptableObject
{
    [field: SerializeField] public CorridorType CorridorType;
    [field: SerializeField] public GameObject[] CorridorPrefabs;
}
