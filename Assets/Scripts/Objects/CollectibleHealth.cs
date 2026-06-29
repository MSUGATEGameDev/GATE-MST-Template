using UnityEngine;

public class CollectibleHealth : Collectible
{
    [Header("Key Settings")]
    public int healAmount = 10;

    // Update is called once per frame
    public override void OnCollection()
    {
        base.OnCollection();

        GameObject player = Player.singleton.gameObject;

        if (player.GetComponent<Health>() != null)
        {
            player.GetComponent<Health>().Heal(healAmount);
        }
    }
}
