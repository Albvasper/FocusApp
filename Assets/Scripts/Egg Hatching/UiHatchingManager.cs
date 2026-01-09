using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UiHatchingManager : MonoBehaviour
{
    [SerializeField] private GameObject namingScreen;
    [SerializeField] private PetData petData;

    private void Awake()
    {
        namingScreen.SetActive(false);
    }

    public void ShowNamingScreen()
    {
        namingScreen.SetActive(true);
    }

    public void SetPetName(TMP_InputField input)
    {
        petData.PetName = input.text;
        SceneManager.LoadScene("PetRoom");
    }
}