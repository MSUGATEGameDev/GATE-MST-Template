using UnityEngine;

public class Objective : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "Tells player about (and keeps track of) tasks.";
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
            ObjectivesManager.CreateObjective(instructions,goal,description,actionOnSuccess);
        }
    }
    public void CancelObjective()
    {
        ObjectivesManager.CancelObjective(description);
    }
    public override void Activate()
    {
        if (!activated && initiateObjectiveBy == InitateObjectiveBy.AnyActivation)
            InitializeObjective();
        ObjectivesManager.CompleteObjectiveTask(description);
    }

    public override void Deactivate()
    {
        if (deactivateDecrements)
        {
            ObjectivesManager.RevertObjectiveTask(description);
        }
    }
}
