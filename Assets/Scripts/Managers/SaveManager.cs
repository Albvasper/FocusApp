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

    private string saveLocation;
    private GameData data;
    private LeafManager leafManager;
    private RewardManager rewardManager;

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

    public void DeleteSave()
    {
        if (File.Exists(saveLocation))
            File.Delete(saveLocation);
    }

    // Apply already loaded data
    private void ApplyData()
    {
        leafManager.SetLeafs(data.CurrentLeafs);
        rewardManager.SetLastDateCheckIn(data.lastDateCheckIn);
        rewardManager.CheckForDailyReward();
    }
}
