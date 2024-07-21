using System;
using UnityEngine;

public class ATMBasicButton : MonoBehaviour, IClickable
{
    public event Action OnPressed;
   
    public virtual void HandleClick()
    {
        OnPressed?.Invoke();
    }
}
