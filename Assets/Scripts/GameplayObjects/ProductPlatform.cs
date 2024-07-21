using System;
using System.Collections.Generic;
using UnityEngine;

public class ProductPlatform : CashRegisterPlatform
{
    public List<StoreItem> PlacedItems { get; private set; } = new List<StoreItem>();

    public Action OnItemPlaced;

    private void OnTriggerEnter(Collider other)
    {
        StoreItem storeItem;

        if (other.TryGetComponent<StoreItem>(out storeItem))
        {
            AddValue(storeItem.Value);
            PlacedItems.Add(storeItem);
        }

        OnItemPlaced?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        StoreItem storeItem;

        if (other.TryGetComponent<StoreItem>(out storeItem))
        {
            ReduceValue(storeItem.Value);
            PlacedItems.Remove(storeItem);
        }
    }
}
