using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// GameAction - When activated, teleports the player to the location of this object.
/// </summary>
public class Teleport : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "When activated, teleports the player to the location of this object.";
    [Tooltip("When checked, appear immediately at the new location with no animation.")] public bool instant = true;
    
    public override void Activate()
    {
        if (instant)
        {
            Player.singleton.transform.position = transform.position + new Vector3(0, 0.824561f, 0);
        }
        else
        {
            Player.singleton.Teleport(transform.position + new Vector3(0, 0.824561f, 0));
        }
    }
    public override void Deactivate()
    {
        // Doesn't do anything.
    }
}
