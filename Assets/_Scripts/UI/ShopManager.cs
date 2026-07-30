using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] Transform _orderContainer;
    [SerializeField] GameObject _orderTemplate;

    [SerializeField] List<PartsSO> _partsSOList;

    [SerializeField] List<PartSite> _partSiteList;
    private void Awake()
    {
        Instance = this;

        SpawnPartTemplatesUI();
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

    void SpawnPartTemplatesUI()
    {
        foreach (PartsSO partsSO in _partsSOList) // foreach partso in this list
        {
            GameObject newPart = Instantiate(_orderTemplate, _orderContainer); // create a new template in the container

            newPart.GetComponent<OrderTemplateUI>().SetPartTo(partsSO); // set the part in the template to this

            newPart.SetActive(true); // game object set active
        }
    }
}
