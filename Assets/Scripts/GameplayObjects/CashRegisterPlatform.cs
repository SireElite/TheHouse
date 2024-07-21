using UnityEngine;

public abstract class CashRegisterPlatform : MonoBehaviour
{
    [SerializeField] protected MoneyMonitor _moneyMonitor;

    public int CurrentValue { get; private set; }

    protected void AddValue(int value)
    {
        if(value < 0)
            throw new System.ArgumentOutOfRangeException("Adding negative value");

        CurrentValue += value;
        _moneyMonitor.ChangeValueUI(CurrentValue);
    }

    public void ReduceValue(int value)
    {
        if(CurrentValue - value < 0)
            throw new System.ArgumentOutOfRangeException();

        CurrentValue -= value;

        _moneyMonitor.ChangeValueUI(CurrentValue);
    }
}
