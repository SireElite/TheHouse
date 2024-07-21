using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : SwitchableUI
{
    [Header("FullscreenSettings")]
    [SerializeField] private Toggle _fullscreenToggle;

    [Header("AudioSettigs")]
    [SerializeField] private VolumeSlider _musicVolumeSlider;
    [SerializeField] private VolumeSlider _soundEffectsVolumeSlider;
    [SerializeField] private AudioMixer _mainAudioMixer;

    [Header("FrameRateSettings")]
    [SerializeField] private FrameRateSlider _frameRateLimitSlider;

    [Header("ScreenResolutionSettings")]
    [SerializeField] private ScreenResolutionDropdown _screenResolutionDropdown;

    [Header("MouseSensivitySettings")]
    [SerializeField] private MouseSensivitySlider _mouseSensivitySlider;

    public static Settings Instance;

    public override void Initialize()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception($"More than one {name} Instances");
        }
        #endregion

        AudioSettings.Initialize(_mainAudioMixer, _musicVolumeSlider, _soundEffectsVolumeSlider);
        FrameRateSettings.Initiallize(_frameRateLimitSlider);
        FullscreenSettings.Initialize(_fullscreenToggle);
        ScreenResolutionSettings.Initialize(_screenResolutionDropdown);
        MouseSensivitySettings.Initialize(_mouseSensivitySlider);
        SettingsSaveSystem.Initialize();
    }
}
