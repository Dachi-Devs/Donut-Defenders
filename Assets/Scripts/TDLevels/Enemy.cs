using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float moveSpeed;

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

    public void TakeDamage(float dam)
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

    public void Slow(float slow)
    {
        moveSpeed = startSpeed * (1 - slow);
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
