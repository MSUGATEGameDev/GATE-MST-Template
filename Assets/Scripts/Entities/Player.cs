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
    List<GameObject> meshesForReassembly = new();
    List<Transform> reassemblyPoints = new();

    public override void Die()
    {
        if(curState != EStates.dead)
        {
            base.Die();
            if (meshesForReassembly.Count == 0)
            {
                foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
                {
                    meshesForReassembly.Add(mr.gameObject);
                    reassemblyPoints.Add(Instantiate(new GameObject("Empty"), mr.gameObject.transform.position, mr.gameObject.transform.rotation, mr.gameObject.transform.parent).transform);
                    mr.gameObject.AddComponent<BoxCollider>();
                    mr.gameObject.AddComponent<Rigidbody>().useGravity = true;
                    mr.transform.parent = null;
                }
            }
            else
            {
                foreach(GameObject go in meshesForReassembly)
                {
                    go.AddComponent<BoxCollider>();
                    go.gameObject.AddComponent<Rigidbody>().useGravity = true;
                    go.transform.parent = null;
                }
            }
            StartCoroutine(RespawnAnim());
        }
        
    }
    IEnumerator RespawnAnim()
    {
        yield return new WaitForSeconds(3);
        Respawn();
        foreach (GameObject go in meshesForReassembly)
        {
            Destroy(go.GetComponent<Rigidbody>());
            Destroy(go.GetComponent<BoxCollider>());
        }
        bool notDone = true;
        float moveSpeed = 0f;
        float rotateSpeed = 0f;

        while (notDone)
        {
            moveSpeed += Time.deltaTime;
            rotateSpeed += Time.deltaTime;
            notDone = true;
            for(int i = 0;i < meshesForReassembly.Count; i++)
            {
                meshesForReassembly[i].transform.position = Vector3.MoveTowards(meshesForReassembly[i].transform.position, reassemblyPoints[i].position,moveSpeed);
                meshesForReassembly[i].transform.rotation = Quaternion.RotateTowards(meshesForReassembly[i].transform.rotation, reassemblyPoints[i].transform.rotation, rotateSpeed);
                if (!(meshesForReassembly[i].transform.position == reassemblyPoints[i].transform.position && meshesForReassembly[i].transform.rotation == reassemblyPoints[i].rotation))
                {
                    notDone = false;
                }
                else
                {
                    meshesForReassembly[i].transform.parent = reassemblyPoints[i].parent;
                }
            }
            yield return null;
        }
        curState = EStates.idle;
    }
    public void Respawn()
    {
        transform.position = SpawnPoint.currentSpawn.transform.position + new Vector3(0, 0.824561f,0);
        transform.rotation = Quaternion.Euler(0, SpawnPoint.currentSpawn.transform.rotation.y, 0);
        GetComponentInChildren<Animator>().enabled = true;
        
    }
    #endregion
}
