using UnityEngine;
using UnityEngine.UI;

public class LevelPellet : MonoBehaviour
{
    [SerializeField] private GameObject pellet;

    public void ShowPellet()
    {
        pellet.gameObject.SetActive(true);
    }

    public void HidePellet()
    {
        pellet.gameObject.SetActive(false);
    }
}
