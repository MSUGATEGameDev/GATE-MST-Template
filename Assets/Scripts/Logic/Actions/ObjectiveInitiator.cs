using UnityEngine;

public class ObjectiveInitiator : GameAction
{
    [SerializeField] Objective objective;
    [SerializeField] bool deactivateCancels = false;
    public override void Activate()
    {
        objective.InitializeObjective();
    }

    public override void Deactivate()
    {
        if(deactivateCancels)
            objective.CancelObjective();
    }
}
