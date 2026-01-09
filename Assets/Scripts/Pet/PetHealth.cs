using System.Data.Common;
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

    [SerializeField] private UiManager uiManager;
    [SerializeField] private PetDataManager petDataManager;
    private int health;
    private int maxHealth;

    private void Awake()
    {
        maxHealth = PetData.MaxHealth;
    }

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
