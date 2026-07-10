using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class KillSomething : GameAction
{
    [Header("What To Kill")]
    [Tooltip("When activated, this will kill the player.")]public bool player;
    [Tooltip("When activated, this will kill all enemies loaded into the game.")]public bool allEnemies;
    [Tooltip("When activaed, this will kill all entities listed.")]public List<Entity> specificEntities;

    public override void Activate()
    {
        if (player)
        {
            Player.singleton.Die();
        }
        if (allEnemies)
        {
            foreach(Enemy nme in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
            {
                nme.Die();
            }
        }
        foreach (var entity in specificEntities) {
            entity.Die();
        }
        specificEntities.Clear();
    }

    public void AddVictim(Entity victim)
    {
        specificEntities.Add(victim);
    }

    public override void Deactivate()
    {
    }
}
