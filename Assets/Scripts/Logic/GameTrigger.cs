using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// GameTrigger -- Provides a starting point for the logic of any triggers (which are meant to initiate actions.)
/// </summary>
public abstract class GameTrigger : GameAction
{
    #region Inspector-Editable Variables
    [Header("Trigger Settings")]
    [Tooltip("These objects will be triggered when this trigger is activated.")]    public List<GameAction> objectsToActivate = new();
    [Tooltip("If selected, only activate the first time.")]                         public bool singleActivation = false;
    [Tooltip("Allows this trigger to deactivate items.")]                           public bool deactivates = true;
    #endregion
    
    #region Internal Variables
    bool activated = false;
    #endregion

    #region Functions
    public override void Activate() 
    {
            ActivateItems();
    }
    /// <summary>
    /// Activates all the sub-objects assigned to be activated by this object.
    /// </summary>
    public void ActivateItems() 
    {
        if (!activated || !singleActivation) // If it's not a single activation that has already been activated.
        {
            activated = true;

            foreach (var gameAction in objectsToActivate)
            {
                if (gameAction != null)
                {
                    gameAction.Activate();
                }
            }
        }
    }
    public override void Deactivate()
    {
            DeactivateItems();
    }
    /// <summary>
    /// Deactivates all the sub-objects assigne to be deactivated by this object.
    /// </summary>
    public void DeactivateItems() // Deactivating its assigned sub-actions.
    {
        if (deactivates && (!activated || !singleActivation))
        {
            foreach (var gameAction in objectsToActivate)
            {
                if (gameAction != null)
                {
                    gameAction.Deactivate();
                }
            }
        }
    }

    /// <summary>
    /// Some triggers have an area of effect where they can be pressed by the action button. This function is used as needed by this kind of trigger when another area of effect has take over.
    /// </summary>
    public virtual void Overridden() { }

    /// <summary>
    /// If this is set to single activation, this function resets that activation, allowing it to be reset again.
    /// </summary>
    public void ResetActivation()
    {
        activated = false;
    }
    #endregion
}
