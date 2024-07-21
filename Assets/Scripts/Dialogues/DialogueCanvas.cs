using UnityEngine;

public class DialogueCanvas : MonoBehaviour
{
    [SerializeField] private DialoguePanel _dialoguePanel;

    private void Awake()
    {
        _dialoguePanel.Initialize();
    }
}
