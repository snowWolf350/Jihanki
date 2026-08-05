using UnityEngine;
using System.Collections.Generic;
using System;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance;

    [SerializeField] List<OrderSO> _orderSOList;

    List<OrderSO> _requiredOrderSOList;

    int _currentOrder = 0;
    int _maxOrders = 3;

    float _orderSpawnTimer;
    float _orderSpawnTimerMax = 5;

    public event EventHandler OnNewOrderSpawned;

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
            OrderSO order =_orderSOList[UnityEngine.Random.Range(0, _orderSOList.Count)];

            _requiredOrderSOList.Add(order);

            _currentOrder++;
            _orderSpawnTimer = 0;
            OnNewOrderSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    public List<OrderSO> GetRequiredOrdersList()
    {
        return _requiredOrderSOList;
    }
}
