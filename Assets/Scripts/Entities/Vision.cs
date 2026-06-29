//Based Off 3D Enemy FOV Tut https://www.youtube.com/watch?v=j1-OyLo77ss

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public float radius;
    [Range(0, 360)] public float angle;

    public GameObject targetObject;
    public Entity entity;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeeTarget;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<Entity>();
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if ((!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))&& Player.singleton.curState != Entity.EStates.dead)
                {
                        targetObject = target.gameObject;
                        canSeeTarget = true;
                }
                else
                {
                    canSeeTarget = false;
                }
            }
            else
            {
                canSeeTarget = false;
            }
        }
        else if (canSeeTarget)
        {
            canSeeTarget = false;
        }
    }
}