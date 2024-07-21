using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(BoxCollider), typeof(NPCController))]
public class NPC : MonoBehaviour, IDamagable
{
    [field: SerializeField] public GameObject NameUI { get; private set; }
    [field: SerializeField] public GameObject GFX { get; private set; }
    [field: SerializeField] public Transform MoneyDropPoint { get; private set; }
    [field: SerializeField] public NPC_SO NPC_SO { get; private set; }

    [SerializeField, Min(1)] private float _reviveTime;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _dieSound;

    public float CurrentHealth { get; set; }
    public BoxCollider BoxCollider { get; private set; }
    public NPCController Controller { get; private set; }
    public bool IsAlive { get; set; } = true;

    private AudioSource _audioSource;
    private StateMachine _stateMachine;

    private void Awake()
    {
        Controller = GetComponent<NPCController>();
        BoxCollider = GetComponent<BoxCollider>();
        _audioSource = GetComponent<AudioSource>();
        _stateMachine = new StateMachine();
        CurrentHealth = NPC_SO.MaxHealth;
    }

    public void Die()
    {
        _audioSource.PlayOneShot(_dieSound);
        _stateMachine.ChangeState(new DeadState(this));
    }

    public void TakeDamage(float amount)
    {
        if (amount < 0)
            throw new System.ArgumentOutOfRangeException("Negative amount of damage");

        _audioSource.clip = _damageSound;
        _audioSource.Play();

        CurrentHealth -= amount;

        if (CurrentHealth <= 0f)
            Die();
    }

    public void DropMoney()
    {
        CoinSpawner.Instance.Spawn(NPC_SO.CoinNominalValue, MoneyDropPoint.position, NPC_SO.CoinDropAmount, true, true);
    }

    public void SpawnBloodParticles(Vector3 position, Quaternion rotation)
    {
        PoolManager.Instance.Spawn(NPC_SO.BloodParticlesPrefab, position, rotation);
    }
    
    public IEnumerator Revive()
    {
        yield return new WaitForSeconds(_reviveTime);
        _stateMachine.ChangeState(new AliveState(this));
    }
}
