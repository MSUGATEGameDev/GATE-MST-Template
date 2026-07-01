using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Teams")]
    [SerializeField] private int _teamID = 0;

    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int startHealth = 100;
    private int _curHealth = 0;
    public bool invincible = false;

    [Header("Lives")]
    [SerializeField] private bool hasLives = false;
    [SerializeField] private int maxLives = 3;
    [SerializeField] private int startLives = 3;
    private int _curLives = 0;

    [Header("Inscribed")]
    private Entity entity;

    //[Header("Dynamic")]

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<Entity>();
        CurHealth = startHealth;
        CurLives = startLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurHealth == 0)
        {
            if (CurLives > 0 & hasLives)
            {
                CurHealth = startHealth;
                CurLives--;
            }
            else
            {
                if (entity != null)
                {
                    entity.Die();
                }
                else
                {
                    Die();
                }
            }
        }
    }

    public void Damage(int amount)
    {
        if (!invincible)
        {
            CurHealth -= amount;
            if (CurHealth < 0)
            {
                CurHealth = 0;
            }
            if (entity != null)
            {
                entity.Damaged();
            }

            if (this.CompareTag("Player"))
            {
                HUD.DisplayHealth((float)CurHealth / (float)maxHealth * 100f);
            }
            else if (this.CompareTag("Enemy"))
            {
                GetComponent<Enemy>().DisplayHealth((float)CurHealth / (float)maxHealth * 100f);
            }
        }
    }

    public void Heal(int amount)
    {
        CurHealth += amount;
        if (CurHealth > maxHealth)
        {
            CurHealth = maxHealth;
        }
        if (this.CompareTag("Player"))
            HUD.DisplayHealth((float)CurHealth / (float)maxHealth * 100f);
    }

    public void FullHeal(bool useStartHP = false)
    {
        if (useStartHP)
        {
            CurHealth = startHealth;
        }
        else
        {
            CurHealth = maxHealth;
        }
        if (this.CompareTag("Player"))
            HUD.DisplayHealth((float)CurHealth / (float)maxHealth * 100f);
    }

    public void TakeLives(int amount)
    {
        if (!invincible)
        {
            CurLives -= amount;
            if (entity != null)
            {
                //Do Something when live is lost
            }
        }
    }

    public void GiveLives(int amount)
    {
        CurLives += amount;
        if (CurLives > maxLives)
        {
            CurLives = maxLives;
        }
    }

    public void FillLives(bool useStartLives = false)
    {
        if (useStartLives)
        {
            CurLives = startLives;
        }
        else
        {
            CurLives = maxLives;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public int CurHealth
    {
        get { return _curHealth; }
        set { _curHealth = value; }
    }

    public int CurLives
    {
        get { return _curLives; }
        set { _curLives = value; }
    }

    public int TeamID
    {
        get { return _teamID; }
        private set { _teamID = value; }
    }
}