using UnityEngine;

[CreateAssetMenu(fileName = "PetData", menuName = "Scriptable Objects/PetData")]
public class PetData : ScriptableObject
{

    public const int MaxHealth = 20;
    public const int MaxLifeStage = 4;          // If pet ages more than MaxLiofeStage it will die
    public const int MaxAge = 10;               // Every x years, pet will go up one life stage

    public bool PetAssinged;
    public string PetName;
    public PetType Type;
    [Range(1, MaxLifeStage)] public int CurrentLifeStage;
    [Range(1, MaxAge)] public int CurrentAge;
    [Range(0, MaxHealth)] public int CurrentHealth;
}
