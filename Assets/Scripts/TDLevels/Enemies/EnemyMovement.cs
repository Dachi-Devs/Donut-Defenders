using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    private Enemy enemy;

    public float rotSpeed = 10f;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = LevelGeneration.objectLocations[0];
    }

    void Update()
    {
        MoveToWaypoint();
        RotateToPath();

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void MoveToWaypoint()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.moveSpeed * Time.deltaTime, Space.World);
    }

    void RotateToPath()
    {
        Vector3 dir = target.position - transform.position;
        if(dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.forward);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotSpeed * Time.deltaTime).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation.z);
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= LevelGeneration.objectLocations.Count - 1)
        {
            EndPath();
            return;
        }

        waypointIndex++;
        target = LevelGeneration.objectLocations[waypointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.enemyCount--;
        Destroy(gameObject);
    }
}
