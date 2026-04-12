using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private UiManager uiManager;
    private LeafManager leafManager;
    private EditModeManager editModeManager;

    private void Awake() 
    {
        uiManager = GetComponent<UiManager>();     
        leafManager = GetComponent<LeafManager>();    
        editModeManager = GetComponent<EditModeManager>(); 
    }

    public void TryBuyingItem(DecorativeItem item)
    {
        if (leafManager.Leafs >= item.cost)
        {
            editModeManager.DeployItem(item);
            leafManager.SubstractLeafs(item.cost);
            uiManager.ShowHUD();
        }
    }
}
