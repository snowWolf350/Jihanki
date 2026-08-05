using UnityEngine;
using UnityEngine.UI;

public class DeliveryTemplateUI : MonoBehaviour
{
    OrderSO _ordersSO;

    [SerializeField] GameObject _partImage;

    [SerializeField] Transform _deliveryUIContainer;

    [SerializeField] Image _fillImage;

    private void Start()
    {
        
    }

    public void SetOrder_timeTo(OrderSO ordersSO)
    {
        _ordersSO = ordersSO;

        foreach (PartsSO partsSO in _ordersSO._partsList)
        {
            GameObject orderImageSpawned = Instantiate(_partImage,_deliveryUIContainer);

            orderImageSpawned.GetComponent<Image>().sprite = partsSO._partSprite;

            orderImageSpawned.SetActive(true);
        }
    }
}
