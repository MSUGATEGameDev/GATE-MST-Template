using UnityEngine;

public class Enemy : GameTrigger
{
    [Header("Stats")]
    public int initHealth;
    public int damage;
    public int speed;

    // Current Details
    int health;
}
