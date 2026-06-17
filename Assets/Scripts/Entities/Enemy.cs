using UnityEngine;

public class Enemy : Entity
{
    #region Enemy Parts
    [Header("Enemy Parts")]
    #endregion

    public float randomRate = 2f;
    private float nextRandomTime = 0f;
    public AIStates curAIState = AIStates.idle;

    public enum AIStates
    {
        idle,
        wander
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        if (Time.time >= nextRandomTime)
        {
            if (curAIState == AIStates.wander || curAIState == AIStates.idle)
            {
                curAIState = (AIStates)Random.Range(0, 2);
            }

            nextRandomTime = Time.time + randomRate;
        }

        switch (curAIState)
        {
            case AIStates.idle:
                Move(Vector2.zero);
                break;
            case AIStates.wander:
                Wander();
                break;
        }
    }

    public void Wander()
    {
        if (curState == EStates.idle)
        {
            Move(EntityUtils.HeadingToVec2(EntityUtils.GetRandomHeading()));
        }
    }
}
