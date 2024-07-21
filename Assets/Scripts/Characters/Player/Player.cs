using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _handTransform;
    [SerializeField] private CameraController _playerCameraController;

    public static Player Instance;

    public static event Action OnLogicBlocked;
    public static event Action OnLogicUnblocked;

    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception($"More than one {name} Instances");
        }
        #endregion
        StateMachine = new StateMachine();
    }

    private void OnEnable()
    {
        CutscenePlayer.Instance.OnPlayed += BlockLogic;
        CutscenePlayer.Instance.OnStopped += UnblockLogic;
    }

    private void OnDisable()
    {
        CutscenePlayer.Instance.OnPlayed -= BlockLogic;
        CutscenePlayer.Instance.OnStopped -= UnblockLogic;
    }

    public Transform GetHandTransform()
    {
        return _handTransform;
    }

    public void BlockLogic()
    {
        OnLogicBlocked?.Invoke();
        PlayerController.Instance.DisableInputActions();
        _playerCameraController.enabled = false;
        _handTransform.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnblockLogic()
    {
        OnLogicUnblocked?.Invoke();
        PlayerController.Instance.EnableInputActions();
        _playerCameraController.enabled = true;
        _handTransform.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
