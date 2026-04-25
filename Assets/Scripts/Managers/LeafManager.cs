using UnityEngine;

/// <summary>
/// Keeps track of player leafs (currency)
/// </summary>
public class LeafManager : MonoBehaviour
{
    public int Leafs { get; private set; } = 0;

    [SerializeField] private LeafManagerUI leafManagerUI;

    public void SetLeafs(int amount)
    {
        Leafs = amount;
        leafManagerUI.UpdateLeafCounterText(Leafs);
        SaveManager.Instance.SaveLeafs(Leafs);
    }

    public void AddLeafs(int amount)
    {
        Leafs += amount;
        leafManagerUI.UpdateLeafCounterText(Leafs);
        SaveManager.Instance.SaveLeafs(Leafs);
    }
    
    public void SubstractLeafs(int amount)
    {
        Leafs -= amount;
        leafManagerUI.UpdateLeafCounterText(Leafs);
        SaveManager.Instance.SaveLeafs(Leafs);
    }
}
