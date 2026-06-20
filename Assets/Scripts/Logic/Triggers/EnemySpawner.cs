using UnityEngine;

public class EnemySpawner : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
    "Causes an enemy to appear.";
    [Header("Settings")]
    [Tooltip("-1 is Infinite")]public int enemyCount = -1;
    public Enemy enemyPrefab;

    public override void Activate()
    {

    }

    public override void Deactivate()
    {

    }
}
