using UnityEngine;

public class OrderPanel : MonoBehaviour, ICanInteract
{
    [SerializeField] GameObject _hoverVisual;

    [SerializeField] GameObject _orderPanelUI;

    private void Start()
    {
        Player.OnInteractableSiteChanged += Player_OnPartSiteChanged;
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

    public void OnInteract(Player player)
    {
        _orderPanelUI.SetActive(true);
    }
}
