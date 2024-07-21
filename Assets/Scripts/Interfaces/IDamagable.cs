using UnityEngine;

public interface IDamagable
{
    public bool IsAlive { get; set; }

    public void TakeDamage(float amount);

    public void SpawnBloodParticles(Vector3 position, Quaternion rotation);

    public void Die();
}
