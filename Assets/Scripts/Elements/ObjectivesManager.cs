using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    #region Keys
    // Processes that allow a keycard door system to be utilized.
    static List<bool> collectedKeys = new() { false, false, false, false, false, false };
    public enum CollecibleKey { Red, Orange, Yellow, Green, Blue, Violet };
    public static void KeyCollected(CollecibleKey key)
    {
        collectedKeys[(int)key] = true;
        HUD.DisplayKey(key);
        HUD.DisplayNotice(key.ToString() + " Key Collected!");
    }
    public static bool CheckKey(CollecibleKey key) 
    {
        return collectedKeys[(int)key];
    }
    #endregion
    #region Counters
    public static List<string> counterInstructions; // e.g. Kill 10 enemies.
    public static List<int> counterCounts;          // Current count.
    public static List<int> counterGoals;           // e.g. 10
    public static List<string> counterDescriptions; // e.g. enemies killed.
    public static List<GameAction> counterActions;  // What happens when objective is complete?

    public static void CreateObjective(string instructions, int goal, string description, GameAction onCompletion)
    {
        if (!counterDescriptions.Contains(description))
        {
            counterInstructions.Add(instructions);
            counterCounts.Add(0);
            counterGoals.Add(goal);
            counterDescriptions.Add(description);
            HUD.DisplayAnnouncement(instructions, "New Objective!");
        }


    }
    public static void CompleteObjectiveTask(string description)
    {
        if (counterDescriptions.Contains(description))
            {
            int indx = counterDescriptions.IndexOf(description);
            counterCounts[indx]++;
            if (counterCounts[indx] == counterGoals[indx])
            {
                HUD.DisplayAnnouncement(counterInstructions[indx], "Objective Complete!");
                counterActions[indx].Activate();
                counterInstructions.RemoveAt(indx);
                counterGoals.RemoveAt(indx);
                counterDescriptions.RemoveAt(indx);
            }
            HUD.DisplayObjectives();
        }

    }
    #endregion
}
