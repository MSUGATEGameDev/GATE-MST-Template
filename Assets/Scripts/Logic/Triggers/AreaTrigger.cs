using UnityEngine;

public class AreaTrigger : GameTrigger
{
    public new string _ = "-- GameTrigger --\n" +
        "Activates when entered by a player or another object as determined below.";
    [Header("Triggered By")]
    public bool player = true;
    public bool enemies = false;
    public bool objects = true;

    #region Presence
    int presentCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") && player) || (other.CompareTag("Enemy") && enemies) || (other.CompareTag("Object") && objects))
        {
            // If it wasn't already being activated, activate and play animation if it has one.
            if (presentCount == 0)
            {
                ActivateItems();
                if (animator != null)
                {
                    animator.Play("AreaActivate");
                }
            }
            presentCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Player") && player) || (other.CompareTag("Enemy") && enemies) || (other.CompareTag("Object") && objects))
        {
            // If it wasn't already inactive, deactivate and play animation if it has one.
            if (presentCount != 0)
            {
                DeactivateItems();
                if (animator != null)
                {
                    animator.Play("AreaDeactivate");
                }
            }
            presentCount--;
            if (presentCount < 0)
            {
                presentCount = 0;
            }
        }
    }
    #endregion

    #region Animation
    Animator animator;
    private void Start()
    {
        try // Get the animator if it has one.
        {
            animator = GetComponent<Animator>();
        }
        catch { }

    }
    #endregion

}



