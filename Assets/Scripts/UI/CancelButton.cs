using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CancelButton : MonoBehaviour
{
    private SwitchableUI _parentCanvas;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _parentCanvas = transform.parent.GetComponent<SwitchableUI>();
        _button.onClick.AddListener(DisableUI);
    }

    public void DisableUI()
    {
       UISwitcher.DisableUI(_parentCanvas);
    }
}
