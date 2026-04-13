using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private DockManagerUI dockManagerUI;
    private LeafManager leafManager;
    private EditModeManager editModeManager;

    private void Awake() 
    {
        leafManager = GetComponent<LeafManager>();    
        editModeManager = GetComponent<EditModeManager>(); 
    }

    public void TryBuyingItem(DecorativeItem item)
    {
        if (leafManager.Leafs >= item.cost)
        {
            editModeManager.DeployItem(item);
            leafManager.SubstractLeafs(item.cost);
            dockManagerUI.HideDock();
        }
    }
}
