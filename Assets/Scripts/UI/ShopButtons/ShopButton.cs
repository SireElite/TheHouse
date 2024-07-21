using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[RequireComponent(typeof(AudioSource), typeof(Button))]
public abstract class ShopButton : MonoBehaviour
{
    [SerializeField] protected int _cost;
    [SerializeField] private TextMeshProUGUI _boughtTMP;
    [SerializeField] private TextMeshProUGUI _costTMP;

    public event Action OnPurchased;
    public event Action OnCouldNotPurchase;

    protected Button _button;
    protected bool _isPurchased;

    private void Awake()
    {
        _costTMP.text = $"${_cost}";
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TryToPurchase);
    }

    public virtual void TryToPurchase()
    {
        if(PlayerStats.Money >= _cost)
        {
            PlayerStats.SubtractMoney(_cost);

            OnPurchased?.Invoke();

            _button.interactable = false;
            _isPurchased = true;
            _boughtTMP.gameObject.SetActive(true);
        }
        else
        {
            OnCouldNotPurchase?.Invoke();
        }
    }
}
