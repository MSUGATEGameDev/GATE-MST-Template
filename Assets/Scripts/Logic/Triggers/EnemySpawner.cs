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
    Transform placer;
    private void Awake()
    {
        placer = transform.GetChild(0);        
    }
    public override void Activate()
    {
        if(enemyCount > 0)
        {
            enemyCount--;
            SpawnEnemy();
        }
        else if (enemyCount == -1)
        {
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        placer.localPosition = new Vector3(Random.Range(0, 1), .824561f, Random.Range(0, 1));
        Instantiate(enemyPrefab,placer.position,Quaternion.Euler(0,Random.Range(0,360),0));
    }
    public override void Deactivate()
    {
        // Does nothing.
    }
}
