using TMPro;
using UnityEngine;

/// <summary>
/// Updates leaf counter with accurate data.
/// </summary>
public class LeafManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject leafCounterCard;
    [SerializeField] private TextMeshProUGUI leafCounterText;

    public void UpdateLeafCounterText(int leafAmount)
    {
        leafCounterText.text = leafAmount.ToString();
    }

    public void ShowLeafCounterCard()
    {
        leafCounterCard.SetActive(true);
    }

    public void HideLeafCounterCard()
    {
        leafCounterCard.SetActive(false);
    }
}
