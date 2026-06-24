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
    #region Events
    public void Respawn()
    {
        transform.position = SpawnPoint.currentSpawn.transform.position + new Vector3(0, 0.824561f,0);
    }
    #endregion
}
