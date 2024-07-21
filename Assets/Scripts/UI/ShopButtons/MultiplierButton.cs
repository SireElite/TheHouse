using TMPro;
using UnityEngine;

public class MultiplierButton : ShopButton
{
    [SerializeField] private int _moneyMultiplier;
    [SerializeField] private TextMeshProUGUI _multiplierTMP;

    private void Start()
    {
        _multiplierTMP.text = $"x{_moneyMultiplier}";
    }

    public override void TryToPurchase()
    {
        base.TryToPurchase();

        if(_isPurchased)
            PlayerStats.SetMoneyMultiplier(_moneyMultiplier);
    }
}
