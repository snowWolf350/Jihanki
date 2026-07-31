using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CartUI : MonoBehaviour
{
    [SerializeField] GameObject _templateGO;

    [SerializeField] TextMeshProUGUI _cartTotalText;


    List<PartsSO> _cartPartsSOList;

    private void Start()
    {
        ShopManager.OnPartAddedSuccesfully += ShopManager_OnPartAddedSuccesfully;
        ShopManager.OnConfirmBuy += ShopManager_OnConfirmBuy;
    }

    private void ShopManager_OnConfirmBuy(object sender, System.EventArgs e)
    {
        clearCartUI();
    }

    private void ShopManager_OnPartAddedSuccesfully(object sender, System.EventArgs e)
    {
        _cartPartsSOList = ShopManager.Instance.GetAddedPartsList();

        if (transform.childCount != 0)
        {
            clearCartUI();
        }

        foreach (PartsSO partsSO in _cartPartsSOList)
        {
            GameObject newItem = Instantiate(_templateGO, transform);

            newItem.GetComponent<CartTemplateUI>().SetPartTo(partsSO);

            newItem.SetActive(true);
        }

        _cartTotalText.text = "Total : " + ShopManager.Instance.GetCartCost().ToString();
    }

    void clearCartUI()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject == _templateGO) continue;

            Destroy(child.gameObject);
        }
        _cartTotalText.text = "Total : 0";
    }
}
