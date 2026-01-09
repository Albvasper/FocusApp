using UnityEngine;

[CreateAssetMenu(fileName = "PetData", menuName = "Scriptable Objects/PetData")]
public class PetData : ScriptableObject
{

    public const int MaxHealth = 20;
    public const int MaxAge = 4;

    public string PetName;
    public PetType PetType;
    [Range(1,MaxAge)] public int PetAge;
    [Range(0,MaxHealth)] public int PetCurrentHealth;
}
