using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public abstract class GameTrigger : GameAction
{
    // --- GameTrigger --- //
    // A class made to provide a starting point for the logic of any triggers in the game.
    // This abstract class is only a base to be used with all types of triggers in-game.

    [Tooltip("These objects will be triggered when this trigger is activated.")]
    public List<GameAction> objectsToActivate = new();
    
    
    public override void Activate() // This is what happens when this object is triggered. Usually to activate down the line.
    {
        ActivateItems();
    }
    public void ActivateItems() // This is what happens when this object activates down the line.
    {
        foreach (var gameAction in objectsToActivate)
        {
            gameAction.Activate();
        }
    }
    public override void Deactivate() // This is what happens when this object is untriggered. Usually to deactivate down the line.
    {
        DeactivateItems();
    }
    public void DeactivateItems() // This is what happens when this object deactivates down the line.
    {
        foreach (var gameAction in objectsToActivate)
        {
            gameAction.Deactivate();
        }
    }
    public virtual void Overridden() { } // To be used (if needed) by game triggers meant to be pressed by the action button, when another object takes the action button.
}
