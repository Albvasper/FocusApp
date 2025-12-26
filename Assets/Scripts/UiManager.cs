using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI timerText;

    private TimeManager timeManager;

    private void Awake()
    {
        timeManager = GetComponent<TimeManager>();
    }

    public void OnTimerStart(TMP_InputField input)
    {
        float timeGoal = float.Parse(input.text);
        timeManager.InitializeTimer(timeGoal);
    }

    public void UpdateTimerText(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
