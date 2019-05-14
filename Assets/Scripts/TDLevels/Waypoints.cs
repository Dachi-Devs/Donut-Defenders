using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform End;

    public static Transform[] points;

    void Awake()
    {
        points = new Transform[transform.childCount + 1];
        for (int i = 0; i < points.Length - 1; i++)
        {
            points[i] = transform.GetChild(i);
        }
        points[points.Length - 1] = End;
    }
}
