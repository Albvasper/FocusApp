using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private DockManagerUI dockManagerUI;
    [SerializeField] private PricingManagerUI pricingManagerUI;
    
    private PricingManager pricingManager;
    private LeafManager leafManager;
    private EditModeManager editModeManager;
    
    private void Awake() 
    {
        leafManager = GetComponent<LeafManager>();    
        editModeManager = GetComponent<EditModeManager>(); 
        pricingManager = GetComponent<PricingManager>(); 
    }

    public void TryBuyingItem(DecorativeItem item)
    {
        if (item.locked && !pricingManager.IsSubscribedToPomePlus)
        {
            pricingManagerUI.ShowPricingScreen();
            return;    
        }

        if (leafManager.Leafs >= item.cost)
        {
            editModeManager.DeployItem(item);
            leafManager.SubstractLeafs(item.cost);
            dockManagerUI.HideDock();
        }
    }
}
