using UnityEngine;
using UnityEngine.UI;

public class PricingManagerUI : MonoBehaviour
{   
    public bool SubscribedToPomePlus; //{ get; private set; }
    
    [SerializeField] private GameObject pricingScreen;
    [SerializeField] private Toggle monthlyPlanToggle;
    [SerializeField] private Toggle annuallyPlanToggle;

    private bool monthlyPlanSelected;

    public void Start()
    {
        SubscribedToPomePlus = false;
        HidePricingScreen();
        SelectMonthlyPlan();
    }
    
    public void ShowPricingScreen()
    {
        pricingScreen.SetActive(true);
    }

    public void HidePricingScreen()
    {
        pricingScreen.SetActive(false);
    }

    public void SelectMonthlyPlan()
    {
        monthlyPlanSelected = true;
        monthlyPlanToggle.isOn = true;
        annuallyPlanToggle.isOn = false;
    }

    public void SelectAnnuallyPlan()
    {
        monthlyPlanSelected = false;
        monthlyPlanToggle.isOn = false;
        annuallyPlanToggle.isOn = true;
    }

    public void GoToCheckout()
    {
        //TODO: Subscription
        HidePricingScreen();
        SubscribedToPomePlus = true;
    }

    public void Cancel()
    {
        HidePricingScreen();
    }
}
