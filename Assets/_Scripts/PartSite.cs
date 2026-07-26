using UnityEngine;

public class PartSite : InteractSite , ICanInteract
{
    public void SpawnPart(PartsSO partsSO)
    {
        GameObject spawnedPart =  Instantiate(partsSO._partObject,_partPlaceTransform);

        _partObjectPacedHere = spawnedPart.GetComponent<PartObject>();
    }

    public void OnInteract(Player player)
    {
        if (_partObjectPacedHere == null) return;

        _partObjectPacedHere.SetParentTo(player.GetHoldTransform());
        player.SetPartObject(_partObjectPacedHere);
        _partObjectPacedHere = null;
    }
    public void OnAltInteract(Player player)
    {

    }
}
