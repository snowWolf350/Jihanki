using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartTemplateUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _partName;
    [SerializeField] Button _removeFromCart;

    PartsSO _partSO;

    private void Awake()
    {
        _removeFromCart.onClick.AddListener(() =>
        {
            ShopManager.Instance.RemoveFromCart(_partSO);

            Destroy(gameObject);
        });
    }

    public void SetPartTo(PartsSO partsSO)
    {
        _partSO = partsSO;
        _partName.text = partsSO._partName;
    }
}
