using UnityEngine;

public class Door : Actionable
{
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
