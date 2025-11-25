using System;
using UnityEngine;

[Serializable]
public class PlayerController
{
    #region Fields
    // Components
    [field: SerializeField] private Player _player;

    // Movement Fields
    [field: SerializeField] public Vector3 MovementDirection { get; set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float MovementSpeedModifier { get; set; } = 1f;
    [field: SerializeField] public float RotationDamping { get; private set; }

    [field: SerializeField] public float JumpForce { get; set; }
    #endregion

    public PlayerController(Player player)
    {
        _player = player;
    }

    #region 움직임 구현
    /// <summary>
    /// [Public] 이동 방향을 받아 이동 및 회전
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector3 direction)
    {
        Rotate(direction);
        _player.Controller.Move(direction * GetMovementSpeed());
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