using UnityEngine;
using UnityEngine.UI;

public class BuildSiteTemplateUI : MonoBehaviour
{
    [SerializeField] Image _partImage;

    public void SetImageTo(Sprite sprite)
    {
        _partImage.sprite = sprite;
    }
}
