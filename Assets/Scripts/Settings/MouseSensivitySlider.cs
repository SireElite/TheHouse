using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensivitySlider : Slider
{
    [SerializeField] private TextMeshProUGUI _valueTMP;

    public void Initialize()
    {
        onValueChanged.AddListener(ChangeValueTMP);
        value = (minValue + maxValue) / 2;
    }

    private void ChangeValueTMP(float value)
    {
        float valuePercentage = ((value - minValue) / (maxValue - minValue)) * 100f;
        _valueTMP.text = $"{Mathf.Round(valuePercentage)}%";
    }
}
