using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MoveObject : GameAction
{
    public Transform thingToMove;
    [Tooltip("Meters Per Second")]public float speed = 1;
    
    public bool rotateObjectOnMove = false;
    [Tooltip("Degrees Per Second")] public float rotationAngle = 180;
    public List<GameAction> toDoAfterMove;
    Coroutine moveCoroutine;

    IEnumerator MoveObjects()
    {
        if (rotateObjectOnMove)
        {
            while (thingToMove.position != transform.position || thingToMove.rotation != transform.rotation)
            {
                thingToMove.position = Vector3.MoveTowards(thingToMove.position, transform.position, speed * Time.deltaTime);
                thingToMove.rotation = Quaternion.RotateTowards(thingToMove.rotation, transform.rotation, rotationAngle * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            while (thingToMove.position != transform.position)
            {
                thingToMove.position = Vector3.MoveTowards(thingToMove.position, transform.position, speed*Time.deltaTime);
                yield return null;
            }
        }
        
        foreach (GameAction toDo in toDoAfterMove)
        {
            toDo.Activate();
        }
    }

    public override void Activate()
    {
        moveCoroutine = StartCoroutine(MoveObjects());
    }

    public override void Deactivate()
    {
        StopCoroutine(moveCoroutine);
    }
}
