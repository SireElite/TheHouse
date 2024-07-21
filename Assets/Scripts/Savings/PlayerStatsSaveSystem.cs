using UnityEngine;

public class PlayerStatsSaveSystem
{
    private const string MONEY_KEY = "m_money";
    private const int START_MONEY = 50;
    
    public void Initialize()
    {
        PlayerStats.OnMoneyValueChanged += SaveMoney;
    }

    public void SaveMoney(int value)
    {
        PlayerPrefs.SetInt(MONEY_KEY, value);
        PlayerPrefs.Save();
    }

    public int LoadMoney()
    {
        if(PlayerPrefs.HasKey(MONEY_KEY) == false)
            return START_MONEY;

        int value = PlayerPrefs.GetInt(MONEY_KEY);
        return value;
    }
}
