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
    [Header("HUD Components")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image focusHealthBar;
    [SerializeField] private TextMeshProUGUI petNameText;
    [SerializeField] private TextMeshProUGUI focusPetNameText;
    [SerializeField] private TextMeshProUGUI deadPetNameText;
    [SerializeField] private GameObject focusPetInfoWindow;

    [Header("Timer Components")]
    [SerializeField] private Slider timeSlider;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject focusScreenInteractables;
    [SerializeField] private GameObject timerComponents;
    [SerializeField] private GameObject cancelButton;

    [Header("Screens")]
    [SerializeField] private GameObject timerScreen;
    [SerializeField] private GameObject hudScreen;
    [SerializeField] private GameObject deadPetScreen;
    [SerializeField] private GameObject successScreen;

    private TimeManager timeManager;
    private Vector3 timerStartingPosition;

    private void Awake()
    {
        timeManager = GetComponent<TimeManager>();
        timerStartingPosition = timerComponents.transform.localPosition;
    }

    private void Start()
    {
        SetName(PetDataManager.Instance.Data.PetName);
        ShowHUD();
    }

    /// <summary>
    /// Initializes timer and hide setup UI components.
    /// </summary>
    public void OnTimerStart()
    {
        timeManager.InitializeTimer(timeSlider.value);
        StartCoroutine(MoveFocusUI());

        focusScreenInteractables.SetActive(false);
        cancelButton.SetActive(true);
        focusPetInfoWindow.SetActive(true);
    }
    
    /// <summary>
    /// Updates the timer text.
    /// </summary>
    public void OnSliderValueChanged()
    {
        // Snap knob every 5 minutes
        float snapped = Mathf.Round(timeSlider.value / 300f) * 300f;
        timeSlider.SetValueWithoutNotify(snapped);
        UpdateTimerText(snapped);
    }

    /// <summary>
    /// Converts seconds left to minutes and seconds and displays it on the UI.
    /// </summary>
    /// <param name="timeToDisplay">Seconds to convert to minutes and seconds.</param>
    public void UpdateTimerText(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth/maxHealth;
        focusHealthBar.fillAmount = (float)currentHealth/maxHealth;
    }

    public void UpdateFocusScreenHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth/maxHealth;
        focusHealthBar.fillAmount = (float)currentHealth/maxHealth;
    }

    public void ShowTimerScreen()
    {
        timerComponents.transform.localPosition = timerStartingPosition;
        focusPetInfoWindow.SetActive(false);
        hudScreen.SetActive(false);
        timerScreen.SetActive(true);
        focusScreenInteractables.SetActive(true);
        cancelButton.SetActive(false);
    }
    
    public void ShowDeadPetScreen()
    {
        AudioManager.Instance.PlayGameOverSFX();
        deadPetScreen.SetActive(true);
        hudScreen.SetActive(false);
        timerScreen.SetActive(false);
        focusScreenInteractables.SetActive(false);
        cancelButton.SetActive(false);
        timerText.text = "05:00";
        timeSlider.value = 0;
    }

    public void ShowHUD()
    {
        hudScreen.SetActive(true);
        timerScreen.SetActive(false);
        focusScreenInteractables.SetActive(true);
        successScreen.SetActive(false);
        cancelButton.SetActive(false);
        timerText.text = "05:00";
        timeSlider.value = 0;
    }

    public void CancelFocus()
    {
        // TODO: Window confirming that the user wants to cancel focus
        ShowHUD();
        timeManager.CancelFocus();
        timerText.text = "05:00";
        timeSlider.value = 0;
    }

    public void ShowSucessScreen()
    {
        AudioManager.Instance.PlaySuccessSFX();
        successScreen.SetActive(true);
        timerScreen.SetActive(false);
    }

    public void BuyNewEgg()
    {
        PetDataManager.Instance.Data.PetAssinged = false;
        SceneManager.LoadScene("EggHatchingRoom");
    }

    private IEnumerator MoveFocusUI()
    {
        float elapsed = 0f;
        const float Duration = 1f;
        Vector3 targetPosition = new(0f, -417f, 0f);

        while (elapsed < Duration)
        {
            elapsed += Time.deltaTime;
            float time = elapsed / Duration;
            timerComponents.transform.localPosition = 
                Vector3.Lerp(timerStartingPosition, targetPosition, time);
            yield return null;
        }
    }
        
    private void SetName(string petName)
    {
        petNameText.text = petName;
        focusPetNameText.text = petName;
        deadPetNameText.text = petName + " has died!";
    }
}