using UnityEngine;

public class Door : GameAction
{
    public enum HingeType { Swing, Slide}
    public HingeType hingeType = HingeType.Swing;
    public bool opensBothWays = true;
    public enum OpenType {OnlyWhenTriggered, Automatic, WithColoredKey}
    public OpenType opens = OpenType.OnlyWhenTriggered;
    [Tooltip("The key color which unlocks the door.")] public ColorManager.StandardColor colorIfKey = ColorManager.StandardColor.Red;
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
        DoorController dc = GetComponentInChildren<DoorController>(true);
        switch (opens)
        {
            case OpenType.Automatic:
                dc.lockedWithKey = false;
                dc.gameObject.SetActive(true);
                break;
            case OpenType.WithColoredKey:
                dc.lockedWithKey = true;
                dc.keyColor = colorIfKey;
                dc.gameObject.SetActive(true);
                break;
        }
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
