using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// --- PlayerController ---
/// Handles device input, the HUD, and stores a couple of more broadly-used shared variables.
/// Access via singleton: PlayerController.singleton
/// </summary>
public class Player : Entity
{
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
            //DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    #region Player Parts
    [Header("Player Parts")]
    [HideInInspector] public Transform playerCamTransform; // The transform that controls the position of the main camera, which can be referenced by other classes to make things look at the player.
    private Camera playerCamera;

    [SerializeField]
    private GameObject pauseMenu;

    //Interal Cinema Values
    public Camera cinemaCamera;
    private bool cinemaCamActive = false;
    private FocusCamera cinemaCamtarget;
    private float nextReleaseTime = 0f;

    //Player Light Timers
    private bool lightTimerActive = false;
    private float nextLightTime = 0f;
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
        if (lightTimerActive)
        {
            if (Time.time >= nextLightTime)
            {
                lightTimerActive = false;
                setEntityLights(Color.white);
            }
        }
        if (cinemaCamActive)
        {
            FlyCam();
        }
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
    public void OnPushButton(InputAction.CallbackContext context)
    {
        pushing = context.ReadValueAsButton();
    }
    public void OnActionButton(InputAction.CallbackContext context)
    {
        if (context.started && curState != EStates.dead && curState != EStates.disabled)
        {
            if (triggerInRange != null)
            {
                setPlayerLight(Color.green, 2.0f);
                anim.Play("TutBotPushButton");
                triggerInRange.Activate();

            }
        }

    }
    public void OnPauseButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Pause();
        }
    }
    #endregion
    #region Events

    public void setPlayerLight(Color color, float delay)
    {
        nextLightTime = Time.time + delay;
        lightTimerActive = true;
        setEntityLights(color);
    }

    public void DeployCinemaCam(FocusCamera target)
    {
        playerCamera.gameObject.SetActive(false);
        cinemaCamera.gameObject.SetActive(true);
        disabled = true;

        cinemaCamera.transform.position = playerCamera.transform.position;
        cinemaCamera.transform.localEulerAngles = playerCamera.transform.localEulerAngles;

        cinemaCamtarget = target;
        health.invincible = true;
        cinemaCamActive = true;
    }

    private void FlyCam()
    {
        if (cinemaCamtarget != null)
        {
            float stepRot = cinemaCamtarget.dollySpeed / 8 * Time.deltaTime;
            float stepPos = cinemaCamtarget.dollySpeed * Time.deltaTime;
            Vector3 curPos = Vector3.MoveTowards(cinemaCamera.transform.position, cinemaCamtarget.transform.position, stepPos);
            Vector3 curRot = Vector3.RotateTowards(cinemaCamera.transform.forward, cinemaCamtarget.transform.forward, stepRot, 0.0f);
            cinemaCamera.transform.position = curPos;
            cinemaCamera.transform.rotation = Quaternion.LookRotation(curRot);

            if (curPos == cinemaCamtarget.transform.position)
            {
                if (Time.time >= nextReleaseTime)
                {
                    playerCamera.gameObject.SetActive(true);
                    cinemaCamera.gameObject.SetActive(false);
                    cinemaCamera.transform.position = playerCamera.transform.position;
                    cinemaCamera.transform.localEulerAngles = playerCamera.transform.localEulerAngles;
                    cinemaCamActive = false;
                    disabled = false;
                    health.invincible = false;
                }
            } 
            else
            {
                nextReleaseTime = Time.time + cinemaCamtarget.camHold;
            }
        }
    }

    public void Pause()
    {
        if (!pauseMenu.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void BackToTitle()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }
    
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
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            StartCoroutine(RespawnAnim());
        }
        
    }
    /// <summary>
    /// The process of animating the visual of a respawn.
    /// </summary>
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
        health.FullHeal();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        anim.enabled = true;
        curState = EStates.idle;
        anim.Play("TutBotIdle");
    }
    /// <summary>
    /// Puts the player in the spawn point.
    /// </summary>
    public void Respawn()
    {
        transform.position = SpawnPoint.currentSpawn.transform.position + new Vector3(0, 0.824561f,0);
        transform.localEulerAngles = new Vector3(0, SpawnPoint.currentSpawn.transform.localEulerAngles.y, 0);
    }
    #endregion
    /// <summary>
    /// Initiates the animation of teleporting to a new location.
    /// </summary>
    /// <param name="destination">Destination to teleport to.</param>
    public void Teleport(Vector3 destination)
    {
        StartCoroutine(TeleportAnim(destination));
    }
    /// <summary>
    /// The process of animating a teleport to a new location.
    /// </summary>
    /// <param name="destination">Desitnation to teleport to.</param>
    public IEnumerator TeleportAnim(Vector3 destination)
    {
        anim.enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        List<float> moveSpeeds = new();
        List<float> rotateSpeeds = new();
        List<Vector3> firstPosition = new();
        List<float> finalSpeeds = new();
        Transform spotFinder = Instantiate(new GameObject("Empty"), transform).transform;
        Transform spot = Instantiate(new GameObject("Empty"), spotFinder).transform;
        spot.localPosition = new Vector3(.6f, 0, 0);
        if (meshesForReassembly.Count == 0)
        {
            foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
            {
                meshesForReassembly.Add(mr.gameObject);
                reassemblyPoints.Add(Instantiate(new GameObject("Empty"), mr.gameObject.transform.position, mr.gameObject.transform.rotation, mr.gameObject.transform.parent).transform);
                mr.transform.parent = null;
                moveSpeeds.Add(0);
                rotateSpeeds.Add(0);
                spotFinder.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                firstPosition.Add(spot.position);
                finalSpeeds.Add(Random.Range(6f, 10f));
            }
        }
        else
        {
            foreach (GameObject go in meshesForReassembly)
            {
                go.transform.parent = null;
                moveSpeeds.Add(0);
                rotateSpeeds.Add(0);
                spotFinder.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                firstPosition.Add(spot.position);
                finalSpeeds.Add(Random.Range(6f, 10f));
            }
        }
        
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
        transform.position = destination;
        moveSpeeds.Clear();
        rotateSpeeds.Clear();
        firstPosition.Clear();
        foreach (GameObject go in meshesForReassembly)
        {
            moveSpeeds.Add(0);
            rotateSpeeds.Add(0);
            spotFinder.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            firstPosition.Add(spot.position);
        }
        Destroy(spotFinder.gameObject);
        notDone = true;
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
            rotateSpeeds[i] = 0;
        }
        notDone = true;
        while (notDone)
        {
            notDone = false;
            for (int i = 0; i < meshesForReassembly.Count; i++)
            {
                if (moveSpeeds[i] < finalSpeeds[i])
                {
                    moveSpeeds[i] += Random.Range(3f, 10f) * Time.deltaTime;
                }
                rotateSpeeds[i] += Random.Range(100f, 360f) * Time.deltaTime;
                meshesForReassembly[i].transform.position = Vector3.MoveTowards(meshesForReassembly[i].transform.position, reassemblyPoints[i].position, moveSpeeds[i] * Time.deltaTime);
                meshesForReassembly[i].transform.rotation = Quaternion.RotateTowards(meshesForReassembly[i].transform.rotation, reassemblyPoints[i].transform.rotation, rotateSpeeds[i] * Time.deltaTime);
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
        health.FullHeal();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        anim.enabled = true;
        curState = EStates.idle;
    }
}
