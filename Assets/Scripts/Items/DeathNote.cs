using System;
using UnityEngine;

[SelectionBase]
public class DeathNote : StoreItem, IUsable
{
    public static event Action OnUse;

    private Vector3 _takenRotation = new Vector3(35f, 0f, 0f);

    public override void Purchase()
    {
        base.Purchase();

        _takeable = new PickupableBehaviour(this, _takenRotation);
    }

    public void Use()
    {
        OnUse?.Invoke();
    }
}
