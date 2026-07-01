using TMPro;
using UnityEngine;

public class NPC : GameTrigger
{
    #region Description For Unity Inspector
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameTrigger --\n" +
    "A NPC that can tell you things, upon completeing it's dialog you can trigger stuff like with a button";
    #endregion

    #region Inspector-Editable Variables
    [Header("Settings")]
    [Tooltip("When checked, NPC will deactivate on second interact.")] public bool toggle = true;
    bool toggleOn = true;

    [Tooltip("The NPC Name")] public string npcName = "NPC";
    [TextArea(1, 10)]
    [Tooltip("Dialog Messages")] public string[] dialog;

    [Header("Components")]
    [SerializeField] GameObject toolTip; // Tells players to push action button to activate.
    [SerializeField] private GameObject dialogScreen;
    [SerializeField] private TextMeshProUGUI npcNameBox;
    [SerializeField] private TextMeshProUGUI dialogBox;
    [SerializeField] private GameObject dialogBackButton;
    #endregion

    #region Internal Variables
    private int dialogIndex = 0;
    #endregion

    void Awake() // Called before everything else as soon as this object is created.
    {

    }

    public override void Activate()
    {
        if (toggle)
        {
            if (toggleOn)
            {
                ShowDialog();
            }
            else
            {
                base.Deactivate();
            }
            toggleOn = !toggleOn;
        }
        else
            base.Activate();
    }

    public void ShowDialog()
    {
        if (dialog != null && dialog.Length != 0)
        {
            Player.singleton.disabled = true;
            dialogIndex = 0;
            npcNameBox.text = npcName;
            dialogBox.text = dialog[0];
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            dialogScreen.SetActive(true);
        }
        else
        {
            print(npcName + " has not had any dialog configured!");
        }
    }
    public void NextDialog()
    {
        if ((dialogIndex + 1) > (dialog.Length - 1))
        {
            CloseDialog();
            dialogBackButton.SetActive(false);
            base.Activate();
        }
        else
        {
            dialogBackButton.SetActive(true);
            dialogIndex += 1;
            dialogBox.text = dialog[dialogIndex];
        }
    }

    public void PrevDialog()
    {
        if ((dialogIndex - 1) >= 0)
        {
            dialogIndex -= 1;
            dialogBox.text = dialog[dialogIndex];
        }
    }

    public void CloseDialog()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        dialogScreen.SetActive(false);
        Player.singleton.disabled = false;
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
    private void OnDisable()
    {
        if (Player.singleton.triggerInRange == this)
        {
            Player.singleton.triggerInRange = null;
        }
    }
    public override void Overridden() // When another button or trigger has overridden this for assignment of the Action Button.
    {
        toolTip.SetActive(false);
    }
    private void OnTriggerExit(Collider other) // When it leaves the vicinity, this unassigns itself from the Action Button.
    {
        if (other.CompareTag("Player") && Player.singleton.triggerInRange != null && Player.singleton.triggerInRange == this)
        {
            Player.singleton.triggerInRange = null;
            toolTip.SetActive(false);
        }
    }
}
