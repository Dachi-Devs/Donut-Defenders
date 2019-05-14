using UnityEngine;
using System.Collections;

public class MenuTurret : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float rotSpeed = 10f;
    public float maxAngle = 70f;
    public float fireRate;
    private float fireCountdown = 1f;

    public Transform firePoint;

    void Update()
    {
        fireCountdown -= Time.deltaTime;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        float angle = Mathf.PingPong(Time.time*rotSpeed, maxAngle * 2) - maxAngle;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
