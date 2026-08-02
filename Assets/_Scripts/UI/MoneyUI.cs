using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    TextMeshProUGUI _moneyText;
    private void Start()
    {
        _moneyText = GetComponent<TextMeshProUGUI>();
        ShopManager.OnMoneyChanged += ShopManager_OnMoneyChanged;
    }

    private void ShopManager_OnMoneyChanged(object sender, System.EventArgs e)
    {
        _moneyText.text = ShopManager.Instance.GetCurrentAmount().ToString();   
    }
}
