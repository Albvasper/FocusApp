using UnityEngine;
using System.Collections;

/// <summary>
/// Stores focus time goal and elapsed time.
/// </summary>
public class TimeManager : MonoBehaviour
{
    private const float RewardRate = 60f;            // Give reward every x seconds.

    public bool isFocused = false;

    [SerializeField] private ScreenManagerUI screenManagerUI;
    [SerializeField] private TimerManagerUI timerManagerUI;
    
    private float timeRemaining;
    private PetHealth petHealth;
    private PetAge petAge;
    private PetBehavior petBehavior;
    private LeafManager leafManager;
    
    public void Initialize(PetAge petAge, PetHealth petHealth, PetBehavior petBehavior)
    {
        this.petAge = petAge;
        this.petHealth = petHealth;
        this.petBehavior = petBehavior;
    }

    // Called when application is running on background 
    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused && isFocused)
        {
            // App moved to background when on focused mode
            Debug.Log("USER IS NOT FOCUSED!");
            CancelFocusSession();
        } else
        {
            OnApplicationResume();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (isFocused && !focus)
        {
            Debug.Log("USER IS NOT FOCUSED!");
            // App moved to background when on focused mode
            CancelFocusSession();
        } else
        {
            OnApplicationResume();
        }
    }

    private void OnApplicationResume()
    {
        //alreadyDamagedPet = false;
    }

    // Called when user closes the application
    private void OnApplicationQuit()
    {
        if (isFocused)
        {
            CancelFocusSession();
        }
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

    public void CancelFocusSession()
    {
        isFocused = false;
        petBehavior.SetState(State.IDLE);
        StopAllCoroutines();
        timerManagerUI.HideFocusScreen();
        screenManagerUI.ShowFailureScreen();
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
            timerManagerUI.UpdateTimerText(timeRemaining);

            if (rewardCounter >= RewardRate)
            {
                rewardCounter = 0;
                rewardAmount++;
            }
            if (timeRemaining <= 0)
                break;
        }
        GiveRewards(rewardAmount);
        screenManagerUI.ShowSucessScreen();
    }

    private void GiveRewards(int amount)
    {
        isFocused = false;
        leafManager.AddLeafs(amount);
        petBehavior.SetState(State.IDLE);
    }
}
