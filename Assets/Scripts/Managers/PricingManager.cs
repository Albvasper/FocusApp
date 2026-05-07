using UnityEngine;

public class PricingManager : Purchases.UpdatedCustomerInfoListener
{
    [SerializeField] private PricingManagerUI pricingManagerUI;

    public bool IsSubscribedToPomePlus { get; private set;}
    public Purchases Purchases { get; private set; }

    private void Awake() 
    {
        Purchases = GetComponent<Purchases>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start() 
    {
        Purchases.SetDebugLogsEnabled(true);
        Purchases.GetCustomerInfo((customerInfo, error) =>
        {
            if (error != null) return;
            CheckAccess(customerInfo);
        });

        Purchases.GetOfferings((offerings, error) =>
        {
            if (error != null)
            {
                // show error
            }
            else
            {
                if (offerings.Current == null) return;

                foreach (var package in offerings.Current.AvailablePackages)
                {
                    // Show offerings in pricing screen
                    // if (package.PackageType == Purchases.PackageType.Monthly)
                    // {
                    //     monthlyPriceText.text = package.StoreProduct.PriceString; // e.g. "$3.99/month"
                    //     monthlyButton.onClick.AddListener(() => BeginPurchase(package));
                    // }
                    // else if (package.PackageType == Purchases.PackageType.Annual)
                    // {
                    //     yearlyPriceText.text = package.StoreProduct.PriceString; // e.g. "$29.99/year"
                    //     yearlyButton.onClick.AddListener(() => BeginPurchase(package));
                    // }
                }
            }
        });
    }

    public override void CustomerInfoReceived(Purchases.CustomerInfo customerInfo)
    {
        CheckAccess(customerInfo);
    }

    public void BeginPurchase(Purchases.Package package)
    {
        Purchases.PurchasePackage(package, (purchaseResult) =>
        {
            if (purchaseResult.Error != null)
            {
                Debug.LogError("Purchase error: " + purchaseResult.Error.Message);
                pricingManagerUI.ShowErrorScreen();
            }
            else if (purchaseResult.UserCancelled)
            {
                // User cancelled, dont show error
                pricingManagerUI.HidePricingScreen();
            }
            else
            {
                CheckAccess(purchaseResult.CustomerInfo);
                pricingManagerUI.ShowPurchaseSuccessfulScreen();
            }
        });
    }

    public void RestorePruchases()
    {
        Purchases.RestorePurchases((customerInfo, error) =>
        {
            if (error != null)
            {
                pricingManagerUI.ShowErrorScreen();
            }
            else
            {
                CheckAccess(customerInfo);
                pricingManagerUI.ShowPurchaseRestoredScreen();
            }
        });
    }

    private void CheckAccess(Purchases.CustomerInfo customerInfo)
    {
        if (customerInfo.Entitlements.Active.ContainsKey("pome_plus"))
        {
            Debug.Log("Pome+ active");
            IsSubscribedToPomePlus = true;
        }
        else
        {
            Debug.Log("Pome+ not active");
            IsSubscribedToPomePlus = false;
        }
    }
}