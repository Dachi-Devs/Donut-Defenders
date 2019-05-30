using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public Transform nodeT;
    public Transform End;

    public static List<Transform> nodes = new List<Transform>();

    public void SpawnNode(Vector3 coords)
    {
        GameObject node = Instantiate(nodeT.gameObject, coords, Quaternion.identity);
        node.transform.parent = gameObject.transform;
        nodes.Add(node.transform);
    }
}
