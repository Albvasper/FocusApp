using TMPro;
using UnityEngine;

public class RewardManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject dailyRewardScreen;
    [SerializeField] private TextMeshProUGUI leafReward;
    [SerializeField] private RewardManager rewardManager;

    private void Start() 
    {
        SetLeafRewardText();
    } 
    
    public void ShowDailyRewardScreen()
    {
        dailyRewardScreen.SetActive(true);
    }

    public void HideDailyRewardScreen()
    {
        dailyRewardScreen.SetActive(false);
    }

    public void ClaimReward()
    {
        rewardManager.GetReward();    
        HideDailyRewardScreen();
    }

    private void SetLeafRewardText()
    {
        leafReward.text = rewardManager.DailyRewardAmount.ToString();
    }   
}
