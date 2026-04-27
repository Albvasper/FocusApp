using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalManagerUI : MonoBehaviour
{   
    [Header("Transform Parents")]
    [SerializeField] private RectTransform rootRectTransform;
    [SerializeField] private RectTransform todaySessionParent;
    [SerializeField] private RectTransform weeklySessionParent;
    [SerializeField] private RectTransform monthlySessionParent;

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
        string session = 
            System.DateTime.Now.Month.ToString() + 
            "/" +
            System.DateTime.Now.Day.ToString() + 
            " session: " + 
            GiveSessionTimeFormat(sessionTime) +
            " minutes.";
        sessionText.text = session;
        Instantiate(sessionText, todaySessionParent);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rootRectTransform);
        SaveManager.Instance.SaveFocusSession(session);
    }

    public void RenewTodaySession(string session)
    {
        todayUnavailableSessionsText.SetActive(false);
        sessionText.text = session;
        Instantiate(sessionText, todaySessionParent);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rootRectTransform);
    }

    public void RenewWeekSession(string session)
    {
        weeklyUnavailableSessionsText.SetActive(false);
        sessionText.text = session;
        Instantiate(sessionText, weeklySessionParent);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rootRectTransform);
    }

    public void RenewMonthSession(string session)
    {
        monthlyUnavailableSessionsText.SetActive(false);
        sessionText.text = session;
        Instantiate(sessionText, monthlySessionParent);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rootRectTransform);
    }

    private string GiveSessionTimeFormat(float time)
    {
        float minutes = time / 60f;
        return minutes.ToString();
    }

    [ContextMenu("Trigger new session creation")]
    public void ManualCreateSession()
    {
        todayUnavailableSessionsText.SetActive(false);
        string session = 
            "4" + 
            "/" +
            System.DateTime.Now.Day.ToString() + 
            " session: " + 
            "5" +
            " minutes.";
        sessionText.text = session;
        Instantiate(sessionText, todaySessionParent);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rootRectTransform);
        SaveManager.Instance.SaveFocusSession(session);
    }
}
