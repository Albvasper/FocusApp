using UnityEngine;
using System.Collections;

/// <summary>
/// Stores focus time goal and elapsed time.
/// </summary>
public class TimeManager : MonoBehaviour
{
    private const float RewardRate = 5f;            // Give reward every x seconds.

    [Header("Timer components")]
    [SerializeField] private float timeGoal;
    [SerializeField] private float timeRemaining;

    [Header("Pet components")]
    [SerializeField] private PetHealth petHealth;
    [SerializeField] private PetAge petAge;

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

    public void CancelFocus()
    {
        StopAllCoroutines();
        petHealth.TakeDamage();
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
            GiveRewards(rewardAmount);
    }

    private void GiveRewards(int amount)
    {
        petHealth.Heal();
        petAge.MakePetAge();
        rewardAmount = 0;
    }
}
