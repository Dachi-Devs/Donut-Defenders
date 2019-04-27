using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float moveSpeed;

    public float health = 100f;
    public int bounty = 10;

    public GameObject deathEffect;

    void Start()
    {
        moveSpeed = startSpeed;
    }

    public void TakeDamage(float dam)
    {
        health -= dam;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float slow)
    {
        moveSpeed = startSpeed * (1 - slow);
    }

    void Die()
    {
        PlayerStats.Money += bounty;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
        Destroy(gameObject);
    }
}
