using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VolumeSlider : Slider
{
    [SerializeField] private TextMeshProUGUI _valueTMP;

    protected const int MaxVolume = 0;
    protected const int MinVolume = -80;

    private float _startVolume = -24;

    public void Initialize()
    {
        minValue = MinVolume;
        maxValue = MaxVolume;
        value = _startVolume;
        ChangeValueTMP(value);
        onValueChanged.AddListener(ChangeValueTMP);
    }

    protected void ChangeValueTMP(float sliderValue)
    {
        float valuePercentage = ((sliderValue - (MinVolume)) / (MaxVolume - MinVolume)) * 100f;
        _valueTMP.text = $"{Mathf.Round(valuePercentage)}%";
    }
}
