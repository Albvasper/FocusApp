using UnityEngine;

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

    public bool EditingEnabled { get ; set; } = false;

    public void DeployItem(DecorativeItem decoration)
    {
        DecorationObject decorationObject;
        GameObject decorationObjectGO = Instantiate(decoration.item, Vector3.zero, Quaternion.identity);
        decorationObject = decorationObjectGO.GetComponent<DecorationObject>();
        decorationObject.Initialize(this);
    }
}
