using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// -- System -- Keeps track of objectives created by objective Actions and interfaces with the HUD to display them.
/// </summary>
public class ObjectivesManager : MonoBehaviour
{
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- System --\n" +
        "Keeps track of objectives created by objective Actions and interfaces with the HUD to display them.";
    #endregion
    #region Keys
    // Processes that allow a keycard door system to be utilized.
    static List<bool> collectedKeys = new() { false, false, false, false, false, false };
    
    /// <summary>
    /// Lets the game know a certain key has been collected, allowing the player to open objects locked to that key color.
    /// </summary>
    /// <param name="key">Color of the collected key.</param>
    public static void KeyCollected(ColorManager.StandardColor key)
    {
        collectedKeys[(int)key] = true;
        HUD.DisplayKey(key);
    }
    public static bool CheckKey(ColorManager.StandardColor key) 
    {
        return collectedKeys[(int)key];
    }
    #endregion
    #region Counters
    public static List<string> counterInstructions = new(); // e.g. Kill 10 enemies.
    public static List<int> counterCounts = new();          // Current count.
    public static List<int> counterGoals = new();           // e.g. 10
    public static List<string> counterDescriptions = new(); // e.g. enemies killed.
    public static List<GameAction> counterActions = new();  // What happens when objective is complete?

    public static void CreateObjective(string instructions, int goal, string description, GameAction onCompletion)
    {
        if (!counterDescriptions.Contains(description))
        {
            counterInstructions.Add(instructions);
            counterCounts.Add(0);
            counterGoals.Add(goal);
            counterDescriptions.Add(description);
            counterActions.Add(onCompletion);
            HUD.DisplayAnnouncement(instructions, "New Objective!");
            HUD.DisplayObjectives();
        }


    }
    public static void CancelObjective(string description)
    {
        if (counterDescriptions.Contains(description))
        {
            int indx = counterDescriptions.IndexOf(description);
            HUD.DisplayAnnouncement(counterInstructions[indx], "Objective Cancelled");
            counterInstructions.RemoveAt(indx);
            counterGoals.RemoveAt(indx);
            counterCounts.RemoveAt(indx);
            counterDescriptions.RemoveAt(indx);
        }
    }
    public static void CompleteObjectiveTask(string description)
    {
        if (counterDescriptions.Contains(description))
            {
            int indx = counterDescriptions.IndexOf(description);
            counterCounts[indx]++;
            if (counterCounts[indx] >= counterGoals[indx])
            {
                HUD.DisplayAnnouncement(counterInstructions[indx], "Objective Complete!");
                counterActions[indx].Activate();
                counterInstructions.RemoveAt(indx);
                counterGoals.RemoveAt(indx);
                counterCounts.RemoveAt(indx);
                counterDescriptions.RemoveAt(indx);
                counterActions.RemoveAt(indx);
            }
            HUD.DisplayObjectives();
        }

    }
    
    public static void RevertObjectiveTask(string description)
    {
        if (counterDescriptions.Contains(description))
        {
            int indx = counterDescriptions.IndexOf(description);
            counterCounts[indx]--;
            HUD.DisplayObjectives();
        }
    }
    #endregion
}
