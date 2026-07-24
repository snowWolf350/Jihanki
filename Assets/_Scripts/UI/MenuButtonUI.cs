using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MenuButtonUI : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    public UnityEvent OnClick;

    [SerializeField] Material _GreenMaterial;
    [SerializeField] Material _BlackMaterial;

    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.Instance.PlayCoinInsertSound();
        OnClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<MeshRenderer>().material = _GreenMaterial;
        SoundManager.Instance.PlayUiHoverSound();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<MeshRenderer>().material = _BlackMaterial;
    }

}
