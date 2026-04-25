using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates pet card UI with accurate information.
/// </summary>
public class PetCardManagerUI : MonoBehaviour
{   
    [Header("Pet Profile Pictures")]
    [SerializeField] private Sprite bearSprite;
    [SerializeField] private Sprite sharkSprite;
    [SerializeField] private Sprite frogSprite;

    [Header("HUD Components")]
    [SerializeField] private GameObject petCard;
    [SerializeField] private TextMeshProUGUI petNameText;
    [SerializeField] private Image petProfilePicture;

    [Header("Pet bars")]
    [SerializeField] private Image healthBar;
    [SerializeField] private LevelPellet[] levelPellets;
    
    private void Start()
    {
        ClearLevelPellets();
    }

    public void SetName(string petName)
    {
        petNameText.text = petName;
    }

    public void ShowPetCard()
    {
        petCard.SetActive(true);
    }

    public void HidePetCard()
    {
        petCard.SetActive(false);
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth/maxHealth;
    }

    public void UpdateLevelPellets(int currentAge)
    {
        for (int i = 0; i < currentAge; i++)
        {
            levelPellets[i].ShowPellet();
        }
    }

    public void ClearLevelPellets()
    {
        foreach (LevelPellet pellet in levelPellets)
        {
            pellet.HidePellet();
        }
    }

    public void AssignProfilePicture(PetType petType)
    {   
        switch (petType)
        {
            case PetType.Bear:
                petProfilePicture.sprite = bearSprite;
            break;
            case PetType.Shark:
                petProfilePicture.sprite = sharkSprite;
            break;
            case PetType.Frog:
                petProfilePicture.sprite = frogSprite;
            break;
        }
    }
}
