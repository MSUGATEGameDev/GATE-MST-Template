using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

/// <summary>
/// GameTrigger -- Allows a door to auto-open when the player is present or be locked to color-coded keys.
/// </summary>
public class DoorController : GameTrigger
{
    #region Description for Unity Inpsector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameTrigger --\n" +
    "Allows a door to auto-open when the player is present or be locked to color-coded keys.";
    #endregion

    #region Insepctor-Editable Variables
    [Header("Door Settings")]
    [Tooltip("If true, the door will automatically open when the player enters the vecinity with the correct key selected.")] public bool lockedWithKey = false;
    [Tooltip("The key color which unlocks the door.")]public ColorManager.StandardColor keyColor;
    
    [Header("Components")]
    public GameObject lockIcons;
    #endregion

    #region Internal Variables
    bool stillLocked = false;
    #endregion

    private void Start() // The last thing to run right before the object is placed in the game.
    { 
        if (lockedWithKey)
        {
            stillLocked = true;
            lockIcons.SetActive(true);
            Material[] mat = new Material[2] { ColorManager.singleton.standardHolos[(int)keyColor], ColorManager.singleton.holoBlack };
            foreach (Renderer rend in lockIcons.GetComponentsInChildren<Renderer>())
            {
                rend.materials = mat;
            }
        }
        
    }

    void OnTriggerEnter(Collider other) // When something enters the trigger collider associated with this object.
    {
        if (other.CompareTag("Player"))
        {
            if (lockedWithKey && stillLocked)
            {
                if (ObjectivesManager.CheckKey(keyColor))
                {
                    HUD.DisplayNotice("Door Unlocked");
                    lockIcons.SetActive(false);
                    stillLocked = false;
                    ActivateItems();
                }
                else
                {
                    HUD.DisplayNotice(keyColor.ToString() + " key needed.");
                }
            }
            else
            {
                ActivateItems();
            }
        }
    }
    void OnTriggerExit(Collider other) // When something enters the trigger collider associated with this object.
    {
        if (!stillLocked)
        {
            DeactivateItems();
        }
    }
}
