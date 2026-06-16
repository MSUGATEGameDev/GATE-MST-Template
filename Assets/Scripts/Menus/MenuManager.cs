using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Place somewhere outside of the menu to allow you to control the menu.
    [Header("Primary Menu Components")]
    [Tooltip("This is the first page to load when you menu starts.")] [SerializeField]private MenuPage titlePage;
    [Tooltip("This button will appear on any page that's not the title page, to help you get back to the title page.")][SerializeField] private GameObject backButton;


    // These variables are static so they are universally acceptable to all pages, even if they can't find this particular object.
    public static List<GameObject> menuPages = new();   // A list of all menu pages.
    public static MenuPage title_Page;                  // The first page opened.
    public static List<MenuPage> prevPages = new();     // All of the previously opened pages.
    public static GameObject backBtn;                   // The back button, to return to the previous page.
    private void Start()
    {
        // Set the default values.
        title_Page = titlePage;
        backBtn = backButton;
        backBtn.SetActive(false);
        OpenPage(title_Page);
    }
    public static void OpenPage(MenuPage mp) // Opens a menu page.
    {
        prevPages.Add(mp);
        foreach (GameObject pg in menuPages)
        {
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
    public static void PreviousPage() // Opens the page you just came from, back to the title page.
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
