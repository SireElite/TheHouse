using TMPro;
using UnityEngine;

public class MoneyMonitor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyValueTMP;

    public void ChangeValueUI(int value)
    {
        _moneyValueTMP.text = $"${value}";
    }
}
