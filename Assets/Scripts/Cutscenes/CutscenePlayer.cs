using System;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class CutscenePlayer : MonoBehaviour
{
    public static CutscenePlayer Instance { get; private set; }

    public event Action OnPlayed;
    public event Action OnPaused;
    public event Action OnStopped;

    private PlayableDirector _playableDirector;

    void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception($"Already have a {name} Instance");
        }
        #endregion

        _playableDirector = GetComponent<PlayableDirector>();
        _playableDirector.played += TriggerOnPlayedAction;
        _playableDirector.paused += TriggerOnPausedAction;
        _playableDirector.stopped += TriggerOnStoppedAction;
    }

    public void PlayCutscene(PlayableAsset cutscene)
    {
        _playableDirector.Play(cutscene);
    }

    private void TriggerOnPlayedAction(PlayableDirector director)
    {
        OnPlayed?.Invoke();
    }

    private void TriggerOnPausedAction(PlayableDirector director)
    {
        OnPaused?.Invoke();
    }

    private void TriggerOnStoppedAction(PlayableDirector director)
    {
        OnStopped?.Invoke();
    }
}
