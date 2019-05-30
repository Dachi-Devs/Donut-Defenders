using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    private float explosionRadius;
    private float slowPct;
    private float slowDur;
    private int damage;

    [SerializeField]
    private SpriteRenderer spr;
    private ContactBlueprint contactBP;

    void Start()
    {
        BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Enemy e = col.gameObject.GetComponent<Enemy>();
            if (e != null)
            {
                e.EnemyHit(damage, slowPct, slowDur);
            }

            Destroy(gameObject);
            return;
        }
    }

    public void Setup(TurretBlueprint turr)
    {
        contactBP = turr.contact;
        explosionRadius = contactBP.baseExplosionRadius * turr.explosionRadiusMod;
        damage = contactBP.baseDamage * turr.damageMod;
        slowPct = contactBP.baseSlowPct * turr.slowPctMod;
        slowDur = contactBP.baseSlowDur * turr.slowDurMod;
        spr.sprite = contactBP.spr;
    }
}
