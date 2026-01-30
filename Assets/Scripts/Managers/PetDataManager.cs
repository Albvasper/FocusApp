using System;
using UnityEngine;

/// <summary>
/// Saves and loads pet data so it will be stored each session.
/// </summary>
public class PetDataManager : MonoBehaviour
{   

    public static PetDataManager Instance { get; private set; }

    public PetData Data;

    [SerializeField] private Transform spawnPoint;

    [Header("Pet Prefabs")]
    [SerializeField] private GameObject ghostPetPrefab;
    [SerializeField] private GameObject plantPetPrefab;

    [Header("Components")]
    [SerializeField] private GameObject availablePositionsParent;

    private PetAge petAge;
    private PetBehavior petBehavior;
    private PetHealth petHealth;
    private UiManager uiManager;
    private TimeManager timeManager;

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
        uiManager = GetComponent<UiManager>();
        timeManager = GetComponent<TimeManager>();
        LoadData();
    }
    
    public void SaveLifeStage(int newLifeStage)
    {
        Data.CurrentLifeStage = newLifeStage;
        PetDataSaveSystem.Save(Data);
    }

    public void SaveAge(int newAge)
    {
        Data.CurrentAge = newAge;
        PetDataSaveSystem.Save(Data);
    }

    public void SaveHealth(int newHealth)
    {
        Data.CurrentHealth = newHealth;
        PetDataSaveSystem.Save(Data);
    }

    private void LoadData()
    {
        PetDataSaveSystem.Load(Data);
        
        SpawnPet(Data.Type);
        petAge.LifeStage = Data.CurrentLifeStage;
        petHealth.SetHealth(Data.CurrentHealth);
        petAge.Age = Data.CurrentAge;
        CheckPetsHealth();
    }

    private void CheckPetsHealth()
    {
        if (petHealth.health <= 0)
        {
            uiManager.ShowDeadPetScreen();
        }
    }

    // Instantiate pet GO and get components from it
    private void SpawnPet(PetType petType)
    {
        GameObject pet = null;

        switch (petType)
        {
            case PetType.GhostPet:
                pet = Instantiate(ghostPetPrefab, spawnPoint.position, Quaternion.identity);
            break;
            case PetType.PlantPet: 
                pet = Instantiate(plantPetPrefab, spawnPoint.position, Quaternion.identity); 
            break;
        }

        if (pet == null) return;

        petHealth = pet.GetComponent<PetHealth>();
        petBehavior = pet.GetComponent<PetBehavior>();
        petAge = pet.GetComponent<PetAge>();
        InitializePetComponents();
    }

    // Initialize each pet component and inject necessary references
    private void InitializePetComponents()
    {
        if (petHealth == null) return;
        if (petAge == null) return;
        if (petBehavior == null) return;
        
        petHealth.Initialize(uiManager, timeManager);
        timeManager.Initialize(petAge, petHealth);
        petBehavior.Initialize(availablePositionsParent);
    }
}