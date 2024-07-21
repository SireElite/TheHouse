using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        BackgroundMusicTrigger.OnTriggerEntered += Set;
    }

    private void Set(AudioClip clip)
    {
        if(_audioSource.clip == clip)
            return;

        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
