using UnityEngine;

public class DeliveryContainerUI : MonoBehaviour
{
    [SerializeField] GameObject _deliveryTemplateUI;

    private void Start()
    {
        DeliveryManager.Instance.OnNewOrderSpawned += DeliveryManager_OnNewOrderSpawned;
    }

    private void DeliveryManager_OnNewOrderSpawned(object sender, System.EventArgs e)
    {
        foreach (Transform t in transform)
        {
            if (t == _deliveryTemplateUI.transform)
            {
                continue;
            }
            Destroy(t.gameObject);
        }
        foreach (OrderSO ordersSO in DeliveryManager.Instance.GetRequiredOrdersList())
        {
            GameObject spawnedTemplate = Instantiate(_deliveryTemplateUI, transform);
            spawnedTemplate.GetComponent<DeliveryTemplateUI>().SetOrder_timeTo(ordersSO);
            spawnedTemplate.SetActive(true);
        }
    }
}
