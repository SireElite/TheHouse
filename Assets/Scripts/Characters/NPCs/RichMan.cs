using UnityEngine;

public class RichMan : DialogueNPC
{
    [SerializeField] private TextAsset _haveInteractedSpeech;

    private bool _haveInteracted;

    public override void Interact()
    {
        if(_haveInteracted)
        { 
            StartDialogue(_haveInteractedSpeech);
        }
        else
        {
            _haveInteracted = true; 
            base.Interact();
        }

        DisableInteractUI();
    }
}
