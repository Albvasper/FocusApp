using System.IO;
using UnityEngine;

public static class PetDataSaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/petData.json";

    public static void Save(PetData petData)
    {
        PetSaveData saveData = new PetSaveData()
        {
            petAssigned = petData.PetAssinged,
            petName = petData.PetName,
            type = petData.Type,
            currentLifeStage = petData.CurrentLifeStage,
            currentAge = petData.CurrentAge,
            currentHealth = petData.CurrentHealth
        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
    }

    public static void Load(PetData petData)
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save found. Creating new one.");
            Save(petData);
            return;
        }

        string json = File.ReadAllText(SavePath);

        // If file is empty or broken
        if (string.IsNullOrWhiteSpace(json))
        {
            Debug.LogWarning("Save file empty. Recreating.");
            Save(petData);
            return;
        }

        try
        {
            PetSaveData saveData = JsonUtility.FromJson<PetSaveData>(json);

            petData.PetAssinged      = saveData.petAssigned;
            petData.PetName          = saveData.petName;
            petData.Type             = saveData.type;
            petData.CurrentLifeStage = saveData.currentLifeStage;
            petData.CurrentAge       = saveData.currentAge;
            petData.CurrentHealth    = saveData.currentHealth;
        }
        catch
        {
            Debug.LogError("Save file corrupted. Resetting.");
            Save(petData);
        }
    }


    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
            File.Delete(SavePath);
    }
}
