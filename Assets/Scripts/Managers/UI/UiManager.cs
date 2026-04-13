using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles showing screens, button functionalities and timers.
/// </summary>
public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leafCounterText;

    [Header("Screens")]
    [SerializeField] private GameObject hudScreen;
    [SerializeField] private GameObject deadPetScreen;
    [SerializeField] private GameObject successScreen;
    [SerializeField] private GameObject failureScreen;

    private void Start()
    {
        ShowHUD();
    }

    public void UpdateLeafCounterText(int leafAmount)
    {
        leafCounterText.text = leafAmount.ToString();
    }
    
    public void ShowDeadPetScreen()
    {
        AudioManager.Instance.PlayGameOverSFX();
        deadPetScreen.SetActive(true);
        hudScreen.SetActive(false);
        //timerScreen.SetActive(false);
        //focusScreenInteractables.SetActive(false);
        //focusScreenBG.SetActive(false);
        //cancelButton.SetActive(false);
        //timerText.text = "05:00";
        //timeSlider.value = 0;
    }

    public void ShowHUD()
    {
        //HideDock();
        //petCard.SetActive(true);
        hudScreen.SetActive(true);
        //timerScreen.SetActive(false);
        //focusScreenInteractables.SetActive(true);
        //focusScreenBG.SetActive(true);
        successScreen.SetActive(false);
        //cancelButton.SetActive(false);
        failureScreen.SetActive(false);
        //timerText.text = "05:00";
        //timeSlider.value = 0;
    }

    public void ShowFailureScreen()
    {
        AudioManager.Instance.PlayGameOverSFX();
        failureScreen.SetActive(true);
        //timerScreen.SetActive(false);
    }

    public void ShowSucessScreen()
    {
        AudioManager.Instance.PlaySuccessSFX();
        successScreen.SetActive(true);
        //timerScreen.SetActive(false);
    }

    public void BuyNewEgg()
    {
        PetDataManager.Instance.Data.PetAssinged = false;
        SceneManager.LoadScene("EggHatchingRoom");
    }

    public void HideUI()
    {
        //petCard.SetActive(false);
        //timerScreen.SetActive(false);
        hudScreen.SetActive(false);
        deadPetScreen.SetActive(false);
        successScreen.SetActive(false);
        failureScreen.SetActive(false);
    }
}