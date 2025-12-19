using UnityEngine;

public class EnemySpawner : Triggerable
{
    [Header("Settings")]
    [Tooltip("-1 is Infinite")]public int enemyCount = -1;
    public Enemy enemyPrefab;
}
