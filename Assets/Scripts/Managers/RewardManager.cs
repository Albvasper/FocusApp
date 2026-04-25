using UnityEngine;

/// <summary>
/// Checks if player recieved their daily reward.
/// </summary>
public class RewardManager : MonoBehaviour
{   
    public int DailyRewardAmount = 15;
    [SerializeField] private RewardManagerUI rewardManagerUI;

    private LeafManager leafManager;
    private int lastDate;

    private void Awake() 
    {
        leafManager = GetComponent<LeafManager>();    
    }
    
    public void SetLastDateCheckIn(int date)
    {
        lastDate = date;
    }
    
    public void CheckForDailyReward() 
    {
        if (lastDate != System.DateTime.Now.Day)
        {
           rewardManagerUI.ShowDailyRewardScreen();
        }    
    }

    public void GetReward()
    {
        lastDate = System.DateTime.Now.Day;
        leafManager.AddLeafs(DailyRewardAmount);
        SaveManager.Instance.SaveLastCheckIn(lastDate);
    }
}
