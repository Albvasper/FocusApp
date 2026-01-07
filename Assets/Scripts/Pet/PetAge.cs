using UnityEngine;

public class PetAge : MonoBehaviour
{
    public int Age = 0;
    
    private int maxAge = 4;
    private PetHealth petHealth;

    private void Awake()
    {
        petHealth = GetComponent<PetHealth>();
    }

    public void MakePetAge()
    {
        Age++;
        if (Age >= maxAge)
            petHealth.Die();
    }
}
