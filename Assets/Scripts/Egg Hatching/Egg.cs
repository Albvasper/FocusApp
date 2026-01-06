using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum PetType
{
    Pet1,
    Pet2,
    Pet3
}

/// <summary>
/// Assigns pet type and hatches it.
/// </summary>
public class Egg : MonoBehaviour
{
    [SerializeField] private int hatchingTimeMin = 10;
    [SerializeField] private int hatchingTimeMax = 25;

    private PetType petType;
    private EggAnimator animator;

    private void Awake()
    {
        animator = GetComponent<EggAnimator>();
    }

    private void Start()
    {
        petType = (PetType)Random.Range(0, 3);
        StartCoroutine(StartHatching());
    }

    private IEnumerator StartHatching()
    {
        int hatchingTime = Random.Range(hatchingTimeMin, hatchingTimeMax);
        yield return new WaitForSeconds(hatchingTime);
        StartCoroutine(Hatch());
    }

    private IEnumerator Hatch()
    {
        int delay = 6;
        animator.HatchEgg();
        // Pass pet type!

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("PetRoom");
    }
}