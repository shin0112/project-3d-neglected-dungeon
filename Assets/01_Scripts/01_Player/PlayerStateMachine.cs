using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    // Movement Fields
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public float RotationDamping { get; private set; }

    public float JumpForce { get; set; }

    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        this.MainCameraTransform = Camera.main.transform;

        this.MovementSpeed = player.PlayerState.GroundData.BaseSpeed;
        this.RotationDamping = player.PlayerState.GroundData.BaseRotationDamping;
    }
}
