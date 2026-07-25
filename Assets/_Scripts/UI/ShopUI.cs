using UnityEngine;
using System.Collections.Generic;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    [SerializeField] Transform _orderContainer;
    [SerializeField] GameObject _orderTemplate;

    [SerializeField] List<PartsSO> _partsSOList;

    [SerializeField] List<PartSite> _partSiteList;
    private void Awake()
    {
        Instance = this;

        foreach(PartsSO partsSO in _partsSOList)
        {
            GameObject newPart = Instantiate(_orderTemplate, _orderContainer);

            newPart.GetComponent<OrderTemplateUI>().SetPartTo(partsSO);

            newPart.SetActive(true);
        }
    }

    public void BuyPart(PartsSO partsSO)
    {
        bool partSiteIsAvailible = false ;
        PartSite availiblePartSite = null;

        foreach (PartSite partSite in _partSiteList)
        {
            if (partSite.IsPartPlacedHere()) continue;

            //this part Site is empty
            partSiteIsAvailible = true;
            availiblePartSite = partSite;
            break;
        }

        if (partSiteIsAvailible == false)
        {
            Debug.Log("no space");
            return;
        }

        //partsite is availible

        availiblePartSite.SpawnPart(partsSO);

    }
}
