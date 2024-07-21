using System.Collections.Generic;
using UnityEngine;

public class ShopUI : SwitchableUI
{
    [SerializeField] private Transform _scrollContentTransform;
    [SerializeField] private AudioClip _noSound;
    [SerializeField] private AudioClip _purchaseSound;

    private List<ShopButton> _shopButtons = new List<ShopButton>();

    public override void Initialize()
    {
        for(int i = 0; i < _scrollContentTransform.childCount; i++)
        {
            ShopButton shopButton = _scrollContentTransform.GetChild(i).GetComponent<ShopButton>();
            _shopButtons.Add(shopButton);
            shopButton.OnCouldNotPurchase += PlayNoSound;
            shopButton.OnPurchased += PlayPurchaseSound;
        }
    }

    private void PlayNoSound()
    {
        GlobalSoundsPlayer.Instance.PlayOneShot(_noSound);
    }    

    private void PlayPurchaseSound() 
    {
        GlobalSoundsPlayer.Instance.PlayOneShot(_purchaseSound);
    }
}
