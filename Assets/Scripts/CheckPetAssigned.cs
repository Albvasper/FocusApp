using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// When the user opens the app it will check if a pet was assigned or not.
/// </summary>
public class CheckPetAssigned : MonoBehaviour
{
    [SerializeField] private PetData data;

    private void Awake()
    {
        PetDataSaveSystem.Load(data);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        
        if (data.PetAssinged)
            SceneManager.LoadScene("PetRoom");
        else
            SceneManager.LoadScene("Onboarding");
    }
}
