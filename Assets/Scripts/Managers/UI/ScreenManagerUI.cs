using UnityEngine;

/// <summary>
/// Swaps UI screens.
/// </summary>
public class ScreenManagerUI : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject successScreen;
    [SerializeField] private GameObject failureScreen;
    [SerializeField] private GameObject editModeScreen;

    private void Start()
    {
        ShowMainScreen();
    }

    public void ShowMainScreen()
    {
        mainScreen.SetActive(true);
        successScreen.SetActive(false);
        failureScreen.SetActive(false);
        editModeScreen.SetActive(false);
    }

    public void HideMainScreen()
    {
        mainScreen.SetActive(false);
        successScreen.SetActive(false);
        failureScreen.SetActive(false);
        editModeScreen.SetActive(false);
    }

    public void ShowFailureScreen()
    {
        AudioManager.Instance.PlayGameOverSFX();
        failureScreen.SetActive(true);
    }

    public void ShowSucessScreen()
    {
        AudioManager.Instance.PlaySuccessSFX();
        successScreen.SetActive(true);
    }

    public void ShowEditModeScreen()
    {
        editModeScreen.SetActive(true);
    }

    public void HideEditModeScreen()
    {
        editModeScreen.SetActive(false);
    }
}
