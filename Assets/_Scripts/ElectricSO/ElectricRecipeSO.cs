using UnityEngine;

[CreateAssetMenu(fileName = "ElectricRecipeSO", menuName = "Scriptable Objects/ElectricRecipeSO")]
public class ElectricRecipeSO : ScriptableObject
{
    public PartsSO input;
    public PartsSO output;
}
