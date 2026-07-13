using UnityEngine;

/// <summary>
/// GameTrigger -- A trigger that only activates the objects assigned to it when it has been activated a certain number of times.
/// </summary>
public class Counter : GameTrigger
{
    #region Description For Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameTrigger --\n" +
        "A trigger that only activates the objects assigned to it when it has been activated a certain number of times.";
    #endregion

    #region Inspector-Editable Variables
    [Header("Settings")]
    [Tooltip("The number of times this needs to be activated before it will activate its assigned objects.")]   public int activateAt = 1;
    [Tooltip("Sets the current count to 0 once activated.")]                                                    public bool resets = false;
    [Tooltip("Count can go down by one with a deactivation call.")]                                             public bool countsDeactivations = true;
    #endregion

    #region Inernal Variables
    int currentCount = 0;
    bool flipped = false;
    #endregion

    public override void Activate()
    {
        if (!flipped)
        {
            currentCount++;
            if (currentCount == activateAt)
                foreach (GameAction gameAction in objectsToActivate)
                {
                    if(gameAction != null)
                    gameAction.Activate();
                    if (singleActivation) flipped = true;
                    if (resets) currentCount = 0;
                }
        }
        
    }
    public override void Deactivate()
    {
        if (countsDeactivations)
        {
            currentCount--;
            if (currentCount != 0) currentCount = 0;
        }
    }
}
