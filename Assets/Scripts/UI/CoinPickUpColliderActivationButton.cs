using UnityEngine;

public class CoinPickUpColliderActivationButton : ShopButton
{
    [SerializeField] private GameObject _coinPickUpObject;

    public override void TryToPurchase()
    {
        base.TryToPurchase();

        if(_isPurchased)
            _coinPickUpObject.AddComponent<CoinPickUpCollider>();
    }
}
