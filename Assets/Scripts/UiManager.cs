using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// Handles showing screens, button functionalities and timers.
/// </summary>
public class UiManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Slider timeSlider;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject focusScreenInteractables;
    [SerializeField] private GameObject timerComponents;
    [SerializeField] private GameObject cancelButton;
     [Header("Screens")]
    [SerializeField] private GameObject timerScreen;
    [SerializeField] private GameObject hudScreen;

    private TimeManager timeManager;
    private Vector3 timerStartingPosition;

    private void Awake()
    {
        timeManager = GetComponent<TimeManager>();
        timerStartingPosition = timerComponents.transform.localPosition;
    }

    private void Start()
    {
        ShowHUD();
    }

    /// <summary>
    /// Initializes timer and hide setup UI components.
    /// </summary>
    public void OnTimerStart()
    {
        timeManager.InitializeTimer(timeSlider.value);

        focusScreenInteractables.SetActive(false);
        cancelButton.SetActive(true);
        StartCoroutine(MoveFocusUI());
    }
    
    /// <summary>
    /// Updates the timer text.
    /// </summary>
    public void OnSliderValueChanged()
    {
        UpdateTimerText(timeSlider.value);
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

    public void ShowTimerScreen()
    {
        timerComponents.transform.localPosition = timerStartingPosition;
        hudScreen.SetActive(false);
        timerScreen.SetActive(true);
        focusScreenInteractables.SetActive(true);
        cancelButton.SetActive(false);
    }

    public void ShowHUD()
    {
        hudScreen.SetActive(true);
        timerScreen.SetActive(false);
        focusScreenInteractables.SetActive(true);
        cancelButton.SetActive(false);
    }

    public void CancelFocus()
    {
        ShowHUD();
        timeManager.RestartTimer();
        timerText.text = "00:00";
        timeSlider.value = 0;
    }

    private IEnumerator MoveFocusUI()
    {
        float elapsed = 0f;
        const float Duration = 1f;
        Vector3 targetPosition = new(0f, -678.5f, 0f);

        while (elapsed < Duration)
        {
            elapsed += Time.deltaTime;
            float time = elapsed / Duration;
            timerComponents.transform.localPosition = 
                Vector3.Lerp(timerStartingPosition, targetPosition, time);
            yield return null;
        }
    }
}
