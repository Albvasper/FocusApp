using UnityEngine;

/// <summary>
/// Checks if player recieved their daily reward.
/// </summary>
public class RewardManager : MonoBehaviour
{   
    public int DailyRewardAmount = 15;
    [SerializeField] private RewardManagerUI rewardManagerUI;

    private LeafManager leafManager;
    public int lastDate = 0;

    private void Awake() 
    {
        leafManager = GetComponent<LeafManager>();    
    }

    private void Start() 
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
    }
}
