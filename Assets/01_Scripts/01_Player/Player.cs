using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerController Input { get; private set; }
    public CharacterController Controller { get; private set; }

    private void Reset()
    {
        transform.FindChild<PlayerController>("Player");
    }

    private void Awake()
    {
        AnimationData.Initialize();
    }
}
