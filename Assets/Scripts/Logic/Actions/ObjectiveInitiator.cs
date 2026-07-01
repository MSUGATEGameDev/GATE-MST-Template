using UnityEngine;

public class ObjectiveInitiator : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
    "Initiates an objective within the game, providing a text notice to the player and adding a counter to the corner.";
    [SerializeField] Objective objective;
    [SerializeField] bool deactivateCancels = false;
    public override void Activate()
    {
        objective.InitializeObjective(false);
    }

    public override void Deactivate()
    {
        if(deactivateCancels)
            objective.CancelObjective();
    }
}
