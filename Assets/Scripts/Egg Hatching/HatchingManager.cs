using UnityEngine;
using System.Collections;

public class HatchingManager : MonoBehaviour
{
    [SerializeField] private int hatchingTimeMin = 10;
    [SerializeField] private int hatchingTimeMax = 25;

    private PetType petType;

    private void Start()
    {
        petType = (PetType)Random.Range(0, 3);
        StartCoroutine(StartHatching());
    }

    private IEnumerator StartHatching()
    {
        float hatchingTime = Random.Range(hatchingTimeMin, hatchingTimeMax);
        yield return new WaitForSeconds(hatchingTime);
        Hatch();
    }

    private void Hatch()
    {
    }
}
