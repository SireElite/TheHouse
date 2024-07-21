using TMPro;
using UnityEngine;

public class CoinValueCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueTMP;
    [SerializeField] private GameObject _coinValuePanel;

    private void OnEnable()
    {
        PlayerInteractions.OnCoinFound += Enable;
        PlayerInteractions.OnCoinNotFound += Disable;
    }

    private void OnDisable()
    {
        PlayerInteractions.OnCoinFound -= Enable;
        PlayerInteractions.OnCoinNotFound -= Disable;
    }

    private void Enable(int value)
    {
        if(_coinValuePanel.activeSelf)
            return;

        _coinValuePanel.SetActive(true);
        _valueTMP.text = $"{value}$";
    }

    private void Disable()
    {
        if(_coinValuePanel.activeSelf == false)
            return;

        _coinValuePanel.SetActive(false);
    }
}
