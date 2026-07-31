using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] Transform _orderContainer;
    [SerializeField] GameObject _orderTemplate;

    [SerializeField] List<PartsSO> _partsSOList;

    [SerializeField] List<PartSite> _partSiteList;

    [SerializeField] Button _confirmBuyButton;

    List<PartSite> _availiblePartSitesList;

    List<PartsSO> _addedPartsList;
    private void Awake()
    {
        Instance = this;

        _availiblePartSitesList = new List<PartSite>();
        _addedPartsList = new List<PartsSO>();

        SpawnPartTemplatesUI();

        _confirmBuyButton.onClick.AddListener(() =>
        {
            ConfirmBuy();
        });
    }

    public void AddToCart(PartsSO partsSO)
    {
        bool partSiteIsAvailible = false ;
        PartSite availiblePartSite = null;

        foreach (PartSite partSite in _partSiteList)
        {
            if (partSite.IsPartPlacedHere()) continue;

            //this part Site is empty
            partSiteIsAvailible = true;
            partSite.SetIsPartObjectPlacedHereTo(true);
            availiblePartSite = partSite;
            break;
        }

        if (partSiteIsAvailible == false)
        {
            Debug.Log("no space");
            return;
        }

        //partsite is availible

        _availiblePartSitesList.Add(availiblePartSite);
        _addedPartsList.Add(partsSO);
    }

    public void ConfirmBuy()
    {
        if (_addedPartsList.Count == 0) return;

        for (int i = 0; i < _availiblePartSitesList.Count; i++)
        {
            _availiblePartSitesList[i].SpawnPart(_addedPartsList[i]);
        }
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
