using System;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSite : InteractSite, ICanInteract , IHasProgress
{
    [SerializeField] List<ElectricRecipeSO> _electricRecipeSOList;

    float _buildAmount =0 ;
    float _buildAmountMax = 3;

    ElectricRecipeSO _currentElecRecipe;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    public void OnAltInteract(Player player)
    {
        if (_partObjectPacedHere == null) return;

        _buildAmount++;

        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = _buildAmount/_buildAmountMax
        });

        if (_buildAmount >= _buildAmountMax)
        {
            //replace the object
            Destroy(_partPlaceTransform.GetChild(0).gameObject);
            _buildAmount = 0;
            GameObject spawnedPart = Instantiate(_currentElecRecipe.output._partObject, _partPlaceTransform);
            _partObjectPacedHere = spawnedPart.GetComponent<PartObject>();
        }
    }

    public void OnInteract(Player player)
    {
       if (player.TryGetHeldPartObject(out PartObject partObject))
        {

            //player is holding something 
            foreach (ElectricRecipeSO electricRecipe in _electricRecipeSOList)
            {
                if (partObject.GetPartsSO() == electricRecipe.input)
                {
                    partObject.SetParentTo(this);
                    _currentElecRecipe = electricRecipe;
                    break;
                }
            }
        }
    }
}
