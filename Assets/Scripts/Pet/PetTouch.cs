using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Shows timer screen when pet is touched
/// </summary>
public class PetTouch : MonoBehaviour, IPointerDownHandler
{
    private TimerManagerUI timerManagerUI;

    public void Initialize(TimerManagerUI timerManagerUI)
    {
        this.timerManagerUI = timerManagerUI;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        timerManagerUI.ShowTimerScreen();
    }
}
