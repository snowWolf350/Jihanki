using UnityEngine;

public class PartSite : MonoBehaviour
{
    [SerializeField] PartObject _partObjectPacedHere;

    [SerializeField] GameObject _hoverVisual;

    private void Start()
    {
        Player.OnPartSiteChanged += Player_OnPartSiteChanged;
    }

    private void Player_OnPartSiteChanged(object sender, Player.PartSiteEventArgs e)
    {
        if (e.site == this)
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
}
