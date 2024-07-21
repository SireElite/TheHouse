using System;
using UnityEngine;

public class UpdateCaller : MonoBehaviour
{
    public static UpdateCaller Instance { get; private set; }

    public static event Action OnUpdate;
    public static event Action OnFixedUpdate;

    private void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception($"More than one Instance of {name}");
        }
        #endregion
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }
}
