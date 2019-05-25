using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private float speed;
    private float explosionRadius;
    private float slowPct;
    private float slowDur;
    private int damage;

    public float rotSpeed = 10f;
    [SerializeField]
    private SpriteRenderer spr;
    private BulletBlueprint bulletBP;

    public void Setup(TurretBlueprint turr)
    {
        bulletBP = turr.bullet;
        speed = bulletBP.speed;
        explosionRadius = bulletBP.baseExplosionRadius * turr.explosionRadiusMod;
        damage = bulletBP.baseDamage * turr.damageMod;
        slowPct = bulletBP.baseSlowPct * turr.slowPctMod;
        slowDur = bulletBP.baseSlowDur * turr.slowDurMod;
        spr.sprite = bulletBP.spr;
    }

    public void Seek (Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            //HIT
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        RotateToTarget();
    }

    void RotateToTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.forward);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotSpeed * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation.z);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(bulletBP.impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 1f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitObjects)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage (Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
            e.Slow(slowPct, slowDur);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
