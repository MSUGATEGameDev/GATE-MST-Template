using UnityEngine;

public class Chest : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "Contains a reward. Openable by key or action.";
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void Activate()
    {
        animator.Play("ChestOpen");
    }
    public override void Deactivate()
    {
        animator.Play("ChestClose");
    }
}
