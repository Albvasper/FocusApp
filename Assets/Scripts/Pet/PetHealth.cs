using System.Collections;
using UnityEngine;

/// <summary>
/// Manages pet health and death.
/// </summary>
public class PetHealth : MonoBehaviour
{
    public int health;

    [SerializeField] private GameObject halo;
    [SerializeField] private GameObject wings;

    private int maxHealth = PetData.MaxHealth;
    private UiManager uiManager;
    private TimeManager timeManager;
    private PetBehavior behavior;

    private void Awake()
    {
        behavior = GetComponent<PetBehavior>();
    }

    private void Start()
    {
        halo.SetActive(false);
        wings.SetActive(false);

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
    }   

    public void Heal()
    {
        if (health < maxHealth)
        {
            health++;
            PetDataManager.Instance.SaveHealth(health);
            uiManager.UpdateHealthBar(health, maxHealth);
        }
    }

    public void TakeDamage()
    {
        health--;
        PetDataManager.Instance.SaveHealth(health);
        uiManager.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        behavior.CanMove = false;
        timeManager.StopTimer();
        uiManager.HideUI();
        PlaceWingsAndHalo();
        StartCoroutine(DeathAnimation());
        AudioManager.Instance.PlayHeavenlyChoir();
    }

    private void PlaceWingsAndHalo()
    {
        halo.SetActive(true);
        wings.SetActive(true);
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
        uiManager.ShowDeadPetScreen();
    }
}
