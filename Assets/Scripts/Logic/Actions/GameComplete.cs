using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameAction -- Ends the game and takes the player to the credits page.
/// </summary>
public class GameComplete : GameAction
{
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "Ends the game and takes the player to the credits page.";
    #endregion
    public override void Activate()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MenuManager.openCreditsPage = true;
        SceneManager.LoadScene("Menu");
    }
    public override void Deactivate()
    {
        // Does Nothing
    }
}
