using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _cachedTransform;
    private float _xRotation;

    private void Awake()
    {
        _cachedTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        _xRotation -= GameInput.GetMouseAxis(Axis.Y) * MouseSensivitySettings.CurrentSensivity * Time.deltaTime;    
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _cachedTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}
