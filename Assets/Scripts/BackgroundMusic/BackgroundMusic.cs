using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        CutscenePlayer.Instance.OnPlayed += Pause;
        CutscenePlayer.Instance.OnStopped += Unpause;
    }

    private void OnDisable()
    {
        CutscenePlayer.Instance.OnPlayed -= Pause;
        CutscenePlayer.Instance.OnStopped -= Unpause;
    }

    private void Pause()
    {
        _audioSource.Pause();
    }

    private void Unpause()
    {
        _audioSource.UnPause();
    }
}
