using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields
    // Components
    [SerializeField] private Player _player;
    [SerializeField] private CharacterController _controller;
    public Transform MainCameraTransform { get; set; }

    // Movement Fields
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public float RotationDamping { get; private set; }

    public float JumpForce { get; set; }

    // AI Nav
    private Vector3 _target;
    #endregion

    private void Reset()
    {
        _player = transform.FindChild<Player>("Player");
        _controller = transform.FindChild<CharacterController>("Player");
    }

    private void Awake()
    {
        this.MainCameraTransform = Camera.main.transform;
    }

    #region 움직임 구현
    /// <summary>
    /// [Public] 이동 방향을 받아 이동 및 회전
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector3 direction)
    {
        Rotate(direction);
        _controller.Move(direction * GetMovementSpeed());
    }

    /// <summary>
    /// 이동 방향 가져오기
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMovementDirection()
    {
        Vector3 forward = MainCameraTransform.forward;
        Vector3 right = MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * MovementInput.y + right * MovementInput.x;
    }

    /// <summary>
    /// 이동 속도 가져오기
    /// </summary>
    /// <returns></returns>
    private float GetMovementSpeed()
    {
        float moveSpeed = MovementSpeed * MovementSpeedModifier;
        return moveSpeed;
    }

    /// <summary>
    /// 바라보고 있는 방향으로 회전하기
    /// </summary>
    /// <param name="direction"></param>
    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform playerTransform = _player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(
                playerTransform.rotation,
                targetRotation,
                RotationDamping * Time.deltaTime);
        }
    }
    #endregion

}
