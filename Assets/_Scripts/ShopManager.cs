using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] List<PartSite> _partSiteList;

    [SerializeField] Button _confirmBuyButton;

    List<PartSite> _OccupiedPartSitesList;

    List<PartsSO> _addedPartsList;

    int _cartCost;

    public static event EventHandler OnPartAddedSuccesfully;
    public static event EventHandler OnNoAvailiblePartSite;
    public static event EventHandler OnNoMoney;

    public static event EventHandler OnConfirmBuy;

    private void Awake()
    {
        Instance = this;

        _OccupiedPartSitesList = new List<PartSite>();
        _addedPartsList = new List<PartsSO>();

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
            OnNoAvailiblePartSite?.Invoke(this, EventArgs.Empty);
            return;
        }

        //partsite is availible

        if (_cartCost + partsSO._partCost > MoneyManager.Instance.GetCurrentAmount())
        {
            Debug.Log("No money");
            OnNoMoney?.Invoke(this, EventArgs.Empty);
            return;
        }

        _OccupiedPartSitesList.Add(availiblePartSite);
        _addedPartsList.Add(partsSO);

        UpdateCartUIWith(partsSO._partCost);
    }

    public void RemoveFromCart(PartsSO partsSO)
    {
        int partIndex =0;
        foreach (PartsSO p_so in _addedPartsList)
        {
            if (p_so == partsSO)
            {
                //this is the part i want to remove
                _addedPartsList.Remove(p_so);
                _OccupiedPartSitesList.RemoveAt(partIndex);

                UpdateCartUIWith(-partsSO._partCost);
                break;
            }
            partIndex++;
        }
    }
    void UpdateCartUIWith(int partsSOCost)
    {
        _cartCost += partsSOCost;

        OnPartAddedSuccesfully?.Invoke(this, EventArgs.Empty);
    }
    void ClearCart()
    {
        _OccupiedPartSitesList.Clear();
        _addedPartsList.Clear();
    }

    public void ConfirmBuy()
    {
        if (_addedPartsList.Count == 0) return;

        for (int i = 0; i < _OccupiedPartSitesList.Count; i++)
        {
            _OccupiedPartSitesList[i].SpawnPart(_addedPartsList[i]);
        }

        MoneyManager.Instance.AddMoney(-_cartCost);

        _cartCost = 0;
        ClearCart();
        OnConfirmBuy?.Invoke(this, EventArgs.Empty);
    }

    public int GetCartCost()
    {
        return _cartCost;
    }

    public List<PartsSO> GetAddedPartsList()
    {
        return _addedPartsList;
    }
}
