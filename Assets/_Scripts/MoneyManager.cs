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

}
