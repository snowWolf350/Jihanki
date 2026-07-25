using UnityEngine;
using UnityEngine.UI;

public class OrderTemplateUI : MonoBehaviour
{
    [SerializeField] Button _buyButton;

    PartsSO _partsSO;

    private void Awake()
    {
        _buyButton.onClick.AddListener(() =>
        {
            ShopUI.Instance.BuyPart(_partsSO);
        });
    }

    public void SetPartTo(PartsSO partsSO)
    {
        _partsSO = partsSO; 
    }
}
