using UnityEngine;

[CreateAssetMenu(fileName = "PetData", menuName = "Scriptable Objects/PetData")]
public class PetData : ScriptableObject
{

    public const int MaxHealth = 20;
    public const int MaxLifeStage = 4;
    public const int MaxAge = 10;

    public string PetName;
    public PetType Type;
    [Range(1, MaxLifeStage)] public int CurrentLifeStage;
    [Range(1, MaxAge)] public int CurrentAge;
    [Range(0, MaxHealth)] public int CurrentHealth;
}
