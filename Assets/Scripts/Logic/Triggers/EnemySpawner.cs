using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
    "Causes enemies to appear.";
    [Header("Settings")]
    [Tooltip("Total amount of enemies it will spawn before shutting off - -1 is Infinite")]public int enemyCount = -1;
    [Tooltip("How frequently it will spawn enemies when activated (in seconds).")]public int spawnFrequency = 3;
    [Tooltip("The max amount of enemies that will be spawned in total.")]public int maxConcurrentSpawn = 5;
    [Tooltip("The enemy that will be spawned when spawned.")]public Enemy enemyPrefab;
    [Tooltip("Actions that will be assigned to the enemy when spawned.")]public List<GameAction> actionsForEnemies;
    Transform placer;
    int concurrentEnemies;
    Coroutine spawnRun;
    private void Awake()
    {
        placer = transform.GetChild(0);        
    }
    public override void Activate()
    {
        spawnRun = StartCoroutine(SpawnProcess());
    }
    void SpawnEnemy()
    {
        concurrentEnemies++;
        if (enemyCount > 0)
        {
            enemyCount--;
        }
            placer.localPosition = new Vector3(Random.Range(0, 1), .824561f, Random.Range(0, 1));
        Enemy thisEnemy = Instantiate(enemyPrefab,placer.position,Quaternion.Euler(0,Random.Range(0,360),0));
        thisEnemy.spawner = this;
        foreach(GameAction action in actionsForEnemies)
        {
            thisEnemy.actions.Add(action);
        }
        HUD.ShowHealthBar();
    }
    public override void Deactivate()
    {
        StopCoroutine(spawnRun);
    }

    IEnumerator SpawnProcess()
    {
        while (enemyCount == -1 || enemyCount > 0)
        {
            if (concurrentEnemies < maxConcurrentSpawn)
            {
                SpawnEnemy();
            }
            
            yield return new WaitForSeconds(spawnFrequency);
        }
        
    }
    public void ReportDeath()
    {
        concurrentEnemies--;
    }
}
