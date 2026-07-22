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
        OnClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<MeshRenderer>().material = _GreenMaterial;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<MeshRenderer>().material = _BlackMaterial;
    }

}
