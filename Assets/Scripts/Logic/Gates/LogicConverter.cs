using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LogicConverter : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
    "Converts any activation or deactivation to a deactivation or activation of any other object.";
    public enum ActionOnActivate { Activate, Deactivate, Nothing }

    public List<ActionActivationPair> whenThisIsActivated;
    public List<ActionActivationPair> whenThisIsDeactivated;

    public override void Activate()
    {
        foreach (ActionActivationPair actPair in whenThisIsActivated)
        {
            if (actPair.activateOrDe == ActionOnActivate.Activate)
            {
                if(actPair.gameAction != null)
                actPair.gameAction.Activate();
            }
            else if (actPair.activateOrDe == ActionOnActivate.Deactivate)
            {
                if (actPair.gameAction != null)
                    actPair.gameAction.Deactivate();
            }
        }
    }
    public override void Deactivate()
    {
        foreach (ActionActivationPair actPair in whenThisIsDeactivated)
        {
            if (actPair.activateOrDe == ActionOnActivate.Activate)
            {
                if (actPair.gameAction != null)
                    actPair.gameAction.Activate();
            }
            else if(actPair.activateOrDe == ActionOnActivate.Deactivate)
            {
                if (actPair.gameAction != null)
                    actPair.gameAction.Deactivate();
            }
        }
    }
}


// 1. Make the container structure serializable
[System.Serializable]
public struct ActionActivationPair
{
    public GameAction gameAction;
    public LogicConverter.ActionOnActivate activateOrDe;
}