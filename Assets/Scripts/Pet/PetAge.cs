using UnityEngine;

public class PetAge : MonoBehaviour
{
    public int LifeStage;
    public int Age;
    
    private int maxLifeStage = PetData.MaxLifeStage;
    private int agingRate = PetData.MaxAge;                 // Every x years, pet will go up one life stage
    private PetHealth petHealth;

    private void Awake()
    {
        petHealth = GetComponent<PetHealth>();
    }

    // TODO: Apply visual change when loading or chaning Life stage

    public void MakePetAge()
    {
        Age++;
        PetDataManager.Instance.SaveAge(Age);
        if (Age >= agingRate)
        {
            Age = 0;
            LifeStage++;
            PetDataManager.Instance.SaveLifeStage(LifeStage);
            if (LifeStage >= maxLifeStage)
                petHealth.Die();
        }
    }
}