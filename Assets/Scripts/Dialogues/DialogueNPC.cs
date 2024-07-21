using System;
using UnityEngine;

public class DialogueNPC : InteractableWithUIBehavior
{
    [SerializeField] protected TextAsset _inkJSON;

    public static event Action<TextAsset> OnDialogueStarted;

    public override void Interact()
    {
        StartDialogue(_inkJSON);
    }

    protected void StartDialogue(TextAsset textAsset)
    {
        OnDialogueStarted?.Invoke(textAsset);
    }
}
