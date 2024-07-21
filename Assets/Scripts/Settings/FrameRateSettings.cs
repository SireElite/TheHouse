using UnityEngine;
using System;

public static class FrameRateSettings
{
    public static int CurrentFrameRateLimit { get; private set; }

    public static event Action<int> OnFrameRateLimitChanged;

    private static FrameRateSlider _slider;

    public static void Initiallize(FrameRateSlider slider)
    {
        QualitySettings.vSyncCount = 0;
        _slider = slider;
        _slider.Initialize();
        ChangeFPSLimit(_slider.value);
        _slider.onValueChanged.AddListener(ChangeFPSLimit);
    }

    public static void SetFPSLimit(float value)
    {
        _slider.value = value;
    }

    private static void ChangeFPSLimit(float value)
    {
        CurrentFrameRateLimit = (int)value;
        Application.targetFrameRate = CurrentFrameRateLimit;
        OnFrameRateLimitChanged?.Invoke(CurrentFrameRateLimit);
    }
}
