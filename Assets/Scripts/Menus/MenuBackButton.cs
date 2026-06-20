using UnityEngine;

/// <summary>
/// Takes you back to the previous page.
/// </summary>
public class MenuBackButton : MonoBehaviour
{
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Menus --\n" +
        "Takes you back to the previous page.";
    #endregion
    public void Navigate()
    {
        MenuManager.PreviousPage();
    }
}
