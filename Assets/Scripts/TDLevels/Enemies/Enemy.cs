using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [SerializeField]
    public float moveSpeed;
    private float slowCountdown;


    public float maxHealth = 100f;
    [HideInInspector]
    public float health;
    public Image healthBar;

    public int bounty = 10;

    public GameObject deathEffect;

    void Start()
    {
        health = maxHealth;
        moveSpeed = startSpeed;
    }

    void Update()
    {
        if(slowCountdown > 0)
        {
            slowCountdown -= Time.deltaTime;
            return;
        }
        ResetSpeed();
    }

    public void EnemyHit(float dam, float slowP, float slowD)
    {
        TakeDamage(dam);
        if(slowP > 0)
            Slow(slowP, slowD);
    }

    private void TakeDamage(float dam)
    {
        health -= dam;

        UpdateHealthBar();

        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        float healthPercent = health / maxHealth;
        healthBar.fillAmount = healthPercent;
    }

    private void Slow(float slow, float duration)
    {
        slowCountdown = duration;
        if (startSpeed * (slow/100) <= moveSpeed)
        {
            moveSpeed = startSpeed * (1 - (slow/100));
        }
    }

    private void  ResetSpeed()
    {
        moveSpeed = startSpeed;
    }

    void Die()
    {
        PlayerStats.energy += bounty;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
        WaveSpawner.enemyCount--; 
        Destroy(gameObject);
    }
}
