using UnityEngine;

[CreateAssetMenu(fileName = "Gun")]
public class GunSO : ScriptableObject
{
    [SerializeField] private float _damage;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _range;
    [SerializeField] private float _bulletsPerShoot;
    [SerializeField] private float _spread;

    public float Damage => _damage;
    public float FireRate => _fireRate;
    public float Range => _range;
    public float BulletsPerShoot => _bulletsPerShoot;
    public float Spread => _spread;
}
