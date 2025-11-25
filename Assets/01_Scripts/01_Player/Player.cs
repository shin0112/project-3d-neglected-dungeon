using UnityEngine;

/// <summary>
/// 플레이어와 관련된 데이터를 컨트롤하기 위한 일종의 컨테이너
/// </summary>
public class Player : MonoBehaviour
{
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    [field: Header("Info")]
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public PlayerController Input { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }

    private void Reset()
    {
        Animator = transform.FindChild<Animator>("Player");
        Input = transform.FindChild<PlayerController>("Player");
        Controller = transform.FindChild<CharacterController>("Player");
    }

    private void Awake()
    {
        AnimationData.Initialize();
    }
}
