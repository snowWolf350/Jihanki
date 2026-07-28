using UnityEngine;

public interface IPartParent
{
    public Transform GetPlacementTransform();

    public void SetPartObjectTo(PartObject partobject);
}
