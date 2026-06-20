using UnityEngine;

/// <summary>
/// -- Tool -- Attach to any visual object to allow it to be seen in the editor but not the game.
/// </summary>
public class HideForGame:MonoBehaviour
{
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Tool --\n" +
        "Attach to any visual object to allow it to be seen in the editor but not the game.";
    #endregion
    private void Start() // The last function run before the object first appears in the game.
    {
        GetComponent<Renderer>().enabled = false; // Hide the visual.
    }
}
