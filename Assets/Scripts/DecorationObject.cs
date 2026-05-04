using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Controls deocration prop in the room.
/// </summary>
public class DecorationObject : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
{
    public SpriteRenderer SpriteRenderer { get; private set; }
    public string ID;
    
    [SerializeField] private Button frontLayerButton;
    [SerializeField] private Button bgLayerButton;

    private int propsForegroundLayer;
    private int propBackGroundLayer;
    private Vector3 offset;
    private GameObject canvas;
    private EditModeManager editModeManager;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        propsForegroundLayer = SortingLayer.NameToID("PropsForeground");
        propBackGroundLayer = SortingLayer.NameToID("PropsBackground");
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

    /// <summary>
    /// Apply prop foreground sorting layer to sprite renderer.
    /// </summary>
    public void BringToFrontLayer()
    {
        SpriteRenderer.sortingLayerID = propsForegroundLayer;
        frontLayerButton.enabled = false;
        bgLayerButton.enabled = true;
    }

    /// <summary>
    /// Apply prop background sorting layer to sprite renderer.
    /// </summary>
    public void BringToBackgroundLayer()
    {
        SpriteRenderer.sortingLayerID = propBackGroundLayer;
        frontLayerButton.enabled = true;
        bgLayerButton.enabled = false;
    }
}