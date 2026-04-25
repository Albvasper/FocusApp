using TMPro;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PetType
{
    Bear,
    Frog,
    Shark
}

/// <summary>
/// Creates pet data at onboarding process.
/// </summary>
public class OnBoardingManager : MonoBehaviour
{
    private string saveLocation;

    private void Start() 
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }   

    public void CreateData(PetType petTypePicked, TMP_InputField petNamePicked)
    {
        GameData data = new()
        {
            Type = petTypePicked,
            PetName = petNamePicked.text,
            CurrentLeafs = 0,
            lastDateCheckIn = System.DateTime.Now.Day
        };
        File.WriteAllText(saveLocation, JsonUtility.ToJson(data));
    }

    public void GoToRoomLevel()
    {
        SceneManager.LoadScene("PetRoom");
    }
}