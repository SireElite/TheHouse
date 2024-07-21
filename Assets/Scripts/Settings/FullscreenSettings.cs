using UnityEngine;
using UnityEngine.UI;

public static class FullscreenSettings
{
    public static Toggle Toggle { get; private set; }

    public static void Initialize(Toggle toggle)
    {
        Toggle = toggle;
        Toggle.onValueChanged.AddListener(SetFullscreen);
    }

    public static void SetFullscreen(bool isOn)
    {
        Screen.fullScreen = isOn;
    }
}
