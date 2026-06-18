using UnityEngine;

public class Objective : GameAction
{
    [Header("Initialization")]
    public InitateObjectiveBy initiateObjectiveBy;
    public enum InitateObjectiveBy { AnyActivation, InitiatorOnly }
    [SerializeField] bool repeatInitializations = false;
    [Header("Progress")]
    [SerializeField] bool deactivateDecrements = false;
    bool activated = false;
    [Header("Details")]
    [Tooltip("Tell the player what to do.")]public string instructions = "Defeat the enemies!";
    [Tooltip("How many steps are there to do?")] public int goal = 10;
    [Tooltip("How would you describe what they do each time they complete a step?")] public string description = "enemies defeated.";
    [Tooltip("What happens when they complete the goal?")]public GameAction actionOnSuccess;


    public void InitializeObjective()
    {
        if(!activated || repeatInitializations)
        {
            activated = true;

        }
    }
    public void CancelObjective()
    {

    }
    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }
}
