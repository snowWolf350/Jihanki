using UnityEngine;
using System.Collections.Generic;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance;

    [SerializeField] List<OrderSO> _orderSOList;

    List<OrderSO> _requiredOrderSOList;

    int _maxOrders = 3;

    float _orderSpawnTimer;
    float _orderSpawnTimerMax = 5;

    private void Awake()
    {
        Instance = this;
        _requiredOrderSOList = new List<OrderSO>();
    }

    private void Update()
    {
        if (_requiredOrderSOList.Count >= _maxOrders)
        {
            return;
        }

        _orderSpawnTimer += Time.deltaTime;
        if (_orderSpawnTimer > _orderSpawnTimerMax)
        {
            _requiredOrderSOList.Add(_orderSOList[Random.Range(0, _orderSOList.Count)]);
            _orderSpawnTimer = 0;
        }
    }

    public List<OrderSO> GetRequiredOrdersList()
    {
        return _requiredOrderSOList;
    }
}
