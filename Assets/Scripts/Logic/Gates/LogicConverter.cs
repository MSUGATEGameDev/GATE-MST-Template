using System.Collections.Generic;
using UnityEngine;

public class LogicConverter : GameTrigger
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
    "Converts any activation or deactivation to a deactivation or activation of any other object.";

    [Tooltip("These objects will be triggered when this trigger is deactivated.")] public List<GameAction> objectsToDeactivate;
    public enum ActionOnActivate { Activate, Deactivate, Nothing }
    [Tooltip("What do you want to happen to the listed items when this gate is activated?")]
    public ActionOnActivate actionOnActivate = ActionOnActivate.Activate;
    [Tooltip("What do you want to happen to the listed items when this gate is deactivated?")]
    public ActionOnActivate actionOnDeactivate = ActionOnActivate.Deactivate;

    public override void Activate()
    {
        switch (actionOnActivate)
        {
            case ActionOnActivate.Activate:
                foreach (GameAction gameAction in objectsToActivate)
                {
                    gameAction.Activate();
                }
                break;
                case ActionOnActivate.Deactivate:
                foreach (GameAction gameAction in objectsToActivate)
                {
                    gameAction.Deactivate();
                }
                break;
        }

    }
    public override void Deactivate()
    {
        switch (actionOnDeactivate)
        {
            case ActionOnActivate.Activate:
                foreach (GameAction gameAction in objectsToDeactivate)
                {
                    gameAction.Activate();
                }
                break;
            case ActionOnActivate.Deactivate:
                foreach (GameAction gameAction in objectsToDeactivate)
                {
                    gameAction.Deactivate();
                }
                break;
        }

    }
}
