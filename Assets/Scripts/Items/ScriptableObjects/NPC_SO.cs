using UnityEngine;

[CreateAssetMenu(fileName = "NPC_SO")]
public class NPC_SO : ScriptableObject
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private GameObject _bloodParticlesPrefab;
    [SerializeField] private int _coinNominalValue;
    [SerializeField] private int _coinDropAmount;

    public float MaxHealth => _maxHealth;
    public GameObject BloodParticlesPrefab => _bloodParticlesPrefab;
    public int CoinNominalValue => _coinNominalValue;
    public int CoinDropAmount => _coinDropAmount;
}
