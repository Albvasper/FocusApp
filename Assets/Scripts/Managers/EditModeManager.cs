using UnityEngine;
using System.Collections.Generic;

public class EditModeManager : MonoBehaviour
{
    /*
        2 ways:
            Bought an item:
                - Spawn item in room
                - drag and drop item to place it
                - mini menu up top to flip it
                - click on X to get out of edit mode

            Edit mode:
                - drag and drop item to place it
                - mini menu up top to flip it
                - click on X to get out of edit mode
    */

    public bool EditingEnabled { get ; private set; } = false;
    public List<DecorationObject> Decorations = new();

    [SerializeField] private Color standardBGColor;
    [SerializeField] private Color editModeBGColor;

    private GameObject pet;
    private Camera mainCamera;

    private void Awake() 
    {
        mainCamera = Camera.main;    
    }

    public void Initialize(GameObject pet)
    {
        this.pet = pet;
    }

    public void EnterEditMode()
    {
        pet.SetActive(false);
        EditingEnabled = true;
        mainCamera.backgroundColor = editModeBGColor;
    }
    
    public void ExitEditMode()
    {
        pet.SetActive(true);
        EditingEnabled = false;
        mainCamera.backgroundColor = standardBGColor;
        SaveManager.Instance.SaveDecorationChanges();
    }

    public void DeployItem(DecorativeItem decoration)
    {
        DecorationObject decorationObject;
        GameObject decorationObjectGO = Instantiate(decoration.item, Vector3.zero, Quaternion.identity);
        
        decorationObject = decorationObjectGO.GetComponent<DecorationObject>();
        Decorations.Add(decorationObject);
        decorationObject.Initialize(this);
        SaveManager.Instance.SaveDecoration(decorationObject);
    }
}
