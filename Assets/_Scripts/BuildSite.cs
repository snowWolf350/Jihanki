using UnityEngine;
using System.Collections.Generic;
using System;

public class BuildSite : InteractSite , ICanInteract , IHasProgress
{
    [SerializeField] List<PartsSO> _buildOrder;

    [SerializeField] List<PartSO_GameObjects> PartsSO_GameObjectsList; 

    int _buildIndex = 0;

    float _buildAmount;
    float _buildAmountMax = 5;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    [Serializable]
    struct PartSO_GameObjects
    {
        public PartsSO partsSO;
        public GameObject GameObject;   
    }

    public void OnInteract(Player player)
    {
        if (player.TryGetHeldPartObject(out PartObject partObject))
        {
            _partObjectPacedHere = partObject;
            //player is holding something
            if (_partObjectPacedHere.GetPartsSO() == _buildOrder[_buildIndex])
            {
                //this is the part needed for building
                _partObjectPacedHere.SetParentTo(_partPlaceTransform);
                player.SetPartObject(null);
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
            foreach (PartSO_GameObjects p_go in PartsSO_GameObjectsList)
            {
                if (p_go.partsSO == _partObjectPacedHere.GetPartsSO())
                {
                    p_go.GameObject.SetActive(true);
                }
            }

            _buildAmount = 0;
            _buildIndex++;
        }
    }
}
