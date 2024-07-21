using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : SwitchableUI
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    public static Menu Instance { get; private set; }

    public static event Action OnMenuOpened;

    public override void Initialize()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception($"Have more than 1 {name} Instance");
        }
        #endregion

        _settingsButton.onClick.AddListener(OpenSettings);
        _resumeButton.onClick.AddListener(CloseMenu);
        _exitButton.onClick.AddListener(ExitGame);
    }

    private void OnEnable()
    {
        OnMenuOpened?.Invoke();
    }

    private void CloseMenu()
    {
        OnShouldBeClosed?.Invoke(this);
    }

    private void OpenSettings()
    {
        OnShouldBeOpened?.Invoke(Settings.Instance);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
