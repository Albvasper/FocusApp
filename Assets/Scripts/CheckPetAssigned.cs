using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("EggHatchingRoom");
    }
}
