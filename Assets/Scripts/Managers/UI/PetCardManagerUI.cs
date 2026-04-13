using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates pet card UI with accurate information.
/// </summary>
public class PetCardManagerUI : MonoBehaviour
{
    [Header("HUD Components")]
    [SerializeField] private GameObject petCard;
    [SerializeField] private TextMeshProUGUI petNameText;
    [SerializeField] private TextMeshProUGUI deadPetNameText;
    [SerializeField] private Image petProfilePicture;

    [Header("Pet bars")]
    [SerializeField] private Image healthBar;
    [SerializeField] private LevelPellet[] levelPellets;
    
    private void Start()
    {
        SetName(PetDataManager.Instance.Data.PetName);
        ClearLevelPellets();
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

    public void AssignProfilePicture(Sprite petSprite)
    {
        petProfilePicture.sprite = petSprite;
    }

    private void SetName(string petName)
    {
        petNameText.text = petName;
        deadPetNameText.text = petName + " has died!";
    }
}
