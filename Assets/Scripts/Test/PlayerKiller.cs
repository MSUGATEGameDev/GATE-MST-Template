using UnityEngine;

public class PlayerKiller : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "A test action that kills the player with a button.";
    public override void Activate()
    {

        FindAnyObjectByType<Player>().Die();
    }

    public override void Deactivate()
    {

    }
}
