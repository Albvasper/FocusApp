using UnityEngine;

public class PricingManager : MonoBehaviour
{
    [SerializeField] private PricingManagerUI pricingManagerUI;

    public bool IsSubscribedToPomePlus { get; private set;}

    private void Start() 
    {
        // Check if subsribed to pome+
        IsSubscribedToPomePlus = false;
    }

    public void SubscribeToPomePlus()
    {
        IsSubscribedToPomePlus = true;
    }
}
