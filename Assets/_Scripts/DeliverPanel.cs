using UnityEngine;

public class DeliverPanel : MonoBehaviour, ICanInteract
{
    [SerializeField] GameObject _hoverVisual;

    [SerializeField] GameObject _shopUI;

    bool _inShop;

    private void Start()
    {
        Player.OnInteractableSiteChanged += Player_OnPartSiteChanged;

        HideShop();
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
    public void OnAltInteract(Player player)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(Player player)
    {
        throw new System.NotImplementedException();
    }

    public void ShowShop()
    {
        _shopUI.SetActive(true);
    }
    public void HideShop()
    {
        _shopUI.SetActive(false);
    }
}
