using UnityEngine;

public class DeliverPanel : MonoBehaviour, ICanInteract
{
    [SerializeField] GameObject _hoverVisual;

    [SerializeField] GameObject _DeliveryShopUI;

    bool _inShop;

    private void Start()
    {
        Player.OnInteractableSiteChanged += Player_OnPartSiteChanged;

        HideShop();
    }
    private void OnDestroy()
    {
        Player.OnInteractableSiteChanged -= Player_OnPartSiteChanged;
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

    }

    public void OnInteract(Player player)
    {
        if (_inShop)
        {
            //we are aldready in shop and should close it 
            GameManager.Instance.SetGameStateToPlaying();
            _inShop = false;
            HideShop();
        }
        else
        {
            GameManager.Instance.SetGameStateToMenu();
            _inShop = true;
            ShowShop();
        }
    }

    public void ShowShop()
    {
        _DeliveryShopUI.SetActive(true);
    }
    public void HideShop()
    {
        _DeliveryShopUI.SetActive(false);
    }
}
