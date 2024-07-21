using UnityEngine;

public class DoorButton : ShopButton
{
    [SerializeField] private Door _door;

    public override void TryToPurchase()
    {
        base.TryToPurchase();

        if(_isPurchased)
            _door.Open();
    }
}
