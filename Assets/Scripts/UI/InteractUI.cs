using UnityEngine;

public class InteractUI : MonoBehaviour
{
    private Transform _cameraTransform;
    private Transform _cachedTransform;

    private void Awake()
    {
        _cameraTransform = Camera.main.GetComponent<Transform>();
        _cachedTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        _cachedTransform.LookAt(_cameraTransform);
    }
}
