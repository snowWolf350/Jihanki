using UnityEngine;

public class PartObject : MonoBehaviour
{
    [SerializeField] PartsSO _partsSO;

    IPartParent _currentParent;

    public void SetParentTo(IPartParent newPartParent)
    {
        if (_currentParent != null)
        {
            _currentParent.SetPartObjectTo(null);
        }

        _currentParent = newPartParent;
        _currentParent.SetPartObjectTo(this);

        transform.parent = newPartParent.GetPlacementTransform();
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public PartsSO GetPartsSO()
    {
        return _partsSO;
    }
}
