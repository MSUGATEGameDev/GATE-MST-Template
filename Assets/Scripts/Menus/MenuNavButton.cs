using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// -- Menus -- Opens a scene, takes you to a menu page, opens menus on a menu page, or quits the game.
/// </summary>
public class MenuNavButton : MonoBehaviour
{
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Menus --\n" +
        "Opens a scene, takes you to a menu page, opens menus on a menu page, or quits the game.";
    #endregion

    #region Inspector-Editable Variables
    public enum MenuButtonType { Page, Scene, Submenu, Quit};
    
    public MenuButtonType buttonType;
    [Header("For Page Buttons")]
    [Tooltip("This button will navigate to this menu page when pressed.")]public MenuPage pageToOpen;
    
    [Header("For Scene Buttons")]
    [Tooltip("This button will load this scene when pressed.")] public string sceneToOpen;
    
    [Header("For Submenu Buttons")]
    [Tooltip("These things will appear when pressed.")] public List<GameObject> objectsToShow;
    [Tooltip("These things will disappear when pressed.")] public List<GameObject> objectsToHide;
    #endregion

    /// <summary>
    /// Opens the thing the button was told to open.
    /// </summary>
    public void Navigate()
    {
        switch (buttonType)
        {
            case MenuButtonType.Page:
                MenuManager.OpenPage(pageToOpen);
                
                break;
            case MenuButtonType.Scene:
                SceneManager.LoadScene(sceneToOpen);
                break;
            case MenuButtonType.Submenu:
                foreach (GameObject go in objectsToShow)
                {
                    go.SetActive(true);
                }
                foreach (GameObject go in objectsToHide)
                {
                    go.SetActive(false);
                }
                break;
            case MenuButtonType.Quit:
                Application.Quit();
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #endif
                break;
        }
        
    }
}
