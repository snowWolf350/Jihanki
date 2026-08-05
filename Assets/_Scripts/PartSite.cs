using UnityEngine;

public class PartSite : InteractSite , ICanInteract
{
    public void SpawnPart(PartsSO partsSO)
    {
        GameObject spawnedPart =  Instantiate(partsSO._partObject,_partPlaceTransform);

        spawnedPart.GetComponent<PartObject>().SetParentTo(this);
    }

    public void OnInteract(Player player)
    {
        if (player.TryGetHeldPartObject(out PartObject partobject))
        {
            //player is carrying a partobj place it here
            partobject.SetParentTo(this);
            SetIsPartObjectPlacedHereTo(true);
        }
        else
        {
            //player is not carrying any obj
            if (_partObjectPacedHere == null) return; // if site is empty
            //Give player the object
            _partObjectPacedHere.SetParentTo(player);
            SetIsPartObjectPlacedHereTo(false);
        }
    }
    public void OnAltInteract(Player player)
    {

    }

}
