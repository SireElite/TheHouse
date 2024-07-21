using UnityEngine;

public static class SettingsSaveSystem
{
    private const string MUSIC_VOLUME_KEY = "m_musicVolume";
    private const string SOUND_EFFECTS_VOLUME_KEY = "m_soundEffectsVolume";
    private const string FRAME_RATE_LIMIT_KEY = "m_limit";
    private const string SCREEN_RESOLUTION_INDEX_KEY = "m_screenResolutionIndex";
    private const string MOUSE_SENSIVITY_KEY = "m_mouseSensivity";
    private const string HAS_SAVED_DATA_KEY = "m_hasData";

    private static bool HasSavedData;

    public static void Initialize()
    {
        AudioSettings.OnMusicVolumeChanged += SaveMusicVolume;
        AudioSettings.OnSoundEffectsVolumeChanged += SaveSoundEffectsVolume;
        FrameRateSettings.OnFrameRateLimitChanged += SaveFrameRateLimit;
        ScreenResolutionSettings.OnNewResolutionSet += SaveScreenResolutionIndex;
        MouseSensivitySettings.OnCurrentSensivityChanged += SaveMouseSensivity;

        HasSavedData = PlayerPrefs.GetInt(HAS_SAVED_DATA_KEY)  == 1 ? true : false;

        if(HasSavedData == false)
        {
            SaveCurrentValues();
            PlayerPrefs.SetInt(HAS_SAVED_DATA_KEY, 1);
            PlayerPrefs.Save();
        }
        else
        {
            LoadAudioSettings();
            LoadFrameRateLimitSettings();
            LoadScreenResolution();
            LoadMouseSensivity();
        }
    }

    private static void SaveCurrentValues()
    {
        SaveMusicVolume(AudioSettings.CurrentMusicVolume);
        SaveSoundEffectsVolume(AudioSettings.CurrentSoundEffectsVolume);
        SaveFrameRateLimit(FrameRateSettings.CurrentFrameRateLimit);
        SaveMouseSensivity(MouseSensivitySettings.CurrentSensivity);
        SaveScreenResolutionIndex(ScreenResolutionSettings.CurrentResolutionIndex);
    }

    private static void SaveScreenResolutionIndex(int index)
    {
        PlayerPrefs.SetInt(SCREEN_RESOLUTION_INDEX_KEY, index);
        PlayerPrefs.Save();
    }

    private static void SaveMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value);
        PlayerPrefs.Save();
    }

    private static void SaveSoundEffectsVolume(float value)
    {
        PlayerPrefs.SetFloat(SOUND_EFFECTS_VOLUME_KEY, value);
        PlayerPrefs.Save();
    }

    private static void SaveFrameRateLimit(int value)
    {
        PlayerPrefs.SetInt(FRAME_RATE_LIMIT_KEY, value);
        PlayerPrefs.Save();
    }

    private static void SaveMouseSensivity(int value)
    {
        PlayerPrefs.SetInt(MOUSE_SENSIVITY_KEY, value);
        PlayerPrefs.Save();
    }

    public static void LoadScreenResolution()
    {
        int index = PlayerPrefs.GetInt(SCREEN_RESOLUTION_INDEX_KEY);
        ScreenResolutionSettings.SetScreenResolution(index);
    }

    public static void LoadFrameRateLimitSettings()
    {
        int limit = PlayerPrefs.GetInt(FRAME_RATE_LIMIT_KEY);
        FrameRateSettings.SetFPSLimit(limit);
    }

    private static void LoadAudioSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
        float soundEffectsVolume = PlayerPrefs.GetFloat(SOUND_EFFECTS_VOLUME_KEY);
        AudioSettings.SetValues(musicVolume, soundEffectsVolume);
    }

    private static void LoadMouseSensivity()
    {
        int sensivity = PlayerPrefs.GetInt(MOUSE_SENSIVITY_KEY);
        MouseSensivitySettings.SetMouseSensivity(sensivity);
    }
}
