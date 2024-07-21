using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CashRegister : MonoBehaviour
{
    [SerializeField] private MoneyPlatform _moneyPlatform;
    [SerializeField] private ProductPlatform _productPlatform;

    private AudioSource _audioSource;

    private void Awake()
    {
         _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _productPlatform.OnItemPlaced += TryToPurchase;
        _moneyPlatform.OnCoinPlaced += TryToPurchase;
    }

    private void OnDisable()
    {
        _productPlatform.OnItemPlaced -= TryToPurchase;
        _moneyPlatform.OnCoinPlaced -= TryToPurchase;
    }

    private void TryToPurchase()
    {
        if (_moneyPlatform.CurrentValue >= _productPlatform.CurrentValue && _productPlatform.CurrentValue != 0)
        {
            foreach (StoreItem storeItem in _productPlatform.PlacedItems)
            {
                if (storeItem.IsPurchased == false)
                {
                    _productPlatform.ReduceValue(storeItem.Value);
                    _moneyPlatform.TakeMoney(storeItem.Value);
                    storeItem.Purchase();
                    _audioSource.Play();
                }
            }
        }
    }
}
