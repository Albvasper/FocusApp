using TMPro;
using UnityEngine;

public class JournalManagerUI : MonoBehaviour
{   
    [Header("Transform Parents")]
    [SerializeField] private Transform todaySessionParent;
    [SerializeField] private Transform weeklySessionParent;
    [SerializeField] private Transform monthlySessionParent;

    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI sessionText;
    [SerializeField] private GameObject todayUnavailableSessionsText;
    [SerializeField] private GameObject weeklyUnavailableSessionsText;
    [SerializeField] private GameObject monthlyUnavailableSessionsText;

    private float sessionTime;    

    public void SetSessionTime(float sessionTime)
    {
        this.sessionTime = sessionTime;
    }

    public void CreateSessionInJournal()
    {
        todayUnavailableSessionsText.SetActive(false);
        sessionText.text = 
            System.DateTime.Now.Day.ToString() + 
            "/" +
            System.DateTime.Now.Month.ToString() + 
            " session: " + 
            GiveSessionTimeFormat(sessionTime) +
            " minutes.";
        Instantiate(sessionText, todaySessionParent);
        //TODO: Save 
    }

    private string GiveSessionTimeFormat(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60); 
        float seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
