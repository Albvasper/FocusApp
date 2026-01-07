using UnityEngine;

/// <summary>
/// Saves and loads pet data so it will be stored each session.
/// </summary>
public class PetDataManager : MonoBehaviour
{   
    [SerializeField] private PetData petData;

    [Header("Pet components")]
    [SerializeField] private PetAge petAge;
    [SerializeField] private PetBehavior petBehavior;
    [SerializeField] private PetHealth petHealth;

    [Header("Managers")]
    private UiManager uiManager;

    private void Awake()
    {
        petAge.Age = petData.PetAge;
        //petBehavior
        petHealth.SetHealth(petData.PetCurrentHealth);
    }
}