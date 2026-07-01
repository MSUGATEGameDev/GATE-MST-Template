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
    /// <summary>
    /// Allows an object to check if a certain key has been collected and unlock accordingly.
    /// </summary>
    /// <param name="key">Color of the collected key.</param>
    /// <returns>TRUE if collected, FALSE if not</returns>
    public static bool CheckKey(ColorManager.StandardColor key) 
    {
        return collectedKeys[(int)key];
    }
    #endregion

    #region Objective Counters
    public static List<string> counterInstructions = new(); // e.g. Kill 10 enemies.
    public static List<int> counterCounts = new();          // Current count.
    public static List<int> counterGoals = new();           // e.g. 10
    public static List<string> counterDescriptions = new(); // e.g. enemies killed.
    public static List<string> endMessages = new();
    public static List<List<GameAction>> counterActions = new();  // What happens when objective is complete?

    /// <summary>
    /// Announces a new objective and adds a counter to the screen.
    /// </summary>
    /// <param name="instructions">Displayed in big text to the player. (e.g. Kill 10 enemies!)</param>
    /// <param name="goal">Number of successful tasks completed to complete this objective. (e.g. 10)</param>
    /// <param name="description">Description of each individal task. (e.g. enemies killed.) </param>
    /// <param name="onCompletion">An action that happens when the objective is completed.</param>
    public static void CreateObjective(string instructions, int goal, string description, string startMessage, string endMessage, List<GameAction> onCompletion, bool complete)
    {
        if (!counterDescriptions.Contains(description))
        {
            counterInstructions.Add(instructions);
            counterCounts.Add(0);
            counterGoals.Add(goal);
            counterDescriptions.Add(description);
            counterActions.Add(onCompletion);
            endMessages.Add(endMessage);
            if (!complete)
            {
                HUD.DisplayAnnouncement(instructions, "New Objective!", true);
                if (startMessage != "")
                {
                    HUD.DisplayNotice(startMessage, true);
                }

                HUD.DisplayObjectives();
            }
        }


    }
    /// <summary>
    /// Cancel an existing objective.
    /// </summary>
    /// <param name="description">Objective to cancel.</param>
    public static void CancelObjective(string description)
    {
        if (counterDescriptions.Contains(description))
        {
            int indx = counterDescriptions.IndexOf(description);
            HUD.DisplayAnnouncement("Objective Cancelled", counterInstructions[indx]);
            counterInstructions.RemoveAt(indx);
            counterGoals.RemoveAt(indx);
            counterCounts.RemoveAt(indx);
            counterDescriptions.RemoveAt(indx);
            counterActions.RemoveAt(indx);
            endMessages.RemoveAt(indx);
        }
    }
    /// <summary>
    /// Call each time an individual task is completed for an objective. Updates the counter.
    /// </summary>
    /// <param name="description">Objective to make progress on.</param>
    public static void CompleteObjectiveTask(string description)
    {
        if (counterDescriptions.Contains(description))
            {
            int indx = counterDescriptions.IndexOf(description);
            counterCounts[indx]++;
            if (counterCounts[indx] >= counterGoals[indx])
            {
                HUD.DisplayAnnouncement("Objective Complete!", counterInstructions[indx],true);
                if (endMessages[indx] != "")
                {
                    HUD.DisplayNotice(endMessages[indx], true);
                }
                foreach(GameAction actn in counterActions[indx])
                {
                    actn.Activate();
                }
                counterInstructions.RemoveAt(indx);
                counterGoals.RemoveAt(indx);
                counterCounts.RemoveAt(indx);
                counterDescriptions.RemoveAt(indx);
                counterActions.RemoveAt(indx);
                endMessages.RemoveAt(indx);
            }
            HUD.DisplayObjectives();
        }

    }
    /// <summary>
    /// Call each time an individual task is undone (e.g. 3 buttons need to be held down, but one was released).
    /// </summary>
    /// <param name="description">Objective in question</param>
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
