using UnityEngine;

public class EnemyKiller : GameAction
{
    public override void Activate()
    {
        FindAnyObjectByType<Enemy>().Kill();
    }

    public override void Deactivate()
    {
        
    }
}
