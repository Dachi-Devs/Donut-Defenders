using UnityEngine;

public class TurretController : MonoBehaviour
{

    private Transform target;
    private Enemy targetEnemy;
    [SerializeField]
    private TurretBlueprint turretBP;
    [SerializeField]
    private SpriteRenderer sprite;

    private float range;
    private float fireRate;
    private float fireCountdown;

    [Header("Editor Setup")]
    public float rotSpeed = 12f;
    public string enemyTag = "Enemy";
    public Transform firePoint;
    [SerializeField]
    private GameObject bulletObj;

    // Start is called before the first frame update
    void Start()
    {
        if(turretBP != null)
        {
            Setup(turretBP);
        }
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && shortestDistance <= range)
        {
            target = closestEnemy.transform;
            targetEnemy = closestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        fireCountdown -= Time.deltaTime;

        if (target == null)
        {
            return;
        }

        else
        {
            LockOnTarget();
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.forward);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotSpeed * Time.deltaTime).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation.z);
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletObj, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Setup(turretBP);
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Setup(TurretBlueprint turr)
    {
        turretBP = turr;
        sprite.sprite = turretBP.spr;
        range = turretBP.range;
        fireRate = turretBP.fireRate;
    }

    public void Overcharge()
    {
        sprite.sprite = turretBP.ocSpr;
    }
}
