using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

/// <summary>
/// Stores focus time goal and elapsed time.
/// </summary>
public class TimeManager : MonoBehaviour
{
    private const float RewardRate = 5f;            // Give reward every x seconds.

    [Header("Timer components")]
    [SerializeField] private float timeGoal;
    [SerializeField] private float timeRemaining;

    private bool isFocused = false;
    private int rewardAmount = 0;
    private bool alreadyDamagedPet = false;
    private PetHealth petHealth;
    private PetAge petAge;
    private UiManager uiManager;
    
    private void Awake()
    {
        uiManager = GetComponent<UiManager>();
    }

    // Called when application is running on background 
    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused && isFocused && !alreadyDamagedPet)
        {
            Debug.Log("USER IS NOT FOCUSED!");
            alreadyDamagedPet = true;
            // App moved to background when on focused mode
            petHealth.TakeDamage();
            PetDataManager.Instance.SaveHealth(petHealth.health);
            PetDataManager.Instance.SaveAge(petAge.Age);
            PetDataManager.Instance.SaveLifeStage(petAge.LifeStage);
        } else
        {
            OnApplicationResume();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (isFocused && !focus && !alreadyDamagedPet)
        {
            Debug.Log("USER IS NOT FOCUSED!");
            alreadyDamagedPet = true;
            // App moved to background when on focused mode
            petHealth.TakeDamage();
            PetDataManager.Instance.SaveHealth(petHealth.health);
            PetDataManager.Instance.SaveAge(petAge.Age);
            PetDataManager.Instance.SaveLifeStage(petAge.LifeStage);
        } else
        {
            OnApplicationResume();
        }
    }

    private void OnApplicationResume()
    {
        alreadyDamagedPet = false;
    }

    // Called when user closes the application
    private void OnApplicationQuit()
    {
        petHealth.TakeDamage();
        isFocused = false;
        StopAllCoroutines();
    }

    public void Initialize(PetAge petAge, PetHealth petHealth)
    {
        this.petAge = petAge;
        this.petHealth = petHealth;
    }
    
    public void InitializeTimer(float timeGoal)
    {
        timeRemaining = timeGoal;
        isFocused = true;
        StopAllCoroutines();
        StartCoroutine(Timer());
    }

    public void CancelFocus()
    {
        isFocused = false;
        StopAllCoroutines();
        petHealth.TakeDamage();
    }

    private IEnumerator Timer()
    {
        // When it reaches 5, add a reward.
        int rewardCounter = 0;      

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
        isFocused = false;
        for (int i = 0; i < amount; i++)
        {
            petHealth.Heal();
            petAge.MakePetAge();
        }
        rewardAmount = 0;
    }
}
