using UnityEngine;
using System.Collections.Generic;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Transform _orderContainer;
    [SerializeField] GameObject _orderTemplate;

    [SerializeField] List<PartsSO> _partsSOList;

    private void Awake()
    {
        foreach(PartsSO partsSO in _partsSOList)
        {
            GameObject newPart = Instantiate(_orderTemplate, _orderContainer);

            newPart.GetComponent<OrderTemplateUI>().SetPartTo(partsSO);

            newPart.SetActive(true);
        }
    }
}
