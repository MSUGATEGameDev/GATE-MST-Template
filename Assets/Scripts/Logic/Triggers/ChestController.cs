using UnityEngine;

public class ChestController : GameTrigger
{
    [ReadOnly]
    [TextArea(1, 10)]
    public new string _ = "-- GameTrigger --\n" +
    "Allows a chest to auto-open when the player is present or be locked to color-coded keys.";
    [Header("Chest Settings")]
    [Tooltip("")] public bool lockedWithKey = false;
    public ColorManager.StandardColor keyColor;
    [Header("Components")]
    public GameObject lockIcon;
    public bool opened = false;

    // Internal Logic
    bool stillLocked = false;

    private void Start()
    {
        if (lockedWithKey)
        {
            stillLocked = true;
            lockIcon.SetActive(true);
            Material[] mat = new Material[2] {ColorManager.current.standardHolos[(int)keyColor], ColorManager.current.holoBlack };
            foreach (Renderer rend in lockIcon.GetComponentsInChildren<Renderer>())
            {
                rend.materials = mat;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !opened)
        {
            if (lockedWithKey && stillLocked)
            {
                if (ObjectivesManager.CheckKey(keyColor))
                {
                    HUD.DisplayNotice("Chest Unlocked");
                    lockIcon.SetActive(false);
                    stillLocked = false;
                    opened = true;
                    ActivateItems();
                }
                else
                {
                    HUD.DisplayNotice(keyColor.ToString() + " key needed.");
                }
            }
            else
            {
                opened = true;
                ActivateItems();
            }
        }
    }
}
