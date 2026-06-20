using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public abstract class GameTrigger : GameAction
{
    // --- GameTrigger --- //
    // A class made to provide a starting point for the logic of any triggers in the game.
    // This abstract class is only a base to be used with all types of triggers in-game.
    [Header("Trigger Settings")]
    [Tooltip("These objects will be triggered when this trigger is activated.")]
    public List<GameAction> objectsToActivate = new();
    [Tooltip("If selected, only activate the first time.")]public bool singleActivation = false;
    bool activated = false;
    
    public override void Activate() // If this object is activated as an action. Usually to activate down the line.
    {
        if(!activated || !singleActivation)
        {
            activated = true;
            ActivateItems();
        }
    }
    public void ActivateItems() // Activating its assigned sub-actions.
    {
        foreach (var gameAction in objectsToActivate)
        {
            if(gameAction != null)
            {
                gameAction.Activate();
            }  
        }
    }
    public override void Deactivate() // If this object is deactivated as an action. Usually to deactivate down the line.
    {
        DeactivateItems();
    }
    public void DeactivateItems() // Deactivating its assigned sub-actions.
    {
        foreach (var gameAction in objectsToActivate)
        {
            if (gameAction != null)
            {
                gameAction.Deactivate();
            }
        }
    }
    public virtual void Overridden() { } // Some triggers have an area of effect where they can be pressed by the action button. This function is used as needed by this kind of trigger when another area of effect has take over.
    public void ResetActivation() // If this is set to single activation, this function resets that activation, allowing it to be reset again.
    {
        activated = false;
    }
}
