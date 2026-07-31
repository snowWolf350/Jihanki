using UnityEngine;

[CreateAssetMenu(fileName = "PartsSO", menuName = "Scriptable Objects/PartsSO")]
public class PartsSO : ScriptableObject
{
    public string _partName;
    public Sprite _partSprite;
    public GameObject _partObject;
    public int _partCost;
}
