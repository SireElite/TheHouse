using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GlobalSoundsPlayer : MonoBehaviour
{
    public static GlobalSoundsPlayer Instance { get; private set; }

    private AudioSource _audioSource;

    private void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception($"Have more than 1 {name} Instance");
        }
        #endregion

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip, float volume = 1)
    {
        _audioSource.PlayOneShot(clip, volume);
    }
}
