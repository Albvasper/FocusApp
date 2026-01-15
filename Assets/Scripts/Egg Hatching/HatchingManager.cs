using UnityEngine;
using System.Collections;

public enum PetType
{
    GhostPet,
    PlantPet
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
    [SerializeField] private PetData petData;
    [SerializeField] private SpriteRenderer petSprite;

    [Header("Pet Sprites")]
    [SerializeField] private Sprite ghostPetSprite;
    [SerializeField] private Sprite plantPetSprite;

    private UiHatchingManager uiHatchingManager;

    private void Awake()
    {
        uiHatchingManager = GetComponent<UiHatchingManager>();
    }

    private void Start()
    {
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
        petData.Type = petType;
        petData.CurrentLifeStage = 1;
        petData.CurrentHealth = PetData.MaxHealth;
        petData.CurrentAge = 0;
        return petType;
    }

    private void DisplayPickedPetSprite(PetType petType)
    {
        switch (petType)
        {
            case PetType.GhostPet: petSprite.sprite = ghostPetSprite; break;
            case PetType.PlantPet: petSprite.sprite = plantPetSprite; break;
        }
    }
}