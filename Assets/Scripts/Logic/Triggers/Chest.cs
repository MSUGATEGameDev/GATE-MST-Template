using System.Collections;
using UnityEngine;

public class Chest : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "Contains a reward. Openable by key or action.";
    public Collectible collectibleToPresent;
    Collectible instantiatedCollectible;
    public Transform thingHolder;

    Animator animator;
    private void Start()
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
    }
    public override void Deactivate()
    {
        animator.Play("ChestClose");
    }
}
