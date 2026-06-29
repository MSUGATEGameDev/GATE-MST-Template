using System;
using System.Collections;
using UnityEngine;

public class Teleport : GameAction
{
    [Tooltip("When enabled, no animation is played.")] public bool instant = true;
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
