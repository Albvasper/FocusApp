using UnityEngine;

public class PetHealth : MonoBehaviour
{
    [Range(0, 4)][SerializeField] private int maxHealth = 4;
    public int Health { get; private set; }

    private void Start()
    {
        Health  = maxHealth;
    }

    public void Heal()
    {
        if (Health < maxHealth)
            Health++;
    }

    public void TakeDamage()
    {
        Health--;
        if (Health <= 0)
            Die();
    }

    private void Die()
    {
    }
}
