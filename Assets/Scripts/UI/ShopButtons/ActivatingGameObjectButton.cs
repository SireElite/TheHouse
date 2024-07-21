using UnityEngine;

public class ActivatingGameObjectButton : ShopButton
{
    [SerializeField] private GameObject _objectToActivate;

    public override void TryToPurchase()
    {
        base.TryToPurchase();

        if(_isPurchased)
            _objectToActivate.SetActive(true);
    }   
}
