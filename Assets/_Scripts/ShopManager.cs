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

    [SerializeField] TextMeshProUGUI _moneyText;

    List<PartSite> _availiblePartSitesList;

    List<PartsSO> _addedPartsList;

    int _currentMoney = 1000;

    int _cartCost;

    public static event EventHandler OnPartAddedSuccesfully;
    public static event EventHandler OnNoAvailiblePartSite;
    public static event EventHandler OnNoMoney;

    public static event EventHandler OnConfirmBuy;

    private void Awake()
    {
        Instance = this;

        _availiblePartSitesList = new List<PartSite>();
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

        if (_cartCost + partsSO._partCost > _currentMoney)
        {
            Debug.Log("No money");
            OnNoMoney?.Invoke(this, EventArgs.Empty);
            return;
        }

        _cartCost += partsSO._partCost;

        _availiblePartSitesList.Add(availiblePartSite);
        _addedPartsList.Add(partsSO);

        OnPartAddedSuccesfully?.Invoke(this, EventArgs.Empty);

    }

    void ClearCart()
    {
        _availiblePartSitesList.Clear();
        _addedPartsList.Clear();
    }

    public void ConfirmBuy()
    {
        if (_addedPartsList.Count == 0) return;

        for (int i = 0; i < _availiblePartSitesList.Count; i++)
        {
            _availiblePartSitesList[i].SpawnPart(_addedPartsList[i]);
        }

        AddMoney(-_cartCost);

        _cartCost = 0;
        ClearCart();
        OnConfirmBuy?.Invoke(this, EventArgs.Empty);
    }

    public void AddMoney(int addAmount)
    {
        _currentMoney += addAmount;
        _moneyText.text = _currentMoney.ToString();
    }

    public int GetCartCost()
    {
        return _cartCost;
    }

    public int GetCurrentAmount()
    {
        return _currentMoney;
    }

    public List<PartsSO> GetAddedPartsList()
    {
        return _addedPartsList;
    }
}
