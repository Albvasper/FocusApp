using UnityEngine;
using System.Collections;

public enum PetType
{
    Pet1,
    Pet2,
    Pet3
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

    private UiHatchingManager uiHatchingManager;

    private void Awake()
    {
        uiHatchingManager = GetComponent<UiHatchingManager>();
    }

    private void Start()
    {
        GenerateNewPet();
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

    private void GenerateNewPet()
    {
        PetType petType = (PetType)Random.Range(0, 3);
        petData.PetType = petType;
        petData.PetAge = 1;
        petData.PetCurrentHealth = PetData.MaxHealth;
    }
}