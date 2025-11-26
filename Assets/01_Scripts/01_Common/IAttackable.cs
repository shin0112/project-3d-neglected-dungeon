using UnityEngine;

public interface IAttackable
{
    public void TakeDamage(float damage);
    public Transform Transform { get; }
}
