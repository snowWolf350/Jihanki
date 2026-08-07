using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DeliveryShopTemplateUI : MonoBehaviour
{
    BuildSite _buildSite;

    [SerializeField] Button _deliverButton;
    [SerializeField] TextMeshProUGUI _buildSiteText;

    private void Awake()
    {
        _deliverButton.onClick.AddListener(() =>
        {
            CheckOrder();
        });
    }

    public void SetBuildSiteTo(BuildSite buildSite)
    {
        _buildSite = buildSite;

        _buildSiteText.text = buildSite.name;
    }

    void CheckOrder()
    {
        List<PartsSO> buildSitePartsSoList = _buildSite.GetPartsSOList();

        List<OrderSO> order_timeList = DeliveryManager.Instance.GetRequiredOrdersList();

        OrderSO correctOrderSO = null;

        foreach (OrderSO ordersSO in order_timeList)
        {
            if (ordersSO._partsList.Count != buildSitePartsSoList.Count)
            {
                Debug.Log("Not matching number of parts");
                return;
            }

            bool correctOrderFound = false;
            //cycling through each order
            foreach (PartsSO requiredPartSO in ordersSO._partsList)
            {
                if(buildSitePartsSoList.Contains(requiredPartSO) == false)
                {
                    //needed part is not there hence this is not the order
                    correctOrderFound = false;
                    break;
                }
                correctOrderFound = true;
            }
            if (correctOrderFound == true)
            {
                Debug.Log("Correct order found");
                correctOrderSO = ordersSO;
                break;
            }
        }
        if(correctOrderSO == null)
        {
            Debug.Log("No correct order found");
            return;
        }

        //correct order is found and stored
        Debug.Log("Correct order found and added money");
        DeliveryManager.Instance.CompleteOrder(correctOrderSO);
        _buildSite.ClearBuild();
        MoneyManager.Instance.AddMoney(correctOrderSO._orderCost);
    }
}
