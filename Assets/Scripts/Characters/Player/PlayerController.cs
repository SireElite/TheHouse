using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private float _fallingSpeed = -50f;
    [SerializeField] private float _speed;

    public static PlayerController Instance;

    private PlayerInputActions _playerInputActions;
    private Transform _cachedTransform;
    private Vector3 _velocity;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception($"Have more than one {name} Instance");
        }
        #endregion

        _cachedTransform = GetComponent<Transform>();

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    private void Update()
    {
        if(_playerInputActions.Player.Movement.inProgress)
            Move();

        if(_playerInputActions.Player.enabled)
            Rotate();

        Fall();
    }
  
    private void Fall()
    {
        _velocity.y = _fallingSpeed * Time.deltaTime;
        _characterController.Move(_velocity);
    }

    private void Rotate()
    {
        _cachedTransform.Rotate(Vector3.up * GameInput.GetMouseAxis(Axis.X) * MouseSensivitySettings.CurrentSensivity * Time.deltaTime);
    }

    private void Move()
    {
        Vector2 inputVector = GameInput.Instance.GetInputVectorNormalized();
        Vector3 moveDirection = _cachedTransform.right * inputVector.x + _cachedTransform.forward * inputVector.y;
        _characterController.Move(moveDirection * _speed * Time.deltaTime);
    }

    public void EnableInputActions()
    {
        _playerInputActions.Enable();
    }

    public void DisableInputActions()
    {
        _playerInputActions.Disable();
    }
}
