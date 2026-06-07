using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // --- PlayerController --- //
    // Handles device input, the HUD, and stores a couple of more broadly-used shared variables.
    // Access via singleton: PlayerController.singleton

    #region Singleton Setup
    // Creating a unique singleton of the PlayerController on Awake.
    public static PlayerController singleton;
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

    InputAction moveAction;
    InputAction lookAction;
    InputAction jumpAction;
    InputAction attackAction;
    InputAction sprintAction;

    public float moveSpeed = 3.0f;
    public float runSpeed = 5.0f;
    public float jumpSpeed = 5.0f;
    public float lookXSpeed = 30.0f;
    public float lookYSpeed = 30.0f;

    public float minAngle = -45;
    public float maxAngle = 45;

    public Transform groundCheck;

    private Rigidbody rigid;
    private Camera mainCam;
    private Animator anim;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
        sprintAction = InputSystem.actions.FindAction("Sprint");


        rigid = GetComponent<Rigidbody>();
        mainCam = GetComponentInChildren<Camera>();
        anim = GetComponentInChildren<Animator>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        Move();
        Look();

        if (attackAction.WasPerformedThisFrame())
        {
            anim.Play("TutBotPunch");
        }
    }

    private void Look()
    {
        Vector2 lookValue = lookAction.ReadValue<Vector2>();

        transform.localEulerAngles += new Vector3(
            0,
            lookValue.x * lookXSpeed * Time.deltaTime,
            0
        );

        float deltaY = -1 * lookValue.y * lookYSpeed * Time.deltaTime;
        float newY = mainCam.transform.localEulerAngles.x + deltaY;

        newY = AngleWithin180(newY);

        newY = Mathf.Clamp(newY, minAngle, maxAngle);

        mainCam.transform.localEulerAngles = new Vector3(
            newY,
            mainCam.transform.localEulerAngles.y,
            mainCam.transform.localEulerAngles.z
        );
    }

    private void Move()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        Vector3 vel = Vector3.zero;
        vel += transform.right * moveValue.x;
        vel += transform.forward * moveValue.y;

        if (sprintAction.IsPressed())
        {
            vel *= runSpeed;
        }
        else
        {
            vel *= moveSpeed;
        }

        vel.y = rigid.linearVelocity.y;

        //Jump Conditional
        if (jumpAction.WasPerformedThisFrame() &&
           Physics.Raycast(groundCheck.position, -1 * transform.up, .1f))
        {
            vel += transform.up * jumpSpeed;
        }

        rigid.linearVelocity = vel;

        float forwardVal = Vector3.Dot(vel, transform.forward);
        anim.SetFloat("move", forwardVal);
    }

    private float AngleWithin180(float angle)
    {
        if (angle > 180)
        {
            return angle - 360;
        }
        else
        {
            return angle;
        }
    }


    #region Player Parts
    [Header("Player Parts")]
    public Transform playerCamera;  // The transform that controls the position of the main camera, which can be referenced by other classes to make things look at the player.
    public Transform playerBody;    // The transform that controls the player. Used for moving the player around.
    #endregion

    #region Input & Controls
    [HideInInspector] public GameTrigger triggerInRange;
    void OnActionButton(InputAction.CallbackContext context)
    {
        if (triggerInRange != null)
        {
            triggerInRange.Activate();
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
        while(upcomingMessages.Count != 0)
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
