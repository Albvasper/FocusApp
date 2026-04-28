using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;

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

    [Header("UI Components")]
    [SerializeField] private Animator sadPetAnimator;
    [SerializeField] private Animator happyPetAnimator;

    [Header("Bear Animation Clips")]
    [SerializeField] private AnimatorController bearHappyController;
    [SerializeField] private AnimatorController bearSadController;
    [Header("Frog Animation Clips")]
    [SerializeField] private AnimatorController frogHappyController;
    [SerializeField] private AnimatorController frogSadController;
    [Header("Shark Animation Clips")]
    [SerializeField] private AnimatorController sharkHappyController;
    [SerializeField] private AnimatorController sharkSadController;

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

    public void SetPetType(PetType type)
    {
        switch (type)
        {
            case PetType.Bear:
                sadPetAnimator.runtimeAnimatorController = bearSadController;
                happyPetAnimator.runtimeAnimatorController = bearHappyController;
            break;
            case PetType.Shark:
                sadPetAnimator.runtimeAnimatorController = sharkSadController;
                happyPetAnimator.runtimeAnimatorController = sharkHappyController;
            break;
            case PetType.Frog:
                sadPetAnimator.runtimeAnimatorController = frogSadController;
                happyPetAnimator.runtimeAnimatorController = frogHappyController;
            break;
        }
    }
}
