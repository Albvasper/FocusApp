using UnityEngine;

/// <summary>
/// Keeps track of player leafs (currency)
/// </summary>
public class LeafManager : MonoBehaviour
{
    public int Leafs { get; private set; } = 999;

    [SerializeField] private LeafManagerUI leafManagerUI;

    public void Start()
    {
        leafManagerUI.UpdateLeafCounterText(Leafs);
    }

    public void AddLeafs(int amount)
    {
        Leafs += amount;
        leafManagerUI.UpdateLeafCounterText(Leafs);
    }
    
    public void SubstractLeafs(int amount)
    {
        Leafs -= amount;
        leafManagerUI.UpdateLeafCounterText(Leafs);
    }
}
