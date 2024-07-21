using UnityEngine;

public class CoinSpawner : MonoBehaviour 
{
    [SerializeField] private Coin _coinPrefab;

    public static CoinSpawner Instance { get; private set; }

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception($"Already have {name} Instance");
        }
        #endregion
    }

    public void Spawn(int nominalValue, Vector3 position, int amount, bool useRandomRotation, bool useMultiplier)
    {
        if(nominalValue <= 0)
            throw new System.ArgumentOutOfRangeException();

        for(int i = 0; i < amount; i++)
        {
            Quaternion rotation;
            rotation = useRandomRotation ? GetRandomRotation() : Quaternion.identity;

            GameObject coinGO = PoolManager.Instance.Spawn(_coinPrefab.gameObject, position, rotation);
            Rigidbody coinRB = coinGO.GetComponent<Rigidbody>();

            coinRB.isKinematic = true;
            coinRB.isKinematic = false;

            Coin coin = coinGO.GetComponent<Coin>();

            if(useMultiplier)
            {
                coin.SetNominalValue(nominalValue * PlayerStats.MoneyMultiplier);
            }
            else
            {
                coin.SetNominalValue(nominalValue);
            }
        }
    }
 
    private Quaternion GetRandomRotation()
    {
        float x = Random.Range(-180f, 180f);
        float y = Random.Range(-180f, 180f);
        float z = Random.Range(-180f, 180f);

        return Quaternion.Euler(x, y, z);
    }
}
