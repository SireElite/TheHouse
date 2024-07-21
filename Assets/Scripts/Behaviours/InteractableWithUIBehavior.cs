using UnityEngine;

public abstract class InteractableWithUIBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] protected GameObject _interactUI;

    public abstract void Interact();
   
    public void EnableInteractUI()
    {
        _interactUI.SetActive(true);
    }
    
    public void DisableInteractUI()
    {
        _interactUI.SetActive(false);
    }
}
