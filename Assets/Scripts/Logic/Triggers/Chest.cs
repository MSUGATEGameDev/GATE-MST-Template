using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : GameAction
{
    #region Variables
    [Tooltip("This will be given to the player when they open the chest.")] public Collectible collectibleToPresent;
    Collectible instantiatedCollectible;

    [Header("Components")]
    [Tooltip("Helps animate the collectible that will be given to the player.")] public Transform thingHolder;
    public List<GameAction> activatesOnOpen = new();
    Animator animator;
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public override void Activate()
    {
        animator.Play("ChestOpen");
        StartCoroutine(GetThingSoon());

    }
    IEnumerator GetThingSoon()
    {
        yield return new WaitForSeconds(.5f);
        instantiatedCollectible = Instantiate(collectibleToPresent, thingHolder);
        instantiatedCollectible.transform.localPosition = Vector3.zero;
        instantiatedCollectible.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(4);
        instantiatedCollectible.OnCollection();
        foreach(GameAction action in activatesOnOpen)
        {
            action.Activate();
        }
    }
    public override void Deactivate()
    {
        animator.Play("ChestClose");
    }
}
