using UnityEngine;

public class EnemySpawner : GameTrigger
{
    [Header("Settings")]
    [Tooltip("-1 is Infinite")]public int enemyCount = -1;
    public Enemy enemyPrefab;
}
