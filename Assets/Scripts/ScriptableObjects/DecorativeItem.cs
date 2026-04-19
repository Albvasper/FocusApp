using UnityEngine;

[CreateAssetMenu(fileName = "DecorativeItem", menuName = "Scriptable Objects/DecorativeItem")]
public class DecorativeItem : ScriptableObject
{
    public bool locked;
    public int cost;
    public GameObject item;
}
