using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MoveObject : GameAction
{
    public List<Transform> thingsToMove;
    public float speed;
    public bool faceObjectsOnMove = false;
    Coroutine moveCoroutine;

    IEnumerator MoveObjects()
    {
        yield return null;
    }

    public override void Activate()
    {
        moveCoroutine = StartCoroutine(MoveObjects());
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }
}
