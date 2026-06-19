using UnityEngine;

public class DoorController : GameTrigger
{
    [Header("Door Settings")]
    [Tooltip("")]public bool lockedWithKey = false;
    public ColorManager.StandardColor keyColor;
    [Header("Components")]
    public GameObject lockIcons;

    // Internal Logic
    bool stillLocked = false;

    private void Start()
    {
        if (lockedWithKey)
        {
            stillLocked = true;
            lockIcons.SetActive(true);
            Material[] mat = new Material[2] { ColorManager.current.holoBlack, ColorManager.current.standardHolos[(int)keyColor] };
            foreach (Renderer rend in lockIcons.GetComponentsInChildren<Renderer>())
            {
                rend.materials = mat;
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (lockedWithKey && stillLocked)
            {
                if (ObjectivesManager.CheckKey(keyColor))
                {
                    HUD.DisplayNotice("Door Unlocked");
                    lockIcons.SetActive(false);
                    stillLocked = false;
                    ActivateItems();
                }
                else
                {
                    HUD.DisplayNotice(keyColor.ToString() + " key needed.");
                }
            }
            else
            {
                ActivateItems();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (!stillLocked)
        {
            DeactivateItems();
        }
    }
}
