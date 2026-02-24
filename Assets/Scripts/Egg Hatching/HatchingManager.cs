using UnityEngine;
using System.Collections;

public enum PetType
{
    Bear,
    Frog,
    Shark
}

/// <summary>
/// Assigns pet type and hatches it.
/// </summary>
public class HatchingManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int hatchingTimeMin = 10;
    [SerializeField] private int hatchingTimeMax = 25;

    [Header("Components")]
    [SerializeField] private EggAnimator animator;
    [SerializeField] private PetData data;
    [SerializeField] private SpriteRenderer petSprite;

    [Header("Pet Sprites")]
    [SerializeField] private Sprite bearPetSprite;
    [SerializeField] private Sprite frogPetSprite;
    [SerializeField] private Sprite sharkPetSprite;

    private UiHatchingManager uiHatchingManager;

    private void Awake()
    {
        uiHatchingManager = GetComponent<UiHatchingManager>();
        PetDataSaveSystem.Load(data);
    }

    private void Start()
    {
        data.PetAssinged = true;
        PetDataSaveSystem.Save(data);
        
        PetType pickedPetType = GenerateNewPet();
        DisplayPickedPetSprite(pickedPetType);
        
        animator.ShakeEggAnimation();
        StartCoroutine(WaitForHatching());
    }

    private IEnumerator WaitForHatching()
    {
        float hatchingTime = Random.Range(hatchingTimeMin, hatchingTimeMax);
        // Wait for it to hatch
        yield return new WaitForSeconds(hatchingTime);
        yield return Hatch();
    }

    private IEnumerator Hatch()
    {
        int delay = 6;

        animator.HatchEggAnimation();
        yield return new WaitForSeconds(delay);
        uiHatchingManager.ShowNamingScreen();
    }

    private PetType GenerateNewPet()
    {
        PetType petType = (PetType)Random.Range(0, 2);
        data.Type = petType;
        data.CurrentLifeStage = 1;
        data.CurrentHealth = PetData.MaxHealth;
        data.CurrentAge = 0;
        
        PetDataSaveSystem.Save(data);
        return petType;
    }

    private void DisplayPickedPetSprite(PetType petType)
    {
        switch (petType)
        {
            case PetType.Bear: petSprite.sprite = bearPetSprite; break;
            case PetType.Frog: petSprite.sprite = frogPetSprite; break;
            case PetType.Shark: petSprite.sprite = sharkPetSprite; break;
        }
    }
}