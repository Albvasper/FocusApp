using UnityEngine;

[CreateAssetMenu(fileName = "PetData", menuName = "Scriptable Objects/PetData")]
public class PetData : ScriptableObject
{
    public string PetName;
    public PetType PetType;
    [Range(0,4)] public int PetAge;
    [Range(0,20)] public int PetCurrentHealth;
}
