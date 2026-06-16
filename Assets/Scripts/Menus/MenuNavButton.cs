using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuNavButton : MonoBehaviour
{
    public enum MenuButtonType { Page, Scene, Submenu};
    
    public MenuButtonType buttonType;
    [Header("For Page Buttons")]
    [Tooltip("This button will navigate to this menu page when pressed.")]public MenuPage pageToOpen;
    
    [Header("For Scene Buttons")]
    [Tooltip("This button will navigate to this scene when pressed.")] public string sceneToOpen;
    
    [Header("For Submenu Buttons")]
    [Tooltip("These things will appear when pressed.")] public List<GameObject> objectsToShow;
    [Tooltip("These things will disappear when pressed.")] public List<GameObject> objectsToHide;
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
        }
        
    }
}
