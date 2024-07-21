using UnityEngine;

[SelectionBase]
public class TV : InteractableWithUIBehavior
{
    [SerializeField] private SwitchableUI _shopUI;

    public override void Interact()
    {
         UISwitcher.EnableUI(_shopUI);
    }
}
