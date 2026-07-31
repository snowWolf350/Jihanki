using TMPro;
using UnityEngine;

public class CartTemplateUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _partName;

    public void SetPartTo(PartsSO partsSO)
    {
        _partName.text = partsSO._partName;
    }
}
