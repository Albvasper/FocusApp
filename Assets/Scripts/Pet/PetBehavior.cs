using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum State
{
    IDLE,
    Focusing
}

public class PetBehavior : MonoBehaviour
{
    private const float MovementCooldownMin = 1f;
    private const float MovementCooldownMax = 10f;

    public bool CanMove = true;

    private State currentState = State.IDLE;
    private List<Vector3> availablePositions = new();
    private Vector3 targetPosition;
    private float cooldown = 0f;
    private float counter = 0f;
    private PetAnimator PetAnimator;

    private void Awake() 
    {
        PetAnimator = GetComponent<PetAnimator>();    
    }

    private void Start()
    {
        // First time move position instantly
        counter = cooldown;
    }
    
    private void FixedUpdate()
    {
        switch (currentState)
        {
            case State.IDLE: IDLEBehavior(); break;

            case State.Focusing: FocusingBehavior(); break;   
        }
    }

    public void Initialize(GameObject availablePositionsParent)
    {
        foreach (Transform child in availablePositionsParent.transform)
        {
            availablePositions.Add(child.position);
        }
    }

    /// <summary>
    /// Change current state of pet
    /// </summary>
    /// <param name="state">New state</param>
    public void SetState(State state)
    {
        currentState = state;
    }

    // When movement cooldown is expired, pick a random point and walk to it.
    private void IDLEBehavior()
    {
        PetAnimator.IsIDLEing();
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

    private void FocusingBehavior()
    {
        PetAnimator.IsFocusing();
    }

    // Move pet to a target position
    private IEnumerator Walk(Vector3 targetPosition)
    {
        Vector3 startingPosition = transform.position;
        float duration = 2;
        float timeElapsed = 0;

        while(timeElapsed < duration)
        {
            PetAnimator.IsWalking();
            transform.position = Vector3.Lerp(startingPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        PetAnimator.IsIDLEing();
    }

}