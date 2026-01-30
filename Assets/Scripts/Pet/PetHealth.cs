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

    public int health;
    private int maxHealth = PetData.MaxHealth;
    private UiManager uiManager;
    private TimeManager timeManager;

    private void Start()
    {
        if (health <= 0) Die();
    }
    public void Initialize(UiManager uiManager, TimeManager timeManager)
    {
        this.uiManager = uiManager;
        this.timeManager = timeManager;
    }

    public void SetHealth(int currentHealth)
    {
        health = currentHealth;
        uiManager.UpdateHealthBar(health, maxHealth);
        uiManager.UpdateFocusScreenHealthBar(health, maxHealth);
    }

    public void Heal()
    {
        if (health < maxHealth)
        {
            health++;
            PetDataManager.Instance.SaveHealth(health);
            uiManager.UpdateHealthBar(health, maxHealth);
            uiManager.UpdateFocusScreenHealthBar(health, maxHealth);
        }
    }

    public void TakeDamage()
    {
        Debug.Log("WTF");
        health--;
        PetDataManager.Instance.SaveHealth(health);
        uiManager.UpdateHealthBar(health, maxHealth);
        uiManager.UpdateFocusScreenHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        uiManager.ShowDeadPetScreen();
        timeManager.StopTimer();
    }
}
