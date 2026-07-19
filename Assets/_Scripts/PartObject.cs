using UnityEngine;

public class PartObject : MonoBehaviour
{
    public void SetParentTo(Transform parentTransform)
    {
        transform.parent = parentTransform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
