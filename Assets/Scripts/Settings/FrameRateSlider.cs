using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameRateSlider : Slider
{
    [SerializeField] private TextMeshProUGUI _sliderValueTMP;

    public void Initialize()
    {
        value = maxValue;
        ChangeValueTMP(value);
        onValueChanged.AddListener(ChangeValueTMP);
    }
 
    public void ChangeValueTMP(float value)
    {
        _sliderValueTMP.text = value.ToString();
    }
}
