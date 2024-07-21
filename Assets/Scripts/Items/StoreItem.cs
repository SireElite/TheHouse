using UnityEngine;
using System;

public class StoreItem : Item
{
    [SerializeField] protected int _value;

    public static event Action<StoreItem> OnPurchased;

    public bool IsPurchased { get; private set; }

    public int Value => _value;

    protected override void InitTakeable()
    {
        _takeable = new GrabbableBehaviour(_rigidbody, transform);
    }

    public virtual void Purchase()
    {
        _value = 0;
        IsPurchased = true;
        OnPurchased?.Invoke(this);
    }
}
