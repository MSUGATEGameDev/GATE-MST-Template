using UnityEngine;

public class Door : GameAction
{
    [ReadOnly]
    [TextArea(3,10)]
    public string About = "Doors can be controlled as an action. When activated, the door opens, when deactivated, the door closes.\n" +
        "If you want to have the door manage itself via auto-opening and keys, enable and configure the 'Door Controller' object contained therein.";
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
