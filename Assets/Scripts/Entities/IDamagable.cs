using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(float damage);
    public void Heal(float heal);
    public bool IsAlive();
}
