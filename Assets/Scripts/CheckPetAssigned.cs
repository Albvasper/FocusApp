using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// When the user opens the app it will check if a pet was assigned or not.
/// </summary>
public class CheckPetAssigned : MonoBehaviour
{   
    private string saveLocation;

    private void Start()
    {
        Application.targetFrameRate = 60;

        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(saveLocation))
        {
            Debug.Log("Data DOES exists");
            SceneManager.LoadScene("PetRoom");
        }
        else
        {
            Debug.Log("Data DOESNT exist");
            SceneManager.LoadScene("Onboarding");
        }
    }
}
