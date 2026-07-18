using UnityEngine;

public class PartObject : MonoBehaviour
{
    public void SetParent(Transform parentTransform)
    {
        transform.parent = parentTransform;
        transform.localPosition = Vector3.zero;
    }
}
