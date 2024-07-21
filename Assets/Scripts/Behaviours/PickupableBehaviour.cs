using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupableBehaviour : ITakeable
{
    public static Action<Item> OnItemShouldBeAddedToInventory;

    private Item _item;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Vector3 _pickUpRotation;

    public PickupableBehaviour(Item item, Vector3 rotation)
    {
        _item = item;
        _rigidbody = item.GetComponent<Rigidbody>();
        _transform = item.transform;
        _pickUpRotation = rotation;
    }

    public PickupableBehaviour(Item item)
    {
        _item = item;
        _rigidbody = item.GetComponent<Rigidbody>();
        _transform = item.transform;
    }

    public void Take()
    {
        if(Inventory.Instance.CanAddItem() == false)
            return;

        OnItemShouldBeAddedToInventory?.Invoke(_item);
        _rigidbody.isKinematic = true;
        _rigidbody.interpolation = RigidbodyInterpolation.None;
        _transform.Rotate(_pickUpRotation);
        _transform.parent = Player.Instance.GetHandTransform();
        _transform.position = _transform.parent.position;
    }

    public void Drop()
    {
        _transform.parent = null;
        _transform.position = PlayerInteractions.Instance.GetDropPoint().position;
        _rigidbody.isKinematic = false;
    }
}
