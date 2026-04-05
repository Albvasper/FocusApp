using UnityEngine;
using System.Collections;

/// <summary>
/// Stores focus time goal and elapsed time.
/// </summary>
public class TimeManager : MonoBehaviour
{
    private const float RewardRate = 60f;            // Give reward every x seconds.

    [Header("Timer components")]
    [SerializeField] private float timeGoal;
    [SerializeField] private float timeRemaining;
    public bool isFocused = false;

    private bool alreadyDamagedPet = false;
    private PetHealth petHealth;
    private PetAge petAge;
    private PetBehavior petBehavior;

    private UiManager uiManager;
    private LeafManager leafManager;
    
    private void Awake()
    {
        uiManager = GetComponent<UiManager>();
        leafManager = GetComponent<LeafManager>();
    }
    
    public void Initialize(PetAge petAge, PetHealth petHealth, PetBehavior petBehavior)
    {
        this.petAge = petAge;
        this.petHealth = petHealth;
        this.petBehavior = petBehavior;
    }

    // Called when application is running on background 
    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused && isFocused && !alreadyDamagedPet)
        {
            // App moved to background when on focused mode
            Debug.Log("USER IS NOT FOCUSED!");
            alreadyDamagedPet = true;
            petHealth.TakeDamage();
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
        if (isFocused)
            petHealth.TakeDamage();
        StopAllCoroutines();
    }
    
    public void InitializeTimer(float timeGoal)
    {
        // Make the phone not go to sleep automatically
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        petBehavior.SetState(State.Focusing);
        timeRemaining = timeGoal;
        isFocused = true;
        StopAllCoroutines();
        StartCoroutine(Timer());
    }

    public void CancelFocus()
    {
        isFocused = false;
        petBehavior.SetState(State.IDLE);
        StopAllCoroutines();
        petHealth.TakeDamage();
    }

    public void StopTimer()
    {
        isFocused = false;
        petBehavior.SetState(State.IDLE);
        StopAllCoroutines();
    }

    // Count seconds and keep count of rewards
    private IEnumerator Timer()
    {
        int rewardAmount = 0;
        // When it reaches X, add a reward.
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
        uiManager.ShowSucessScreen();
    }

    private void GiveRewards(int amount)
    {
        isFocused = false;
        leafManager.AddLeafs(amount);
        petBehavior.SetState(State.IDLE);
    }
}
