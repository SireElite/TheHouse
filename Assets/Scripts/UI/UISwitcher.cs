using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UISwitcher : MonoBehaviour
{
    private static List<SwitchableUI> _openCloseUIArray = new List<SwitchableUI>();

    private PlayerInputActions _playerInputActions;
    private static SwitchableUI _openedUI;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        for(int i = 0; i < transform.childCount; i++)
        {
            SwitchableUI openCLoseUI = transform.GetChild(i).GetComponent<SwitchableUI>();
            _openCloseUIArray.Add(openCLoseUI);
        }
    }

    private void Start()
    {
        foreach(var UI in _openCloseUIArray)
        {
            UI.Initialize();
        }
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Esc.started += HandleEscButton;
        SwitchableUI.OnShouldBeOpened += EnableUI;
        SwitchableUI.OnShouldBeClosed += DisableUI;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Esc.started -= HandleEscButton;
        SwitchableUI.OnShouldBeOpened -= EnableUI;
        SwitchableUI.OnShouldBeClosed -= DisableUI;
    }

    private void HandleEscButton(InputAction.CallbackContext context)
    {
        if(_openedUI != null)
        {
            DisableUI(_openedUI);
            return;
        }

        ToggleUI(Menu.Instance);
    }

    public void ToggleUI(SwitchableUI ui)
    {
        if(ui.isActiveAndEnabled)
        {
            DisableUI(ui);
        }   
        else
        {
            EnableUI(ui);
        }
    }

    public static void EnableUI(SwitchableUI ui)
    {
        if(_openedUI != null)
            DisableUI(_openedUI);

        ui.EnableSelf();
        _openedUI = ui;
    }

    public static void DisableUI(SwitchableUI ui)
    {
        ui.DisableSelf();
        _openedUI = null;
    }
}
