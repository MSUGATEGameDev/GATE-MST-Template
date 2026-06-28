using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
//using UnityEngine.UIElements;

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
        if(curState != EStates.dead)
        {
            Vector2 moveValue = context.ReadValue<Vector2>();
            Move(moveValue);
        }
    }
    public void OnJumpButton(InputAction.CallbackContext context)
    {
        Jump();
    }
    public void OnAttackButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack();
        }
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
        List<float> moveSpeeds = new();
        List<float> rotateSpeeds = new();
        List<Vector3> firstPosition = new();
        List<float> finalSpeeds = new();
        Transform spotFinder = Instantiate(new GameObject("Empty"),transform).transform;
        Transform spot = Instantiate(new GameObject("Empty"), spotFinder).transform;
        spot.localPosition = new Vector3(.6f, 0, 0);
        foreach (GameObject go in meshesForReassembly)
        {
            Destroy(go.GetComponent<Rigidbody>());
            Destroy(go.GetComponent<BoxCollider>());
            moveSpeeds.Add(0);
            rotateSpeeds.Add(0);
            spotFinder.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            firstPosition.Add(spot.position);
            finalSpeeds.Add(Random.Range(6f,10f));
        }
        Destroy(spotFinder.gameObject);
        bool notDone = true;

        while (notDone)
        {
            notDone = false;
            for (int i = 0; i < meshesForReassembly.Count; i++)
            {
                if (moveSpeeds[i] < finalSpeeds[i])
                {
                    moveSpeeds[i] += Random.Range(1f, 5f) * Time.deltaTime;
                }
                meshesForReassembly[i].transform.position = Vector3.MoveTowards(meshesForReassembly[i].transform.position, firstPosition[i], moveSpeeds[i] * Time.deltaTime);
                if (!(meshesForReassembly[i].transform.position == firstPosition[i]))
                {
                    notDone = true;
                }
            }
            yield return null;
        }

        for (int i = 0; i < meshesForReassembly.Count; i++)
        {
            moveSpeeds[i] = 0;
        }
            notDone = true;
        while (notDone)
        {
            notDone = false;
            for(int i = 0;i < meshesForReassembly.Count; i++)
            {
                if (moveSpeeds[i] < finalSpeeds[i]) {
                    moveSpeeds[i] += Random.Range(3f, 10f) * Time.deltaTime;
                }
                rotateSpeeds[i] += Random.Range(100f,360f) * Time.deltaTime;
                meshesForReassembly[i].transform.position = Vector3.MoveTowards(meshesForReassembly[i].transform.position, reassemblyPoints[i].position, moveSpeeds[i]*Time.deltaTime);
                meshesForReassembly[i].transform.rotation = Quaternion.RotateTowards(meshesForReassembly[i].transform.rotation, reassemblyPoints[i].transform.rotation, rotateSpeeds[i]*Time.deltaTime);
                if ((meshesForReassembly[i].transform.position == reassemblyPoints[i].transform.position && meshesForReassembly[i].transform.rotation == reassemblyPoints[i].rotation))
                {
                    meshesForReassembly[i].transform.parent = reassemblyPoints[i].parent;
                }
                else
                {
                    notDone = true;
                }
            }
            yield return null;
        }
        GetComponentInChildren<Animator>().enabled = true;
        curState = EStates.idle;
    }
    public void Respawn()
    {
        transform.position = SpawnPoint.currentSpawn.transform.position + new Vector3(0, 0.824561f,0);
        transform.rotation = Quaternion.Euler(0, SpawnPoint.currentSpawn.transform.rotation.y, 0);
        
    }
    #endregion
}
