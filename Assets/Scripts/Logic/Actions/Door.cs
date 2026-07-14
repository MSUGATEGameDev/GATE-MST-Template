using UnityEngine;

public class Door : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "--GameAction--\n" +
        "Opened on Activate, Closed on Deactivate.\n" +
        "Enable 'Door Controller' object for auto-open and key-locks.";
    public enum HingeType { Swing, Slide}
    public HingeType hingeType = HingeType.Swing;
    public bool opensBothWays = true;
    Transform backSideChecker;
    Transform frontSideChecker;

    Animator animator;
    private void Start()
    {
        backSideChecker = Instantiate(new GameObject("Empty")).transform;
        backSideChecker.parent = transform;
        backSideChecker.localPosition = new Vector3(.2f,0,0);
        frontSideChecker = Instantiate(new GameObject("Empty")).transform;
        frontSideChecker.parent = transform;
        frontSideChecker.localPosition = new Vector3(-.2f, 0, 0);

        try // Get the animator if it has one.
        {
            animator = GetComponent<Animator>();
        }
        catch { }
    }
    bool openFront = true;
    public override void Activate()
    {
        if (animator != null) 
        {
            if (!opensBothWays || hingeType == HingeType.Slide || Vector3.Distance(Player.singleton.transform.position, backSideChecker.position) > Vector3.Distance(Player.singleton.transform.position, frontSideChecker.position))
            {
                animator.Play("DoorOpen");
                openFront = true;
            }
            else
            {
                animator.Play("DoorOpenRev");
                openFront = false;
            }
        }
    }
    public override void Deactivate()
    {
        if (animator != null)
        {
            if (openFront)
                animator.Play("DoorClose");
            else
                animator.Play("DoorCloseRev");
        }
    }
}
