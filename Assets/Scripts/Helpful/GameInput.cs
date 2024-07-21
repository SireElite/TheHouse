using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception($"More than one {name} instance");
        }
        #endregion
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Movement.Enable();
    }

    public static float GetMouseAxis(Axis axis)
    {
        if(axis == Axis.X)
        {
            float mouseX = Input.GetAxis("Mouse X");
            return mouseX;
        }
        else if(axis == Axis.Y)
        {
            float mouseY = Input.GetAxis("Mouse Y");
            return mouseY;
        }

        throw new System.ArgumentOutOfRangeException("No z axis for mouse");
    }

    public Vector2 GetInputVectorNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
