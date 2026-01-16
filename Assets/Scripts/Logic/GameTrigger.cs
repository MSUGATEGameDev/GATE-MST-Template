using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public abstract class GameTrigger : GameAction
{
    // --- GameTrigger --- //
    // A class made to provide a starting point for the logic of any triggers in the game.
    // This abstract class is only a base to be used with all types of triggers in-game.

    [Tooltip("These objects will be activated according to the rules below.")]
    public List<GameAction> objectsToActivate = new();
    public enum ActionOnActivate {Activate,Deactivate,Nothing}
    [Tooltip("What do you want to happen to the listed items?")]
    public ActionOnActivate actionOnActivate = ActionOnActivate.Activate;
    [Tooltip("What do you want to happen to the listed items? (Note: Not every trigger has a deactivate.)")]
    public ActionOnActivate actionOnDeactivate = ActionOnActivate.Deactivate;
    
    public override void Activate() // This is what happens when this object is triggered. Usually to activate down the line.
    {
        ActivateItems();
    }
    public void ActivateItems() // This is what happens when this object activates down the line.
    {
        foreach (var gameAction in objectsToActivate)
        {
            switch (actionOnActivate)
            {
                case ActionOnActivate.Activate:
                    gameAction.Activate();
                    break;
                case ActionOnActivate.Deactivate:
                    gameAction.Deactivate();
                    break;
            }
        }
    }
    public override void Deactivate() // This is what happens when this object is untriggered. Usually to deactivate down the line.
    {
        DeactivateItems();
    }
    public void DeactivateItems() // This is what happens when this object deactivates down the line.
    {
        foreach (var actionable in objectsToActivate)
        {
            switch (actionOnDeactivate)
            {
                case ActionOnActivate.Activate:
                    actionable.Activate();
                    break;
                case ActionOnActivate.Deactivate:
                    actionable.Deactivate();
                    break;
            }
        }
    }
    public virtual void Overridden() { } // To be used (if needed) by game triggers meant to be pressed by the action button, when another object takes the action button.
}
