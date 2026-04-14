using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles all UI logic for onboarding process.
/// </summary>
public class OnboardingManagerUI : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject selectPetScreen;
    [SerializeField] private GameObject namingScreen;

    [Header("Components")]
    [SerializeField] private Button nextButton;
    [SerializeField] private Image petImage;

    [Header("Pet Sprites")]
    [SerializeField] private Sprite bearPetSprite;
    [SerializeField] private Sprite frogPetSprite;
    [SerializeField] private Sprite sharkPetSprite;

    private OnBoardingManager onboardingManager;
    private PetType petTypePicked;

    private void Awake() 
    {
        onboardingManager = GetComponent<OnBoardingManager>();
    }

    private void Start()
    {
        ShowSelectPetScreen();
        nextButton.interactable = false;
    }

    public void ShowSelectPetScreen()
    {
        selectPetScreen.SetActive(true);
        namingScreen.SetActive(false);
    }

    public void ShowNamingScreen()
    {
        selectPetScreen.SetActive(false);
        namingScreen.SetActive(true);
    }

    public void SelectPet(string petTypePicked)
    {
        switch (petTypePicked)
        {
            case "Bear":
                this.petTypePicked = PetType.Bear;
                nextButton.interactable = true;
            break;
            case "Shark":
                this.petTypePicked = PetType.Shark;
                nextButton.interactable = true;
            break;
            case "Frog":
                this.petTypePicked = PetType.Frog;
                nextButton.interactable = true;
            break;
        }
        DisplayPickedPetSprite(this.petTypePicked);
    }

    public void SetPetData(TMP_InputField input)
    {
        onboardingManager.SavePetName(input);
        onboardingManager.GenerateNewPet(petTypePicked);
        onboardingManager.GoToRoomLevel();
    }

    private void DisplayPickedPetSprite(PetType petType)
    {
        switch (petType)
        {
            case PetType.Bear: petImage.sprite = bearPetSprite; break;
            case PetType.Frog: petImage.sprite = frogPetSprite; break;
            case PetType.Shark: petImage.sprite = sharkPetSprite; break;
        }
    }
}
