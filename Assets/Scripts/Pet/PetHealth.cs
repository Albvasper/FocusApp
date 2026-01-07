using UnityEngine;

/// <summary>
/// Manages pet health and death.
/// </summary>
public class PetHealth : MonoBehaviour
{

    /*
        Pet can die by:
            Old age
            Unhappiness: Canceling focus timer too much or not going into the app much?
    */

    private int health;
    private int maxHealth = 20;

    [SerializeField] private UiManager uiManager;

    public void SetHealth(int currentHealth)
    {
        health = currentHealth;
        uiManager.UpdateHealthBar(health, maxHealth);
    }

    public void Heal()
    {
        if (health < maxHealth)
        {
            health++;
            uiManager.UpdateHealthBar(health, maxHealth);
        }
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Die();
            uiManager.UpdateHealthBar(health, maxHealth);
        }
    }

    public void Die()
    {
    }
}
