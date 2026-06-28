using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Entity
{
    #region Enemy Parts
    [Header("Enemy Parts")]
    #endregion

    public float randomRate = 2f;
    private float nextRandomTime = 0f;
    public AIStates curAIState = AIStates.idle;
    [HideInInspector] public EnemySpawner spawner;
    [Tooltip("The actions that will be carried out when the enemy dies.")]public List<GameAction> actions = new();

    public enum AIStates
    {
        idle,
        wander,
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (curState != EStates.dead)
        {
            base.Update();

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
                    Move(Vector2.zero);
                    break;
                case AIStates.wander:
                    Wander();
                    break;
            }
        }
    }

    public void Wander()
    {
        if (curState == EStates.idle)
        {
            Move(EntityUtils.HeadingToVec2(EntityUtils.GetRandomHeading()));
        }
    }

    public override void Die()
    {
        if(curState != EStates.dead)
        {
            base.Die();
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
}
