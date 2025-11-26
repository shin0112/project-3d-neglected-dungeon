using UnityEngine;

public class PlayerAnimationEventRelay : MonoBehaviour
{
    public Player Player { get; set; }

    public void OnAttackHit()
    {
        Player?.OnAttackHit();
    }
}
