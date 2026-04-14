using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PetType
{
    Bear,
    Frog,
    Shark
}

/// <summary>
/// Saves pet data at onboarding process.
/// </summary>
public class OnBoardingManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PetData data;

    private OnboardingManagerUI onboardingManagerUI;

    private void Awake()
    {
        onboardingManagerUI = GetComponent<OnboardingManagerUI>();
        PetDataSaveSystem.Load(data);
    }

    public void SavePetName(TMP_InputField name)
    {
        data.PetName = name.text;
        PetDataSaveSystem.Save(data);
    }

    public void GoToRoomLevel()
    {
        SceneManager.LoadScene("PetRoom");
    }

    public void GenerateNewPet(PetType petTypePicked)
    {
        data.PetAssinged = true;
        data.Type = petTypePicked;
        data.CurrentLifeStage = 1;
        data.CurrentHealth = PetData.MaxHealth;
        data.CurrentAge = 0;
        
        PetDataSaveSystem.Save(data);
    }
}
