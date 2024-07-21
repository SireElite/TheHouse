using UnityEngine;
using System;

public static class ScreenResolutionSettings
{
    public static int CurrentResolutionIndex { get; private set; }

    public static event Action<int> OnNewResolutionSet;

    private static ScreenResolutionDropdown _dropdown;

    public static void Initialize(ScreenResolutionDropdown dropdown)
    {
        _dropdown = dropdown;
        _dropdown.OnNewResolutionSelected += ChangeScreenResolution;
        _dropdown.Initialize();
    }

    public static void SetScreenResolution(int index)
    {
        _dropdown.value = index;
    }

    private static void ChangeScreenResolution(Resolution resolution)
    {
        Screen.SetResolution(resolution.width, resolution.height, FullscreenSettings.Toggle.isOn);
        CurrentResolutionIndex = _dropdown.value;
        OnNewResolutionSet?.Invoke(CurrentResolutionIndex);
    }
}
