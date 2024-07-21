using System;
using UnityEngine;

public class BrokeMan : DialogueNPC
{
    [SerializeField] private MoneyContainer _moneyContainer;

    public event Action OnMoneyCollected;

    private void OnEnable()
    {
        _moneyContainer.OnMoneyCollected += DestroySelf;
    }

    private void OnDisable()
    {
        _moneyContainer.OnMoneyCollected -= DestroySelf;
    }

    public void DestroySelf()
    {
        OnMoneyCollected?.Invoke();
        Destroy(gameObject);
    }
}
