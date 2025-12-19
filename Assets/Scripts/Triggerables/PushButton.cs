using UnityEngine;

public class PushButton : Triggerable
{
    // A button that needs to be actively pressed by the player with the action button while in range.
    Animator animator;
    private void Start()
    {
        try
        {
            animator = GetComponent<Animator>();
        }
        catch { }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.singleton.triggerInRange = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && PlayerController.singleton.triggerInRange != null && PlayerController.singleton.triggerInRange == this)
        {
            PlayerController.singleton.triggerInRange = null;
        }
    }
}
