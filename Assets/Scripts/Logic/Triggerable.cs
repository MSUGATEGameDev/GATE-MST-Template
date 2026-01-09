using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public abstract class Triggerable : Actionable
{
    // A class made to provide a starting point for the logic of any triggers in the game.

    [Tooltip("These objects will be activated when this trigger is activated (and deactivated when this trigger is deactivated).")]
    [SerializeField] List<Actionable> objectsToActivate = new();
    
    // Unless otherwise specified, triggerables will pass along activations and deactivations to their connected actionables.
    public override void Activate()
    {
        foreach (var actionable in objectsToActivate)
        {
            actionable.Activate();
        }
    }
    public override void Deactivate()
    {
        foreach (var actionable in objectsToActivate)
        {
            actionable.Deactivate();
        }
    }
}
