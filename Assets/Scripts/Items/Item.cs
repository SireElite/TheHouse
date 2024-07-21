using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public abstract class Item : MonoBehaviour
{
    [SerializeField] protected ItemSO _itemSO;

    public ItemSO ItemSO => _itemSO;

    protected Collider _collider;
    protected Rigidbody _rigidbody;
    protected ITakeable _takeable;

    protected abstract void InitTakeable();

    protected virtual void Awake()
    {
        GetComponents();
        InitTakeable();
    }

    protected void GetComponents()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Take()
    {
        _takeable.Take();
    }

    public virtual void Drop()
    {
        _takeable.Drop();
    }
}
