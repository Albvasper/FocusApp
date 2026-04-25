using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controls deocration prop in the room.
/// </summary>
public class DecorationObject : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
{
    public SpriteRenderer SpriteRenderer { get; private set; }
    public string ID;
    
    private Vector3 offset;
    private GameObject canvas;
    private EditModeManager editModeManager;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        canvas = transform.GetChild(0).gameObject;
    }

    private void Start() 
    {
        canvas.SetActive(false);
    }

    public void Initialize(EditModeManager editModeManager)
    {
        this.editModeManager = editModeManager;
    }

    /// <summary>
    /// Called when user clicks on prop.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (editModeManager.EditingEnabled)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            canvas.SetActive(true);
            offset = transform.position - mainCamera.ScreenToWorldPoint(eventData.position);
        }
    }

    /// <summary>
    /// Called when user releases click on prop.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData) { }

    /// <summary>
    /// Called when user drags prop.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (editModeManager.EditingEnabled)
            transform.position = mainCamera.ScreenToWorldPoint(eventData.position) + offset;
    }
    
    /// <summary>
    /// Called when selecting prop.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnSelect(BaseEventData eventData) {}

    /// <summary>
    /// Called when user deselects prop/
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDeselect(BaseEventData eventData)
    {
        canvas.SetActive(false);
    }

    /// <summary>
    /// Flips sprite in X axis.
    /// </summary>
    public void FlipDecoration()
    {
        if (SpriteRenderer.flipX)
            SpriteRenderer.flipX = false;
        else
            SpriteRenderer.flipX = true;
    }
}