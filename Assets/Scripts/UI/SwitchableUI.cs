using UnityEngine;
using System;

public abstract class SwitchableUI : MonoBehaviour
{
    public static Action<SwitchableUI> OnShouldBeOpened;
    public static Action<SwitchableUI> OnShouldBeClosed;

    public abstract void Initialize();

    public void EnableSelf()
    {
        gameObject.SetActive(true);
        Player.Instance.BlockLogic();
    }

    public void DisableSelf()
    {
        Player.Instance.UnblockLogic();
        gameObject.SetActive(false);
    }
}
