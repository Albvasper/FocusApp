using System.Collections;
using UnityEngine;

/// <summary>
/// Manages pet health and death.
/// </summary>
public class PetHealth : MonoBehaviour
{
    public int health;

    private int maxHealth = 100;
    private PetCardManagerUI petCardManagerUI;
    private TimeManager timeManager;
    private PetBehavior behavior;

    private void Awake()
    {
        behavior = GetComponent<PetBehavior>();
    }

    public void Initialize(PetCardManagerUI petCardManagerUI, TimeManager timeManager)
    {
        this.petCardManagerUI = petCardManagerUI;
        this.timeManager = timeManager;
    }

    public void SetHealth(int currentHealth)
    {
        health = currentHealth;
        petCardManagerUI.UpdateHealthBar(health, maxHealth);
    }   

    public void Heal()
    {
        if (health < maxHealth)
        {
            health++;
            //TODO: Save health
            petCardManagerUI.UpdateHealthBar(health, maxHealth);
        }
    }

    public void TakeDamage()
    {
        health--;
        //TODO: Save health
        petCardManagerUI.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            //Die();
        }
    }

    private IEnumerator DeathAnimation()
    {
        float duration = 8f;
        float timeElapsed = 0;
        Vector3 startingPosition = transform.position;
        Vector3 targetPosition = new(0f, 45f, 0f);

        while(timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        //petCardManagerUI.ShowDeadPetScreen();
    }
}
