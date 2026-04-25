using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Saves and loads pet data.  
/// </summary>
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    [Header("Pet Prefabs")]
    [SerializeField] private GameObject bearPetPrefab;
    [SerializeField] private GameObject sharkPetPrefab;
    [SerializeField] private GameObject frogPetPrefab;
    [Header("Scene Components")]
    [SerializeField] private Transform petSpawnPoint;
    [SerializeField] private GameObject availablePositionsParent;
    [Header("UI Managers")]
    [SerializeField] private PetCardManagerUI petCardManagerUI;

    private string saveLocation;
    private GameData data;
    private LeafManager leafManager;
    private RewardManager rewardManager;
    private TimeManager timeManager;
    private EditModeManager editModeManager;
    private GameObject pet;
    private PetBehavior petBehavior;
    private PetHealth petHealth;
    
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
        leafManager = GetComponent<LeafManager>();
        rewardManager = GetComponent<RewardManager>();
        timeManager = GetComponent<TimeManager>();
        editModeManager = GetComponent<EditModeManager>();
    }

    private void Start() 
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        LoadData();
    }

    public void LoadData()
    {
        Debug.Log("Load Data");
        if (File.Exists(saveLocation))
        {
            Debug.Log("Data exists, load it then.");
            data = JsonUtility.FromJson<GameData>(File.ReadAllText(saveLocation));
            ApplyData();
        }
        else
        {
            SceneManager.LoadScene("Onboarding");
        }
    }

    public void SaveLeafs(int CurrentLeafs)
    {
        data.CurrentLeafs = CurrentLeafs;
        File.WriteAllText(saveLocation, JsonUtility.ToJson(data));
    }

    public void SaveLastCheckIn(int date)
    {
        data.lastDateCheckIn = date;
        File.WriteAllText(saveLocation, JsonUtility.ToJson(data));
    }

    public void DeleteSave()
    {
        if (File.Exists(saveLocation))
            File.Delete(saveLocation);
    }

    // Apply already loaded data
    private void ApplyData()
    {
        petCardManagerUI.SetName(data.PetName);
        leafManager.SetLeafs(data.CurrentLeafs);
        rewardManager.SetLastDateCheckIn(data.lastDateCheckIn);
        rewardManager.CheckForDailyReward();
        SpawnPet(data.Type);
    }

    // Instantiate pet GO and get components from it
    private void SpawnPet(PetType petType)
    {

        switch (petType)
        {
            case PetType.Bear:
                pet = Instantiate(bearPetPrefab, petSpawnPoint.position, Quaternion.identity);
                petCardManagerUI.AssignProfilePicture(PetType.Bear);
            break;
            case PetType.Frog: 
                pet = Instantiate(frogPetPrefab, petSpawnPoint.position, Quaternion.identity); 
                petCardManagerUI.AssignProfilePicture(PetType.Frog);
            break;
            case PetType.Shark: 
                pet = Instantiate(sharkPetPrefab, petSpawnPoint.position, Quaternion.identity); 
                petCardManagerUI.AssignProfilePicture(PetType.Shark);
            break;
        }

        if (pet == null) return;
    
        petHealth = pet.GetComponent<PetHealth>();
        petBehavior = pet.GetComponent<PetBehavior>();
        InitializePetComponents();
    }

    // Initialize each pet component and inject necessary references
    private void InitializePetComponents()
    {
        if (petHealth == null) return;
        if (petBehavior == null) return;
        
        petHealth.Initialize(petCardManagerUI, timeManager);
        timeManager.Initialize(petBehavior);
        petBehavior.Initialize(availablePositionsParent);
        editModeManager.Initialize(pet);
    }
}
