using UnityEngine;

/// <summary>
/// Saves and loads pet data so it will be stored each session.
/// </summary>
public class PetDataManager : MonoBehaviour
{   

    public static PetDataManager Instance { get; private set; }

    public PetData Data;

    [Header("Pet components")]
    [SerializeField] private PetAge petAge;
    [SerializeField] private PetBehavior petBehavior;
    [SerializeField] private PetHealth petHealth;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        LoadData();
    }

    private void LoadData()
    {
        petAge.LifeStage = Data.CurrentLifeStage;
        //petBehavior
        petHealth.SetHealth(Data.CurrentHealth);
        // TODO: Set pet type and replace sprite and animations
        petAge.Age = Data.CurrentAge;
    }

    private void SaveData()
    {
        // TODO: Save pet data
    }
}