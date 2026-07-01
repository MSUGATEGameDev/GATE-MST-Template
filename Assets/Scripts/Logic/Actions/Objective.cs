using UnityEngine;
using System.Collections.Generic;

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
    [Tooltip("Tell the player what to do.")]public string objectiveTitle = "Defeat the enemies!";
    [Tooltip("Something it will only say at the start of the challenge.")] public string startNotes = "";
    [Tooltip("Something it will only say at the end of the challenge.")] public string endNotes = "";
    [Tooltip("How many steps are there to do?")] public int goal = 10;
    [Tooltip("How would you describe what they do each time they complete a step?")] public string description = "enemies defeated.";
    [Tooltip("What happens when they complete the goal?")]public List<GameAction> actionsOnSuccess;


    public void InitializeObjective(bool byActivate)
    {
        if(!activated || repeatInitializations)
        {
            activated = true;
            ObjectivesManager.CreateObjective(objectiveTitle,goal,description,startNotes,endNotes,actionsOnSuccess,(byActivate && goal==1));
        }
    }
    public void CancelObjective()
    {
        ObjectivesManager.CancelObjective(description);
    }
    public override void Activate()
    {
        if (!activated && initiateObjectiveBy == InitateObjectiveBy.AnyActivation)
            InitializeObjective(true);
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
