using UnityEngine;

public class EnemyKiller : GameAction
{
    [ReadOnly]
    [TextArea(1, 10)]
    public string _ = "-- GameAction --\n" +
        "A test action that kills a random enemy with a button.";
    public override void Activate()
    {
        FindAnyObjectByType<Enemy>().Kill();
    }

    public override void Deactivate()
    {
        
    }
}
