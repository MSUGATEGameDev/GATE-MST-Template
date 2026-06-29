using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// -- Menus -- To be placed at the top of the menu, to control the components below it. (NOTE: Only one menu system can exist per scene without errors.)
/// </summary>
public class MenuManager : MonoBehaviour
{
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Menus --\n" +
        "To be placed at the top of the menu, to control the components below it.\n" +
        "(NOTE: Only one menu system can exist per scene without errors.)";
    #endregion

    #region Editable Variables
    [Header("Primary Menu Components")]
    [Tooltip("This is the first page to load when you menu starts.")] [SerializeField]private MenuPage titlePage;
    [Tooltip("This button will appear on any page that's not the title page, to help you get back to the title page.")][SerializeField] private GameObject backButton;
    #endregion

    #region Global Menu Variables
    // These variables are static so they are universally acceptable to all pages, even if they can't find this particular object.
    public static List<GameObject> menuPages = new();   // A list of all menu pages.
    public static MenuPage title_Page;                  // The first page opened.
    public static List<MenuPage> prevPages = new();     // All of the previously opened pages.
    public static GameObject backBtn;                   // The back button, to return to the previous page.
    private void Awake() // The very first function run by a class the instant it is created.
    {
        // Set the default values.
        title_Page = titlePage;
        backBtn = backButton;
        backBtn.SetActive(false);
        OpenPage(title_Page);
    }
    #endregion

    /// <summary>
    /// Open a particular page, closing other pages.
    /// </summary>
    /// <param name="mp">Page to open</param>
    public static void OpenPage(MenuPage mp)
    {
        prevPages.Add(mp);
        foreach (GameObject pg in menuPages)
        {
            if(pg != null)
                pg.SetActive(false);
        }
        mp.gameObject.SetActive(true);
        if (mp == title_Page)
        {
            backBtn.SetActive(false);
            prevPages.Clear();
            prevPages.Add(title_Page);
        }
        else
        {
            backBtn.SetActive(true);
        }
    }
    /// <summary>
    /// Go back to the previous page, all the way back to the title page.
    /// </summary>
    public static void PreviousPage()
    {
        foreach (GameObject pg in menuPages)
        {
            pg.SetActive(false);
        }
        prevPages[prevPages.Count - 2].gameObject.SetActive(true);
        prevPages.RemoveAt(prevPages.Count - 1);
        if (prevPages[prevPages.Count - 1] == title_Page)
        {
            backBtn.SetActive(false);
            prevPages.Clear();
            prevPages.Add(title_Page);
        }
        else
        {
            backBtn.SetActive(true);
        }
    }
}
