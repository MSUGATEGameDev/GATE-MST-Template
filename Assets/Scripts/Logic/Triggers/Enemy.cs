using UnityEngine;

public class Enemy : Triggerable
{
    [Header("Stats")]
    public int initHealth;
    public int damage;
    public int speed;

    // Current Details
    int health;
}
