using UnityEngine;

public class Counter : Triggerable
{
    // Activates things only after the right amount of effects have triggered it.
    int currentCount = 0;
    bool flipped = false;

    public int activateAt = 1;
    [Tooltip("Sets the current count to 0 once activated.")] public bool resets = false;
    [Tooltip("Count can go down by one with a deactivation call.")] public bool countsDeactivations = true;
    [Tooltip("Only triggers once.")]public bool singleActivation = false;

    public override void Activate()
    {
        if (!flipped)
        {
            currentCount++;
            if (currentCount == activateAt)
                foreach (Actionable actionable in objectsToActivate)
                {
                    actionable.Activate();
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
