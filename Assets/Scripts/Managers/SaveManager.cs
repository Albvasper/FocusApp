using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//TODO: Separate load logic from save manager
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
    [Header("Decoration Prefabs")]
    [SerializeField] private List<DecorativeItem> decorativeItems = new();

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
        data.LastDateCheckIn = date;
        File.WriteAllText(saveLocation, JsonUtility.ToJson(data));
    }

    public void SaveDecoration(DecorationObject decoration)
    {
        data.Decorations.Add(new()
        {
            ID = decoration.ID,
            X = decoration.transform.position.x,
            Y = decoration.transform.position.y,
            Flipped = decoration.SpriteRenderer.flipX
        });
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
        rewardManager.SetLastDateCheckIn(data.LastDateCheckIn);
        rewardManager.CheckForDailyReward();
        SpawnPet(data.Type);
        LoadDecorations();
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
    
    private void LoadDecorations()
    {
        foreach (DecorationData decorationData in data.Decorations)
        {
            DecorativeItem item = decorativeItems.Find(i => i.name == decorationData.ID);

            if (item == null) 
                continue;

            DecorationObject decorationObject;
            GameObject decorationObjectGO = 
                Instantiate(item.item, new Vector3(decorationData.X, decorationData.Y, 0f), Quaternion.identity);
            decorationObject = decorationObjectGO.GetComponent<DecorationObject>();
            decorationObject.Initialize(editModeManager);
        }
    }
}
