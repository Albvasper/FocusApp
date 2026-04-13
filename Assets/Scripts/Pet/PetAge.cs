using UnityEngine;

public class PetAge : MonoBehaviour
{
    public int LifeStage;
    public int Age;
    
    [SerializeField] private Sprite babyPetSprite;
    [SerializeField] private Sprite standardPetSprite;
    [SerializeField] private Sprite oldPetSprite;

    private PetHealth health;
    private PetCardManagerUI petCardManagerUI;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        health = GetComponent<PetHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (LifeStage > PetData.MaxLifeStage) health.Die();
    }

    public void Initialize(PetCardManagerUI petCardManagerUI)
    {
        this.petCardManagerUI = petCardManagerUI;
    }

    public void SetAge(int currentAge)
    {
        Age = currentAge;
        Evolve();
        petCardManagerUI.UpdateLevelPellets(Age);
    }

    public void MakePetAge()
    {
        Age++;
        petCardManagerUI.UpdateLevelPellets(Age);
        PetDataManager.Instance.SaveAge(Age);
        if (Age >= PetData.MaxAge)
        {
            Age = 0;
            LifeStage++;
            Evolve();
            petCardManagerUI.ClearLevelPellets();
            petCardManagerUI.UpdateLevelPellets(Age);
            PetDataManager.Instance.SaveAge(Age);
            PetDataManager.Instance.SaveLifeStage(LifeStage);
            if (LifeStage >= PetData.MaxLifeStage)
                health.Die();
        }
    }

    // When going into a new life stage switch sprite
    private void Evolve()
    {
        switch (LifeStage)
        {
            case 1: 
                spriteRenderer.sprite = babyPetSprite; 
                petCardManagerUI.AssignProfilePicture(babyPetSprite);
            break;
            case 2: 
                spriteRenderer.sprite = standardPetSprite; 
                petCardManagerUI.AssignProfilePicture(standardPetSprite);
            break;
            default: 
                spriteRenderer.sprite = oldPetSprite; 
                petCardManagerUI.AssignProfilePicture(oldPetSprite);
            break;
        }
    }
}