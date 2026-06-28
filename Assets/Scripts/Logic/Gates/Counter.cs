using UnityEngine;

public class Counter : GameTrigger
{
    public string _ = "-- GameTrigger --\n" +
        "A trigger that only activates the objects assigned to it when it has been activated a certain number of times.";
    int currentCount = 0;
    bool flipped = false;

    [Header("Settings")]
    public int activateAt = 1;
    [Tooltip("Sets the current count to 0 once activated.")] 
    public bool resets = false;
    [Tooltip("Count can go down by one with a deactivation call.")] 
    public bool countsDeactivations = true;
    [Tooltip("Only triggers once.")]

    public override void Activate()
    {
        if (!flipped)
        {
            currentCount++;
            if (currentCount == activateAt)
                foreach (GameAction gameAction in objectsToActivate)
                {
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
