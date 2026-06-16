using UnityEngine;

public class PushButton : GameTrigger
{
    // --- Push Button --- //
    // A button that needs to be actively pressed by the player with the action button while in range.
    // Many instances can be called at once.

    [Header("Settings")]
    [Tooltip("When checked, button will deactivate on second push.")] public bool toggle = true;
    bool toggleOn = true;

    [Header("Components")]
    Animator animator; // Used to handle visual animations.
    [SerializeField]GameObject toolTip; // Tells players to push action button to activate.
    

    void Awake() // Called before everything else as soon as this object is created.
    {
        try
        {
            animator = GetComponent<Animator>();
        }
        catch { }
    }

    public override void Activate() 
    {
        if (toggle)
        {
            Debug.Log(toggle + " - " + toggleOn);
            if (toggleOn)
            {
                base.Activate();
            }
            else
            {
                base.Deactivate();
            }
            toggleOn = !toggleOn;
        }

        
        animator.Play("ButtonPush");
    }

    private void OnTriggerEnter(Collider other) // When player enters vicinity, this assigns itself to the Action Button.
    {
        if (other.CompareTag("Player"))
        {
            if (Player.singleton.triggerInRange != null) // Override the old object that was going to use the action button.
                Player.singleton.triggerInRange.Overridden();
            Player.singleton.triggerInRange = this;
            toolTip.SetActive(true);
        }
    }
    public override void Overridden() // When another button or trigger has overridden this for assignment of the Action Button.
    {
        toolTip.SetActive(false);
    }
    private void OnTriggerExit(Collider other) // When it leaves the vicinity, this unassigns itself from the Action Button.
    {
        if(other.CompareTag("Player") && Player.singleton.triggerInRange != null && Player.singleton.triggerInRange == this)
        {
            Player.singleton.triggerInRange = null;
            toolTip.SetActive(false);
        }
    }
}
