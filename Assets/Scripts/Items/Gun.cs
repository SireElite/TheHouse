using UnityEngine;

[SelectionBase, RequireComponent(typeof(AudioSource))]
public class Gun : Item, IUsable
{
    [SerializeField] private GunSO _gunSO;
    [SerializeField] private GameObject _impactEffectPrefab;
    [SerializeField] private AudioClip _shootSound;

    private const int AntiPlayerBitmask = ~(1 << 7);

    private Transform _cameraTransform;
    private float _fireDelay;

    public void Use()
    {
        Shoot();
    }

    protected override void InitTakeable()
    {
        _takeable = new PickupableBehaviour(this);
    }

    private new void Awake()
    {
        base.Awake();
        InitTakeable();
        _cameraTransform = Camera.main.transform;
    }
  
    private void Shoot()
    {
            if(Time.time >= _fireDelay)
            {
            _fireDelay = Time.time + 1f / _gunSO.FireRate;

            RaycastHit hitInfo;

            GlobalSoundsPlayer.Instance.PlayOneShot(_shootSound);

            for (int i = 0; i < _gunSO.BulletsPerShoot; i++)
            {
                Vector3 bulletDirection = GetShootDirection();

                if (Physics.Raycast(_cameraTransform.position, bulletDirection, out hitInfo, _gunSO.Range, AntiPlayerBitmask))
                {
                    IDamagable damageable;

                    if (hitInfo.collider.TryGetComponent<IDamagable>(out damageable))
                    {
                        damageable.SpawnBloodParticles(hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        damageable.TakeDamage(_gunSO.Damage);
                    }
                    else
                    {
                        PoolManager.Instance.Spawn(_impactEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    }
                }
            }
        }
    }


    private Vector3 GetShootDirection()
    {
        float randomX = Random.Range(-_gunSO.Spread, _gunSO.Spread);
        float randomY = Random.Range(-_gunSO.Spread, _gunSO.Spread);
        float randomZ = Random.Range(-_gunSO.Spread, _gunSO.Spread);

        Vector3 targetPos = _cameraTransform.position + _cameraTransform.forward * _gunSO.Range;
        targetPos = new Vector3(targetPos.x + randomX, targetPos.y + randomY, targetPos.z + randomZ);

        Vector3 direction = targetPos - _cameraTransform.position;
        return direction.normalized;
    }

    public override void Drop()
    {
        base.Drop();
        transform.rotation = Quaternion.LookRotation(_cameraTransform.right, Vector3.up);
    }
}
