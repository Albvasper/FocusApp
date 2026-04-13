using UnityEngine;

public class DockManager : MonoBehaviour
{

    [SerializeField] private RectTransform dock;
    [SerializeField] private GameObject shopScreen;
    [SerializeField] private GameObject journalScreen;
    [SerializeField] private GameObject editModeScreen;
    [SerializeField] private EditModeManager editModeManager;

    private float dockDefaultPositionY = -1151.39f;

    public void ExpandDock()
    {
        dock.anchoredPosition = new Vector2(dock.anchoredPosition.x, 0f);
    }

    public void HideDock()
    {
        dock.anchoredPosition = new Vector2(dock.anchoredPosition.x, dockDefaultPositionY);
    }

    public void ShowStoreScreen()
    {
        shopScreen.SetActive(true);
        journalScreen.SetActive(false);
    }
    
    public void ShowJournalScreen()
    {
        shopScreen.SetActive(false);
        journalScreen.SetActive(true);
    }

    public void EnableEditMode()
    {
        editModeManager.EnterEditMode();
        editModeScreen.SetActive(true);
    }

    public void QuitEditMode()
    {
        editModeManager.ExitEditMode();
        editModeScreen.SetActive(false);
    }
}
