using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : MonoBehaviour
{
    void Awake()
    {
        MenuManager.menuPages.Add(gameObject);
    }
}
