using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform waypoint;

    public void SpawnWaypoint(Transform coords)
    {
        GameObject wayp = Instantiate(waypoint.gameObject, coords.position, coords.rotation);
        wayp.transform.parent = gameObject.transform;
        LevelGeneration.objectLocations.Add(wayp.transform);
    }
}
