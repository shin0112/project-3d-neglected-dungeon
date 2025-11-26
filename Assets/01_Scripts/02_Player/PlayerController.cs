using UnityEngine;

/// <summary>
/// CharacterController를 사용해 플레이어의 이동 및 회전을 관리하는 클래스
/// </summary>
[System.Serializable]
public class PlayerController
{
    #region 필드
    // Components
    [field: SerializeField] private Player _player;
    [field: SerializeField] private NavigationController _navigation;
    [field: SerializeField] private CharacterController _controller;
    [field: SerializeField] private Transform _mainCameraTransform;

    // Movement Fields
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float MovementSpeedModifier { get; set; } = 1f;
    [field: SerializeField] private Vector3 _movementDirection;

    [field: SerializeField] public float RotationDamping { get; private set; }

    [field: SerializeField] public float JumpForce { get; set; }
    #endregion

    public PlayerController(Player player, CharacterController controller)
    {
        _player = player;
        _navigation = new();
        _controller = controller;
        _mainCameraTransform = Camera.main.transform;

        MovementSpeed = _player.State.GroundData.BaseSpeed;
        RotationDamping = _player.State.GroundData.BaseRotationDamping;
    }

    #region 움직임 구현
    /// <summary>
    /// [Public] 타겟의 위치를 확인한 후 경로를 구해 회전 및 이동
    /// </summary>
    /// <param name="target"></param>
    public void Move(Monster target)
    {
        _navigation.UpdatePosition(_player.transform.position);
        _movementDirection = _navigation.GetDirectionTo(target.transform.position);
        Rotate(_movementDirection);
        _controller.Move(_movementDirection * GetMovementSpeed() * Time.deltaTime);
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
    /// 방향대로 회전하기
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