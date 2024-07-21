using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SelfRemovingParticles : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    void OnEnable()
    {
        _particleSystem = GetComponent<ParticleSystem>();

        Invoke(nameof(RemoveSelf), _particleSystem.main.duration);
    }

    private void RemoveSelf()
    {
        PoolManager.Instance.Despawn(gameObject);
    }
}
