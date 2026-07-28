using UnityEngine;

public class InteractSite : MonoBehaviour ,IPartParent
{
    protected PartObject _partObjectPacedHere;

    [SerializeField] GameObject _hoverVisual;

    [SerializeField] protected Transform _partPlaceTransform;

    private void Start()
    {
        Player.OnInteractableSiteChanged += Player_OnPartSiteChanged;

        _hoverVisual.SetActive(false);
    }
    private void Player_OnPartSiteChanged(object sender, Player.InteractableSiteEventArgs e)
    {
        if (e.interactale == this as ICanInteract)
        {
            _hoverVisual.SetActive(true);
        }
        else
        {
            _hoverVisual.SetActive(false);
        }
    }


    public PartObject GetPartObject()
    {
        return _partObjectPacedHere;
    }
    public bool IsPartPlacedHere()
    {
        return _partObjectPacedHere != null;
    }

    public Transform GetPlacementTransform()
    {
        return _partPlaceTransform;
    }

    public void SetPartObjectTo(PartObject partobject)
    {
        _partObjectPacedHere = partobject;
    }
}
