using UnityEngine;
using System;
using TMPro;

[RequireComponent(typeof(MeshRenderer))]
public class ValuePerSpawnButton : MonoBehaviour, IClickable
{
    [SerializeField, Range(1, 1000)] private int _value;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Material _unselectedMaterial;
    [SerializeField] private Material _selectedMaterial;

    public int Value => _value;

    public event Action<ValuePerSpawnButton> OnPressed;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _buttonText.text = $"${_value}";
    }

    public void HandleClick()
    {
        OnPressed?.Invoke(this);
    }

    public void Select()
    {
        _meshRenderer.material = _selectedMaterial;
    }

    public void Deselect()
    {
        _meshRenderer.material = _unselectedMaterial;
    }
}
