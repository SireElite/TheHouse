using System;
using UnityEngine;

public class BackgroundMusicTrigger : SceneVisibleTrigger
{
    [SerializeField] private AudioClip _backgroundMusic;

    public static event Action<AudioClip> OnTriggerEntered;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() == true)
            OnTriggerEntered?.Invoke(_backgroundMusic);
    }
}
