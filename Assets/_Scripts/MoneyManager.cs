using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public static event EventHandler OnMoneyChanged;



    int _currentMoney = 3000;

    private void Awake()
    {
        Instance = this;
    }


    public void AddMoney(int addAmount)
    {
        _currentMoney += addAmount;
        OnMoneyChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetCurrentAmount()
    {
        return _currentMoney;
    }
    public MoneyData SaveMoneyAmount()
    {
        return new MoneyData(_currentMoney);
    }
    public void LoadMoneyAmount(MoneyData data)
    {
        _currentMoney = data.amount;
        OnMoneyChanged?.Invoke(this, EventArgs.Empty);
    }
}
[Serializable]
public class MoneyData
{
    public int amount;

    public MoneyData(int amount)
    {
        this.amount = amount;
    }
}