using UnityEngine;
using UnityEngine.UI;

public class OrderTemplateUI : MonoBehaviour
{
    [SerializeField] Button _buyButton;

    PartsSO _partsSO;

    private void Awake()
    {
        _buyButton.onClick.AddListener(() =>
        {
            Debug.Log("Bought " + _partsSO._partName);
        });
    }

    public void SetPartTo(PartsSO partsSO)
    {
        _partsSO = partsSO; 
    }
}
