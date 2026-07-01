using UnityEngine;

public class SpawnPoint : GameAction // Used for bringing the player back into the game.
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "Spawns the player into the game and provides a checkpoint after dying.";
    [HideInInspector]public static SpawnPoint currentSpawn; // The next place the player will spawn on death.

    [Tooltip("The spawn point for the beginning of the level. Only enable for one spawn point.")] public bool startPoint = false;

    private void Start() // When the level loads.
    {
        if (startPoint) // For the start point.
        {
            currentSpawn = this; // Set this as the initial spawn.
            Player.singleton.Respawn(); // Spawn in the player to the start point.
        }
    }

    public override void Activate() // When activated by a trigger, this spawn point becomes the main spawn point. Allowing for checkpoints.
    {
        currentSpawn = this;
    }
    public override void Deactivate() // Nothing happens when deactivated.
    {
        
    }
}
