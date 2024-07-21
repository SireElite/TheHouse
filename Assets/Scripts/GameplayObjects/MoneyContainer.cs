using System;
using TMPro;
using UnityEngine;

public class MoneyContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyAmountTMP;
    [SerializeField] private int _necessaryMoneyAmount;

    public event Action OnMoneyCollected;

    private int _currentMoneyAmount;

    private void OnCollisionEnter(Collision collision)
    {
        Coin coin;

        if(collision.gameObject.TryGetComponent<Coin>(out coin))
        {
            AddMoney(coin);
        }
    }

    private void AddMoney(Coin coin)
    {
        if(coin.Value >= _necessaryMoneyAmount)
        {
            coin.SubtractNominalValue(_necessaryMoneyAmount);
            OnMoneyCollected?.Invoke();
            Destroy(gameObject);
            return;
        }
        else
        {
            _currentMoneyAmount += coin.Value;
            _necessaryMoneyAmount -= coin.Value;
            coin.SubtractNominalValue(coin.Value);
        }

        UpdateMoneyAmountUI();
    }

    private void UpdateMoneyAmountUI()
    {
        _moneyAmountTMP.text = $"${_currentMoneyAmount}";
    }
}
