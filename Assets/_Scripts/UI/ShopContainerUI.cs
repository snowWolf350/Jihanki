using System.Collections.Generic;
using UnityEngine;

public class ShopContainerUI : MonoBehaviour
{

    [SerializeField] Transform _orderContainer;
    [SerializeField] GameObject _orderTemplate;
    [SerializeField] List<PartsSO> _partsSOList;


    void Start()
    {
        foreach (PartsSO partsSO in _partsSOList) // foreach partso in this list
        {
            GameObject newPart = Instantiate(_orderTemplate, _orderContainer); // create a new template in the container

            newPart.GetComponent<OrderTemplateUI>().SetPartTo(partsSO); // set the part in the template to this

            newPart.SetActive(true); // game object set active
        }
    }

}
