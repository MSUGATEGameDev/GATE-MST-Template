using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Controls the player and handles some global variables for the game.

    // This is a Singleton
    // Call the only PlayerController in  the game with PlayerController.singleton
    #region Singleton Setup
    // Creating a unique Singleton of the PlayerController.
    public static PlayerController singleton;

    private void Awake()
    {
        // Check if an instance already exists.
        if (singleton != null && singleton != this)
        {
            // If another instance exists, destroy this one to ensure only one remains.
            Destroy(this.gameObject);
        }
        else
        {
            // If no instance exists, set this one as the instance.
            singleton = this;
            // Optional: use DontDestroyOnLoad to persist across scene changes.
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    // Hidden Variables
    [HideInInspector]public GameTrigger triggerInRange;

    #region Input Functions
    void OnActionButton(InputAction.CallbackContext context)
    {
        if(triggerInRange != null)
        {
            triggerInRange.Activate();
        }
    }
    #endregion
}
