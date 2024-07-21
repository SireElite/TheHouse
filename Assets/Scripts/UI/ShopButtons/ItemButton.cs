using UnityEngine;

public class ItemButton : ShopButton
{
    [SerializeField] private ItemSO _itemSO;

    public override void TryToPurchase()
    {
        base.TryToPurchase();

        if(_isPurchased)
            GiveItem();
    }

    private void GiveItem()
    {
        GameObject itemGO = Instantiate(_itemSO.Prefab, Player.Instance.GetHandTransform().position, Quaternion.identity);
        Item item = itemGO.GetComponent<Item>();
        item.Take();
    }
}
