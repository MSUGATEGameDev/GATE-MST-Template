using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : Entity
{
    // --- PlayerController --- //
    // Handles device input, the HUD, and stores a couple of more broadly-used shared variables.
    // Access via singleton: PlayerController.singleton

    #region Singleton Setup
    // Creating a unique singleton of the PlayerController on Awake.
    public static Player singleton;
    private void Awake()
    {
        // Check if an instance already exists.
        if (singleton != null && singleton != this)
        {
            // If another instance exists, destroy this one to ensure only one remains.
            Destroy(this.gameObject);
        }
        else
        {
            // If no instance exists, set this one as the instance.
            singleton = this;
            // Optional: use DontDestroyOnLoad to persist across scene changes.
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    #region Player Parts
    [Header("Player Parts")]
    [HideInInspector] public Transform playerCamTransform; // The transform that controls the position of the main camera, which can be referenced by other classes to make things look at the player.
    private Camera playerCamera;
    #endregion

    protected override void Start()
    {
        base.Start();

        playerCamera = GetComponentInChildren<Camera>();
        playerCamTransform = playerCamera.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected override void Update()
    {
        base.Update();
        InfluenceMove(playerCamera.transform.eulerAngles.y);
    }

    #region Input & Controls
    [HideInInspector] public GameTrigger triggerInRange;
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveValue = context.ReadValue<Vector2>();
        Move(moveValue);
    }
    public void OnJumpButton(InputAction.CallbackContext context)
    {
        Jump();
    }
    public void OnAttackButton(InputAction.CallbackContext context)
    {
        Attack();
    }
    public void OnRunButton(InputAction.CallbackContext context)
    {

        running = context.ReadValueAsButton();
    }
    public void OnActionButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (triggerInRange != null)
            {
                triggerInRange.Activate();
            }
        }

    }
    #endregion

    #region HUD Elements
    [Header("Hud Elements")]
    [SerializeField] TextMeshPro messageArea; // The text mesh on the screen where we can send basic text messages to the player.
    [SerializeField] int secondsPerMessage = 10;
    List<string> upcomingMessages; // Messages that have been sent and haven't been displayed yet, if we need to queue up a few messages.
    Coroutine messageCoroutine; // Where we store the process that is timing the messages, so we can stop it if needed.
    public void DisplayOnScreenText(string text) // Displays text on the screen.
    {
        upcomingMessages.Add(text);
        if (messageCoroutine == null)
            messageCoroutine = StartCoroutine(MessageTimer(text));
    }
    IEnumerator MessageTimer(string message) // Leaves text up for the indicated time before swapping to the next thing.
    {
        while (upcomingMessages.Count != 0)
        {
            messageArea.text = upcomingMessages[0];
            upcomingMessages.RemoveAt(0);
            yield return new WaitForSeconds(secondsPerMessage);
        }
        messageArea.text = "";
    }
    public void StopOnScreenText() // Stops the message display process and clears everything.
    {
        upcomingMessages.Clear();
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);
        messageArea.text = "";
    }
    #endregion

    #region Events
    public void Respawn()
    {
        transform.position = SpawnPoint.currentSpawn.transform.position;
    }
    #endregion
}
