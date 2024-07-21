using System.Collections;
using UnityEngine;

public class Tycoon : MonoBehaviour
{
    [SerializeField] private Transform _coinSpawnPoint;
    [SerializeField, Min(0.3f)] private float _coinSpawnTime; 
    [SerializeField, Min(1)] private int _coinValue;

    private void Start()
    {
        StartCoroutine(SpawnMoney());
    }

    IEnumerator SpawnMoney()
    {
        while(true)
        {
            yield return new WaitForSeconds(_coinSpawnTime);
            CoinSpawner.Instance.Spawn(_coinValue, _coinSpawnPoint.position, 1, false, true);
        }
    }
}
