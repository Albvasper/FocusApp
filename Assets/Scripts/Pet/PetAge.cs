using UnityEngine;

public class PetAge : MonoBehaviour
{
    public int LifeStage;
    public int Age;
    
    [SerializeField] private Sprite babyPetSprite;
    [SerializeField] private Sprite standardPetSprite;
    [SerializeField] private Sprite oldPetSprite;


    private PetHealth health;
    private UiManager uiManager;
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

    public void Initialize(UiManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public void SetAge(int currentAge)
    {
        Age = currentAge;
        Evolve();
        uiManager.UpdateLevelPellets(Age);
    }

    public void MakePetAge()
    {
        Age++;
        uiManager.UpdateLevelPellets(Age);
        PetDataManager.Instance.SaveAge(Age);
        if (Age >= PetData.MaxAge)
        {
            Age = 0;
            LifeStage++;
            Evolve();
            uiManager.ClearLevelPellets();
            uiManager.UpdateLevelPellets(Age);
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
            case 1: spriteRenderer.sprite = babyPetSprite; break;
            case 2: spriteRenderer.sprite = standardPetSprite; break;
            default: spriteRenderer.sprite = oldPetSprite; break;
        }
    }
}