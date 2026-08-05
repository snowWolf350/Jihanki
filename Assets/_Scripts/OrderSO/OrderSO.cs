using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "OrderSO", menuName = "Scriptable Objects/OrderSO")]
public class OrderSO : ScriptableObject
{
    public List<PartsSO> _partsList;
    public int _orderCost;
    public float _finishTime;
}
