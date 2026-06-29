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

    [Tooltip("Length in seconds that the cam holds at its target for")]
    public float camHold = 2f;
    [Tooltip("Speed that the cam gets to the target")]
    public float dollySpeed = 3f;

    private Player player;

    private void Start()
    {
        try // Get the player if spawned.
        {
            player = Player.singleton;
        }
        catch { }

    }
    public override void Activate()
    {
        if (player != null)
        {
            player.DeployCinemaCam(this);
        }
    }
    public override void Deactivate()
    {
        if (player != null)
        {

        }
    }
}
