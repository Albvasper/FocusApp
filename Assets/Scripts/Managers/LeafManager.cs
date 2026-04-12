using UnityEngine;

/// <summary>
/// Keeps track of player leafs (currency)
/// </summary>
public class LeafManager : MonoBehaviour
{
    public int Leafs { get; private set; }= 999;
    private UiManager uiManager;
    
    private void Awake()
    {
        uiManager = GetComponent<UiManager>();
    }

    public void Start()
    {
        uiManager.UpdateLeafCounterText(Leafs);
    }

    public void AddLeafs(int amount)
    {
        Leafs += amount;
        uiManager.UpdateLeafCounterText(Leafs);
    }
    
    public void SubstractLeafs(int amount)
    {
        Leafs -= amount;
        uiManager.UpdateLeafCounterText(Leafs);
    }
}
