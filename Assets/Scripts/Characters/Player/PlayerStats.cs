using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money { get; private set; }
    public static int MoneyMultiplier { get; private set; } = 1;

    public static Action<int> OnMoneyValueChanged;

    private PlayerStatsSaveSystem _saveSystem = new PlayerStatsSaveSystem();

    private void Awake()
    {
        _saveSystem.Initialize();
    }

    private void Start()
    {
        AddMoney(_saveSystem.LoadMoney());
    }

    public static void AddMoney(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException("Negative amount");

        Money += amount;
        OnMoneyValueChanged?.Invoke(Money);
    }

    public static void SubtractMoney(int amount)
    {
        if(amount < 0 && Money - amount < 0)
            throw new ArgumentOutOfRangeException();

        Money -= amount;

        OnMoneyValueChanged?.Invoke(Money);
    }

    public static void SetMoneyMultiplier(int value)
    {
        if (value < 1)
            throw new ArgumentOutOfRangeException();

        MoneyMultiplier = value;
    }
}
