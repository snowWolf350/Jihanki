using UnityEngine;

public class PartSite : MonoBehaviour , ICanInteract
{
    [SerializeField] PartObject _partObjectPacedHere;

    [SerializeField] GameObject _hoverVisual;

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

    public void OnInteract(Player player)
    {
        if (_partObjectPacedHere == null) return;

        _partObjectPacedHere.SetParentTo(player.GetHoldTransform());
    }
}
