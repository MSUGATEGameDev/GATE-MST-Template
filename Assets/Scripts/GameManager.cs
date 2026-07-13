using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Game System -- Manages some general game features that don't have dedicated managers.
/// </summary>
public class GameManager : MonoBehaviour
{
    // This is a very incomplete class. :D
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- Game System --\n" +
        "Manages some general game features that don't have dedicated managers.";

    [Tooltip("The in-game ceiling. Hidden in editor to allow editing.")]public GameObject ceiling;
    [Tooltip("If enabled, the in-game ceiling will appear at the start.")]public bool indoors = true;
    [Tooltip("GameActions you would like to activate when the game starts.")] public List<GameAction> actions = new();
    [Tooltip("Checking this box will clear the objectives when this scene loads.")] public bool clearObjectives = true;
    private void Awake()
    {
        if(indoors && ceiling != null)
            ceiling.SetActive(true);
        if (clearObjectives) ClearObjectives();
    }
    private void Start()
    {
        foreach(GameAction action in actions)
        {
            if(action != null)
            {
                action.Activate();
            }
        }
        
    }
    private void ClearObjectives()
    {
        ObjectivesManager.counterInstructions = new(); 
        ObjectivesManager.counterCounts = new();         
        ObjectivesManager.counterGoals = new();           
        ObjectivesManager.counterDescriptions = new(); 
        ObjectivesManager.endMessages = new();
        ObjectivesManager.counterActions = new();  
}
}
