using UnityEngine;

public class MenuBackButton : MonoBehaviour
{
    // A button that, when pressed takes you to the menu page you previously had open.
    public void Navigate()
    {
        MenuManager.PreviousPage();
    }
}
