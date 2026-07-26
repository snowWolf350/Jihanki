using UnityEngine;

public class PartObject : MonoBehaviour
{
    [SerializeField] PartsSO _partsSO;

    public void SetParentTo(Transform parentTransform)
    {
        transform.parent = parentTransform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public PartsSO GetPartsSO()
    {
        return _partsSO;
    }
}
