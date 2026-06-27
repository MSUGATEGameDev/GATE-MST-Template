using UnityEngine;

/// <summary>
/// GameAction -- Focuses the players attention on a specific location by temporarily moving the camera.
/// </summary>
public class FocusCamera : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "Focuses the players attention on a specific location by temporarily moving the camera.";
    public override void Activate()
    {
        // Smoothly moves camera to this location.
    }

    public override void Deactivate()
    {
        // Smoothly moves camera back to player.
    }
}
