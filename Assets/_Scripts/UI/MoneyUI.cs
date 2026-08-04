using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    TextMeshProUGUI _moneyText;
    private void Start()
    {
        _moneyText = GetComponent<TextMeshProUGUI>();
        MoneyManager.OnMoneyChanged += MoneyManager_OnMoneyChanged;
    }

    private void MoneyManager_OnMoneyChanged(object sender, System.EventArgs e)
    {
        _moneyText.text = "¥ " + MoneyManager.Instance.GetCurrentAmount().ToString();   
    }
}
