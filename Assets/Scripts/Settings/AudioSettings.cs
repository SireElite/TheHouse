using UnityEngine.Audio;
using System;

public static class AudioSettings
{
    public static float CurrentMusicVolume { get; private set; }
    public static float CurrentSoundEffectsVolume { get; private set; }

    public static Action<float> OnMusicVolumeChanged;
    public static Action<float> OnSoundEffectsVolumeChanged;

    private const string MUSIC_VOLUME = "MusicVolume";
    private const string SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    private static AudioMixer _mainAudioMixer;
    private static VolumeSlider _musicSlider;
    private static VolumeSlider _soundEffectsSlider;

    public static void Initialize(AudioMixer mainMixer, VolumeSlider musicSlider, VolumeSlider soundEffectsSlider)
    {
        _mainAudioMixer = mainMixer;
        _musicSlider = musicSlider;
        _soundEffectsSlider = soundEffectsSlider;

        _soundEffectsSlider.onValueChanged.AddListener(ChangeSoundEffectsVolume);
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);

        _musicSlider.Initialize();
        _soundEffectsSlider.Initialize();
    }

    public static void SetValues(float musicVolume, float soundEffectsVolume)
    {
        CurrentMusicVolume = musicVolume;
        _musicSlider.value = CurrentMusicVolume;

        CurrentSoundEffectsVolume = soundEffectsVolume;
        _soundEffectsSlider.value = CurrentSoundEffectsVolume;
    }

    public static void ChangeMusicVolume(float value)
    {
        _mainAudioMixer.SetFloat(MUSIC_VOLUME, value);
        CurrentMusicVolume = value;
        OnMusicVolumeChanged?.Invoke(CurrentMusicVolume);
    }

    public static void ChangeSoundEffectsVolume(float value)
    {
        _mainAudioMixer.SetFloat(SOUND_EFFECTS_VOLUME, value);
        CurrentSoundEffectsVolume = value;
        OnSoundEffectsVolumeChanged?.Invoke(CurrentSoundEffectsVolume);
    }
}
