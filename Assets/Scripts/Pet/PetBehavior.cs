using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PetBehavior : MonoBehaviour
{
    private const float MovementCooldownMin = 1f;
    private const float MovementCooldownMax = 10f;

    public bool CanMove = true;

    private List<Vector3> availablePositions = new();
    private Vector3 targetPosition;
    private float cooldown = 0f;
    private float counter = 0f;

    private void Start()
    {
        // First time move position instantly
        counter = cooldown;
    }
    
    private void FixedUpdate()
    {
        if (CanMove)
        {
            counter += Time.deltaTime;
            if (counter >= cooldown)
            {
                targetPosition = availablePositions[Random.Range(0, availablePositions.Count-1)];
                cooldown = Random.Range(MovementCooldownMin, MovementCooldownMax);
                StopAllCoroutines();
                StartCoroutine(Walk(targetPosition));
                counter = 0;
            }
        }
    }

    public void Initialize(GameObject availablePositionsParent)
    {
        foreach (Transform child in availablePositionsParent.transform)
        {
            availablePositions.Add(child.position);
        }
    }

    private IEnumerator Walk(Vector3 targetPosition)
    {
        Vector3 startingPosition = transform.position;
        float duration = 2;
        float timeElapsed = 0;

        while(timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

}