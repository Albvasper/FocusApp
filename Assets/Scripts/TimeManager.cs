using UnityEngine;
using System.Collections;

/// <summary>
/// Stores focus time goal and elapsed time.
/// </summary>
public class TimeManager : MonoBehaviour
{
    private const float RewardRate = 5f;            // Give reward every x seconds.

    [SerializeField] private float timeGoal;
    [SerializeField] private float timeRemaining;
    
    private int rewardAmount = 0;
    private UiManager uiManager;

    private void Awake()
    {
        uiManager = GetComponent<UiManager>();
    }

    public void InitializeTimer(float timeGoal)
    {
        timeRemaining = timeGoal;
        StopAllCoroutines();
        StartCoroutine(Timer());
    }

    public void RestartTimer()
    {
        StopAllCoroutines();
    }

    private IEnumerator Timer()
    {
        int rewardCounter = 0;      // When it reaches 5, add a reward.

        while (true)
        {
            yield return new WaitForSeconds(1f);
            rewardCounter++;
            timeRemaining--;
            uiManager.UpdateTimerText(timeRemaining);

            if (rewardCounter >= RewardRate)
            {
                rewardCounter = 0;
                rewardAmount++;
            }
            if (timeRemaining <= 0)
                break;
        }
        // Give rewards here!
    }
}
