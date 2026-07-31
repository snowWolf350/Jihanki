using UnityEngine;
using UnityEngine.UI;

public class OrderTemplateUI : MonoBehaviour
{
    [SerializeField] Button _buyButton;

    [SerializeField]PartsSO _partsSO;

    [SerializeField] Image _partImage;

    private void Awake()
    {
        _buyButton.onClick.AddListener(() =>
        {
            ShopManager.Instance.AddToCart(_partsSO);
        });
    }

    public void SetPartTo(PartsSO partsSO)
    {
        _partsSO = partsSO;
        _partImage.sprite = partsSO._partSprite;
    }
}
