using UnityEngine;

public class Monster : MonoBehaviour
{
    [field: SerializeField] public bool IsAlive { get; private set; } = true;
}
