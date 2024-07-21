using System;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPlatform : CashRegisterPlatform
{
    private List<Coin> _placedCoins = new List<Coin>();

    public event Action OnCoinPlaced;

    private void OnTriggerEnter(Collider collider)
    {
        Coin coin;

        if(collider.TryGetComponent<Coin>(out coin))
        {
            _placedCoins.Add(coin);
            AddValue(coin.Value);
            OnCoinPlaced?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        Coin coin;

        if(collider.TryGetComponent<Coin>(out coin))
        {
            _placedCoins.Remove(coin);
            ReduceValue(coin.Value);
        }
    }

    public void TakeMoney(int value)
    {
        foreach(Coin coin in _placedCoins)
        {
            if(coin.Value < value)
            {
                value -= coin.Value;
                ReduceValue(coin.Value);
                coin.SubtractNominalValue(coin.Value);
            }
            else
            {
                coin.SubtractNominalValue(value);
                ReduceValue(value);
                value -= value;
                return;
            }
        }
    }
}
