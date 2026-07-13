using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ShowOrHide : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "Makes objects appear or disappear on activation.";
    public List<GameObject> objectsToShow = new();
    public List<GameObject> objectsToHide = new();
    [Tooltip("When deactivated, unhide the hidden objects and hide the shown objects.")]public bool undoOnDeactivate = false;

    public override void Activate()
    {
        foreach (GameObject go in objectsToShow)
        {
            if(go != null)
            go.SetActive(true);
        }
        foreach (GameObject go in objectsToHide)
        {
            if(go!=null)
            go.SetActive(false);
        }
    }

    public override void Deactivate()
    {
        if (undoOnDeactivate)
        {
            foreach (GameObject go in objectsToShow)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in objectsToHide)
            {
                go.SetActive(true);
            }
        }
    }
}
