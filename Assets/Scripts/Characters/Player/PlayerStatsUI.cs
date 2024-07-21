using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;

    private void OnEnable()
    {
        PlayerStats.OnMoneyValueChanged += UpdateMoneyUI;
    }

    private void OnDisable()
    {
        PlayerStats.OnMoneyValueChanged -= UpdateMoneyUI;
    }

    private void UpdateMoneyUI(int value)
    {
        _moneyText.text = $"${value}";
    }
}
