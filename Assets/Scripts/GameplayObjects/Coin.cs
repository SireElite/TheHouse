using UnityEngine;

[SelectionBase]
public class Coin : Item
{
    public int Value { get; private set; }

    protected override void InitTakeable()
    {
        _takeable = new GrabbableBehaviour(_rigidbody, transform);
    }

    public void SetNominalValue(int value)
    {
        Value = value;
    }

    public void PickUp()
    {
        PlayerStats.AddMoney(Value);
        PoolManager.Instance.Despawn(gameObject);
    }

    public void SubtractNominalValue(int value)
    {
        if(Value - value < 0)
            throw new System.ArgumentOutOfRangeException($"{name} has negative nominal value ({Value - value})");

        Value -= value;

        if (Value == 0)
            PoolManager.Instance.Despawn(gameObject);
    }
}
