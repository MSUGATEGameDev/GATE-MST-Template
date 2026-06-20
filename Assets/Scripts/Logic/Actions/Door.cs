using UnityEngine;

public class Door : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "--GameAction--\n" +
        "Opened on Activate, Closed on Deactivate.\n" +
        "Enable 'Door Controller' object for auto-open and key-locks.";
    
    Animator animator;
    private void Start()
    {
        try // Get the animator if it has one.
        {
            animator = GetComponent<Animator>();
        }
        catch { }

    }
    public override void Activate()
    {
        if (animator != null) 
        {
            animator.Play("DoorOpen");
        }
    }
    public override void Deactivate()
    {
        if (animator != null)
        {
            animator.Play("DoorClose");
        }
    }
}
