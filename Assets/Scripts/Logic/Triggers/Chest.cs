using UnityEngine;

public class Chest : GameAction
{
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
