using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueTMP;
    [SerializeField] private TextMeshProUGUI _nameTMP;
    [SerializeField] private Button _continueButton;

    private AudioSource _audioSource;
    private DialogueNarrator _dialogueNarrator;

    public void Initialize()
    {
        DialogueNPC.OnDialogueStarted += StartDialogue;
        _audioSource = GetComponent<AudioSource>();
        _dialogueNarrator = new DialogueNarrator(_audioSource, _dialogueTMP, _nameTMP, this);
    }

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(ContinueStory);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(ContinueStory);
    }

    public void StartDialogue(TextAsset inkJSON)
    {
        gameObject.SetActive(true);
        Menu.OnMenuOpened += EndDialogue;

        if(_dialogueNarrator.DialogueIsPlaying == false)
            _dialogueNarrator.EnterDialogueMode(inkJSON);
    }


    private void ContinueStory()
    {
        if(_dialogueNarrator.IsTyping == false)
        {
            if(_dialogueNarrator.CurrentStory.canContinue)
            {
                _dialogueNarrator.ContinueStory();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private void EndDialogue()
    {
        Menu.OnMenuOpened -= EndDialogue;
        _dialogueNarrator.ExitDialogueMode();
        gameObject.SetActive(false);
    }
}
