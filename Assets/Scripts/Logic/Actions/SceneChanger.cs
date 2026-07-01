using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game Action -- Changes the scene when activated.
/// </summary>
public class SceneChanger : GameAction
{

    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "Changes the scene when activated.";
    [Tooltip("This scene will be opened when activated.")]public string sceneToOpen;
    public override void Activate()
    {
        if(sceneToOpen == "Menu")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        SceneManager.LoadScene(sceneToOpen);
    }

    public override void Deactivate()
    {

    }
}
