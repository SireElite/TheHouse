using TMPro;
using UnityEngine;

public class NameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameTMP;

    private Transform _cameraTransform;
    private Transform _cachedTransform;

    private void Awake()
    {
        _nameTMP.text = transform.parent.gameObject.name.ToString();
        _cameraTransform = Camera.main.transform;
        _cachedTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        _cachedTransform.LookAt(_cameraTransform.position);
    }
}
