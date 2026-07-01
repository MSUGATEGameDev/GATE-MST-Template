using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class Enemy : Entity
{
    [Header("Enemy Parts")]

    public GameObject healthBarHolder;
    public Transform healthBar;
    public Image healthBarImage;
    public float searchRate = 2f;
    public float randomRate = 2f;
    private float nextSearchTime = 0f;
    private float nextRandomTime = 0f;

    //private float turnTime = 0.1f;
    //private float turnVel;

    private Vision vision;

    private int searchIndex = 0;
    public AIStates curAIState = AIStates.idle;

    [HideInInspector] public EnemySpawner spawner;
    [Tooltip("The actions that will be carried out when the enemy dies.")]public List<GameAction> actions = new();

    public enum AIStates
    {
        idle,
        wander,
        roomba,
        searching,
        pursue,
    }

    protected override void Start()
    {
        base.Start();
        vision = GetComponent<Vision>();
    }
    public void DisplayHealth(float percentage)
    {
        if(percentage > 0)
        {
            healthBarHolder.SetActive(true);
            healthBar.localScale = new Vector3(percentage / 100, 1, 1);
            if (percentage > 50)
            {
                healthBarImage.color = new Color((50 - (percentage - 50)) / 50, 1, 0);
            }
            else
            {
                healthBarImage.color = new Color(1, percentage / 50, 0);
            }
        }
        else
        {
            healthBarHolder.SetActive(false);
        }
    }
    protected override void Update()
    {
        if (curState != EStates.dead)
        {
            base.Update();

            if (vision.canSeeTarget)
            {
                searchIndex = 0;
                curAIState = AIStates.pursue;
            }
            else if (curAIState == AIStates.pursue)
            {
                searchIndex = 0;
                curAIState = AIStates.searching;
                
            }

            if (Time.time >= nextRandomTime)
            {
                if (curAIState == AIStates.wander || curAIState == AIStates.idle)
                {
                    curAIState = (AIStates)Random.Range(0, 2);
                }

                nextRandomTime = Time.time + randomRate;
            }

            switch (curAIState)
            {
                case AIStates.idle:
                    setEntityLights(Color.white);
                    Move(Vector2.zero);
                    break;
                case AIStates.wander:
                    setEntityLights(Color.white);
                    Wander();
                    break;
                case AIStates.searching:
                    setEntityLights(Color.yellow);
                    Move(Vector2.zero);
                    Search();
                    break;
                case AIStates.roomba:
                    setEntityLights(Color.white);
                    Move(Vector2.zero);
                    Search();
                    break;
                case AIStates.pursue:
                    setEntityLights(Color.red);
                    Move(Vector2.zero);
                    Pursue();
                    break;
            }
        }
    }

    public void Wander()
    {
        if (curState == EStates.idle)
        {
            running = false;
            Move(EntityUtils.HeadingToVec2(EntityUtils.GetRandomHeading()));
        }
    }

    public void Pursue()
    {
        if (vision.targetObject == null) return;
        Vector3 heading = vision.targetObject.transform.position - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        Vector2 vec = new Vector2(direction.x, direction.z);

        if (distance > 1f)
        {
            running = true;
            Move(vec);
        }
        else
        {
            running = false;
            Move(Vector2.zero);
            Attack();
        }
    }

    public void Roomba()
    {
        running = false;
        Move(EntityUtils.HeadingToVec2((Headings)searchIndex));

        if (Time.time >= nextSearchTime)
        {
            if (searchIndex < 8)
            {
                searchIndex++;
            }
            else
            {
                searchIndex = 0;
                curAIState = AIStates.wander;
            }
            nextSearchTime = Time.time + searchRate;
        }
    }

    public void Search()
    {
        running = false;
        Move(EntityUtils.HeadingToVec2((Headings)searchIndex));

        if (Time.time >= nextSearchTime)
        {
            if (searchIndex < 8)
            {
                searchIndex++;
            }
            else
            {
                searchIndex = 0;
                curAIState = AIStates.wander;
            }
            nextSearchTime = Time.time + searchRate;
        }
    }

    public override void Die()
    {
        if(curState != EStates.dead)
        {
            base.Die();
            healthBarHolder.SetActive(false);
            if (spawner != null)
            {
                spawner.ReportDeath();
            }
            foreach (GameAction action in actions)
            {
                action.Activate();
            }
            foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
            {
                try { mr.gameObject.AddComponent<BoxCollider>(); } catch { }
                try
                {
                    mr.gameObject.AddComponent<Rigidbody>().useGravity = true;
                }
                catch { }
            }
            
            StartCoroutine(Despawn());
        }
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (curAIState == AIStates.idle || curAIState == AIStates.wander)
        {
            curAIState = AIStates.roomba;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (curAIState == AIStates.roomba)
        {
            curAIState = AIStates.idle;
        }
    }
}
