using UnityEngine;

public class OrderPanel : MonoBehaviour, ICanInteract
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

    public void OnInteract(Player player)
    {
        if (_inShop)
        {
            //we are aldready in shop and should close it 
            _inShop = false;
            HideShop();
            CameraController.Instance.SetTargetTo(player.transform);
        }
        else
        {
            _inShop = true;
            ShowShop();
            CameraController.Instance.SetTargetTo(_shopUI.transform);
        }
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
