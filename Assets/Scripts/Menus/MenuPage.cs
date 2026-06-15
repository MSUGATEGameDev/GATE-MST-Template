using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : MonoBehaviour
{
    // A simple system that lets you open and close menu pages

    public static List<GameObject> menuPages = new(); // A publicly available list of all menu pages.
    public static MenuPage titlePage;       // The first page.
    public static List<MenuPage> prevPages = new(); // All of the previously opened pages.
    public static GameObject back;    // The back button, to return to the previous page.
    public static void OpenPage(MenuPage mp) // Close other menu pages and open the indicated page.
    {
        prevPages.Add(mp);
        foreach(GameObject pg in menuPages)
        {
            pg.SetActive(false);
        }
        mp.gameObject.SetActive(true);
        if(mp == titlePage)
        {
            back.SetActive(false);
            prevPages.Clear();
            prevPages.Add(titlePage);
        }
        else
        {
            back.SetActive(true);
        }
    }
    public static void PreviousPage() // Opens the page you just came from.
    {
        foreach (GameObject pg in menuPages)
        {
            pg.SetActive(false);
        }
        prevPages[prevPages.Count - 2].gameObject.SetActive(true);
        prevPages.RemoveAt(prevPages.Count - 1);
        if (prevPages[prevPages.Count - 1] == titlePage)
        {
            back.SetActive(false);
            prevPages.Clear();
            prevPages.Add(titlePage);
        }
        else
        {
            back.SetActive(true);
        }
    }
    protected virtual void Start()
    {
        menuPages.Add(gameObject);
        if (titlePage.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        
    }
}
