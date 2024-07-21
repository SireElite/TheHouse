using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using System;

public class DialogueNarrator
{
    public Story CurrentStory { get; private set; }
    public bool IsTyping { get; private set; }
    public bool DialogueIsPlaying { get; private set; }

    private const string SPEAKER_TAG = "speaker";

    private AudioSource _audioSource;
    private TextMeshProUGUI _dialogueTMP;
    private TextMeshProUGUI _nameTMP;
    private MonoBehaviour _monoBehaviour;

    public DialogueNarrator(AudioSource audioSource, TextMeshProUGUI dialogueTMP, TextMeshProUGUI nameTMP, MonoBehaviour monoBehaviour)
    {
        _audioSource = audioSource;
        _nameTMP = nameTMP;
        _dialogueTMP = dialogueTMP;
        _monoBehaviour = monoBehaviour;
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        CurrentStory = new Story(inkJSON.text);
        DialogueIsPlaying = true;
        Player.Instance.StateMachine.EnterState(new DialogueState());
        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        _monoBehaviour.StopAllCoroutines();
        DialogueIsPlaying = false;
        _dialogueTMP.text = string.Empty;
        Player.Instance.StateMachine.ExitCurrentState();
    }

    public void ContinueStory()
    {
        string sentece = CurrentStory.Continue();
        _monoBehaviour.StartCoroutine(TypeLetters(sentece));
        HandleTags(CurrentStory.currentTags);
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            if(splitTag.Length != 2)
                throw new System.Exception($"Tag could not be parsed: {tag}");

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    _nameTMP.text = tagValue;
                    break;
            }
        }
    }

    IEnumerator TypeLetters(string sentence)
    {
        _dialogueTMP.text = string.Empty;
        IsTyping = true;

        foreach (Char letter in sentence)
        {
            _audioSource.Play();
            _dialogueTMP.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        IsTyping = false;
    }
}
