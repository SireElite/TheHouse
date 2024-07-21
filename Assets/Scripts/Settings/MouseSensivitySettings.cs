using System;

public class MouseSensivitySettings
{
    public static int CurrentSensivity { get; private set; }

    public static event Action<int> OnCurrentSensivityChanged;

    private static MouseSensivitySlider _slider;

    public static void Initialize(MouseSensivitySlider slider)
    {
        _slider = slider;
        _slider.onValueChanged.AddListener(ChangeSensivity);
        _slider.Initialize();
    }

    public static void SetMouseSensivity(int value)
    {
        _slider.value = value;
    }

    private static void ChangeSensivity(float value)
    {
        CurrentSensivity = (int)_slider.value;
        OnCurrentSensivityChanged?.Invoke(CurrentSensivity);
    }
}
