using UnityEngine;
using System.Collections.Generic;
using System;

public class BuildSite : InteractSite , ICanInteract , IHasProgress
{
    [SerializeField] List<PartsSO> _buildOrder;

    [SerializeField] List<PartSO_GameObjects> PartsSO_GameObjectsList;

    List<PartsSO> _partsList;

    int _buildIndex = 0;

    float _buildAmount;
    float _buildAmountMax = 5;

    bool _baseBuilt;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    [Serializable]
    struct PartSO_GameObjects
    {
        public PartsSO partsSO;
        public GameObject GameObject;   
    }
    private void Awake()
    {
        _partsList = new List<PartsSO>();
    }
    public void OnInteract(Player player)
    {
        if (player.TryGetHeldPartObject(out PartObject partObject))
        {
            //player is holding something
            if (partObject.GetPartsSO() == _buildOrder[_buildIndex])
            {
                //this is the part needed for building
                partObject.SetParentTo(this);

                if (_baseBuilt == false) return; // base is not done yet

                //base is done can add electric and drinks
                _partsList.Add(partObject.GetPartsSO());
                ShowPartVisual();
                _buildIndex++;
            }
        }
    }
    public void OnAltInteract(Player player)
    {
        if (_partObjectPacedHere == null) return;
        if (_buildIndex != 0) return;

        //base obj is placed here

        _buildAmount++;


        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = _buildAmount / _buildAmountMax
        });

        if (_buildAmount >= _buildAmountMax)
        {
            ShowPartVisual();

            _partsList.Add(_partObjectPacedHere.GetPartsSO());    

            _buildAmount = 0;
            _buildIndex++;
            _baseBuilt = true;
        }
    }

    void ShowPartVisual()
    {
        foreach (PartSO_GameObjects p_go in PartsSO_GameObjectsList)
        {
            if (p_go.partsSO == _partObjectPacedHere.GetPartsSO())
            {
                p_go.GameObject.SetActive(true);
            }
        }
    }
}
