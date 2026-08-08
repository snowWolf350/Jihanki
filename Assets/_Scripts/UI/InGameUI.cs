using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] TextMeshProUGUI _dayText;

    private void Start()
    {
        ShopManager.OnConfirmBuy += ShopManager_OnConfirmBuy;
        GameManager.OnDayChanged += GameManager_OnDayChanged;
    }


    private void OnDestroy()
    {
        ShopManager.OnConfirmBuy -= ShopManager_OnConfirmBuy;
    }

    private void ShopManager_OnConfirmBuy(object sender, System.EventArgs e)
    {
        _moneyText.text = "¥ " + MoneyManager.Instance.GetCurrentAmount().ToString();
    }

    private void GameManager_OnDayChanged(object sender, System.EventArgs e)
    {
        _dayText.text = "0" + GameManager.Instance.GetDaysPassed().ToString();
    }
}
