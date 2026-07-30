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
            //player is carrying a partobj
            partobject.SetParentTo(this);
        }
        else
        {
            //player is not carrying any obj
            if (_partObjectPacedHere == null) return; // if site is empty
            _partObjectPacedHere.SetParentTo(player);
        }
    }
    public void OnAltInteract(Player player)
    {

    }

}
