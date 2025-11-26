using UnityEngine;

/// <summary>
/// Room 간 연결을 위한 door position 저장 클래스
/// </summary>
public class RoomConnectors : MonoBehaviour
{
    [field: SerializeField] public Transform[] DoorPoints { get; private set; }
}
