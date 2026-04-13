using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Animates and displays focus timer on screen.
/// </summary>
public class TimerManagerUI : MonoBehaviour
{
    [Header("UI Components")]

    [SerializeField] private GameObject timerScreen;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject focusScreenInteractables;
    [SerializeField] private GameObject focusScreenBG;
    [SerializeField] private GameObject timerComponents;
    [SerializeField] private GameObject cancelButton;

    [Header("Manager")]
    [SerializeField] private TimeManager timeManager;

    private Vector3 timerStartingPosition;

    private void Start()
    {
        timerStartingPosition = timerComponents.transform.localPosition;
    }

    /// <summary>
    /// Initializes timer and hide setup UI components.
    /// </summary>
    public void OnTimerStart()
    {
        timeManager.InitializeTimer(timeSlider.value);
        StartCoroutine(AnimateFocusUI());

        focusScreenInteractables.SetActive(false);
        focusScreenBG.SetActive(false);
        cancelButton.SetActive(true);
        //focusPetInfoWindow.SetActive(true);
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

    public void ShowTimerScreen()
    {
        timerComponents.transform.localPosition = timerStartingPosition;
        //focusPetInfoWindow.SetActive(false);
        timerScreen.SetActive(true);
        focusScreenInteractables.SetActive(true);
        focusScreenBG.SetActive(true);
        cancelButton.SetActive(false);
    }

    public void CancelFocus()
    {
        // TODO: Window confirming that the user wants to cancel focus
        timeManager.CancelFocusSession();
        timerText.text = "05:00";
        timeSlider.value = 0;
    }
    
    public void HideFocusScreen()
    {
        timerScreen.SetActive(false);    
    }

    // When timer is initiated move timer and cancel button down
    private IEnumerator AnimateFocusUI()
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
}
