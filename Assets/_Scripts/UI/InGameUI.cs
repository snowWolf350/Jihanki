using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;

    private void Start()
    {
        ShopManager.OnConfirmBuy += ShopManager_OnConfirmBuy;
    }

    private void ShopManager_OnConfirmBuy(object sender, System.EventArgs e)
    {
        _moneyText.text = ShopManager.Instance.GetCurrentAmount().ToString();
    }
}
