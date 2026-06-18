using System.Collections.Generic;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    #region Keys
    // Processes that allow a keycard door system to be utilized.
    static List<bool> collectedKeys = new() { false, false, false, false, false, false };
    public enum KeyColor { Red, Orange, Yellow, Green, Blue, Violet };
    public static void KeyCollected(KeyColor key)
    {
        collectedKeys[(int)key] = true;
        HUD.DisplayKey(key);
    }
    public static bool CheckKey(KeyColor key) 
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
