using UnityEngine;

public class PartSite : MonoBehaviour
{
    [SerializeField] PartObject _partObjectPacedHere;

    public PartObject GetPartObject()
    {
        return _partObjectPacedHere;
    }
}
