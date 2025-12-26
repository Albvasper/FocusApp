using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

/*
    - User sets a timer (Intervals of 5)
    - Starts session
    - Start timer and decrese time overtime
    - Every 5 min the user generates something
    - End of session the user gets the item
*/

/// <summary>
/// 
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
    }
}
