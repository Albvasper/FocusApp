using UnityEngine;

/// <summary>
/// Keeps track of player leafs (currency)
/// </summary>
public class LeafManager : MonoBehaviour
{
    private int leafs = 0;
    private UiManager uiManager;
    
    private void Awake()
    {
        uiManager = GetComponent<UiManager>();
    }

    public void AddLeafs(int amount)
    {
        leafs += amount;
        uiManager.UpdateLeafCounterText(leafs);
    }
    
    public void SubstractLeafs(int amount)
    {
        leafs -= amount;
        uiManager.UpdateLeafCounterText(leafs);
    }
}
