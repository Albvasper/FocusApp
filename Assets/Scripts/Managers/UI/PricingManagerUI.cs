using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PricingManagerUI : MonoBehaviour
{   
    [Header("Screens")]
    [SerializeField] private GameObject pricingScreen;
    [SerializeField] private GameObject errorMessageScreen;
    [SerializeField] private GameObject purchaseSuccessfulScreen;
    [SerializeField] private GameObject purchaseRestoredScreen;
    [Header("Components")]
    [SerializeField] private Toggle monthlyPlanToggle;
    [SerializeField] private Toggle annuallyPlanToggle;
    [Header("Manager")]
    [SerializeField] private PricingManager pricingManager;

    private bool monthlyPlanSelected;
    private Purchases.Package monthlyPackage;
    private Purchases.Package yearlyPackage;

    private void OnEnable() 
    {
        /*
        pricingManager.Purchases.GetOfferings((offerings, error) =>
        {
            if (error != null || offerings.Current == null) return;

            foreach (var package in offerings.Current.AvailablePackages)
            {
                if (package.PackageType == Purchases.PackageType.Monthly)
                    monthlyPackage = package;
                else if (package.PackageType == Purchases.PackageType.Annual)
                    yearlyPackage = package;
            }
        });
        */
    }

    public void Start()
    {
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

    public void ShowErrorScreen()
    {
        HidePricingScreen();
        errorMessageScreen.SetActive(true);
    }

    public void HideErrorScreen()
    {
        errorMessageScreen.SetActive(false);
    }

    public void ShowPurchaseRestoredScreen()
    {
        HidePricingScreen();
        purchaseRestoredScreen.SetActive(true);
    }

    public void HidePurchaseRestoredScreen()
    {
        purchaseRestoredScreen.SetActive(false);
    }

    public void ShowPurchaseSuccessfulScreen()
    {
        HidePricingScreen();
        purchaseSuccessfulScreen.SetActive(true);
    }

    public void HidePurchaseSuccessfulScreen()
    {
        purchaseSuccessfulScreen.SetActive(false);
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
        if (monthlyPlanSelected)
        {
            if (monthlyPackage == null) return;
            pricingManager.BeginPurchase(monthlyPackage);
        }
        else
        {
            if (yearlyPackage == null) return;
            pricingManager.BeginPurchase(yearlyPackage);
        }

    }

    public void RestorePruchases()
    {
        pricingManager.RestorePruchases();
    }
}
