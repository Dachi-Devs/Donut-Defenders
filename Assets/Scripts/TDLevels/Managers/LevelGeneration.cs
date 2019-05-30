using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Nodes nodeGen;
    public int nodeCount;
    public int waypointCount;
    public Waypoints waypointGen;
    public GameObject waypointsObj;
    public GameObject start;
    public GameObject startObj;
    public GameObject end;
    public Transform spawnPos;
    public float xMin = Mathf.Infinity;
    public float xMax = Mathf.NegativeInfinity;
    public float yMin = Mathf.Infinity;
    public float yMax = Mathf.NegativeInfinity;

    private WaveSpawner waveSpawn;

    public bool isGenerateNew;

    public int gridSize;
    private bool isConnectedPath;

    public static List<Transform> objectLocations;

    // Start is called before the first frame update
    void Start()
    {
        waypointGen = FindObjectOfType<Waypoints>();
        objectLocations = new List<Transform>();
        waveSpawn = gameObject.GetComponent<WaveSpawner>();
        waypointsObj = waypointGen.gameObject;



        if(!isGenerateNew)
        {
            startObj = GameObject.FindGameObjectWithTag("Start");
            waveSpawn.spawnPoint = startObj.transform;

            for(int i = 0; i < waypointsObj.transform.childCount; i++)
            {
                objectLocations.Add(waypointsObj.transform.GetChild(i));
            }

            return;
        }

        StartPlacement();
        while(waypointCount > 0)
        {
            GenerateSpawnPos();
            while (!CheckIfEmptySpot(spawnPos.position))
            {
                GenerateSpawnPos();
            }
            WaypointPlacement();
        }
        GenerateSpawnPos();
        while (!CheckIfEmptySpot(spawnPos.position))
        {
            GenerateSpawnPos();
        }
        EndPlacement();

        while (nodeCount > 0)
            NodePlacement();

        GetWorldBounds();
        SetCameraBounds();
    }

    void GenerateSpawnPos()
    {
        int dir;

        dir = Random.Range(0, 10);

        Transform corner;
        Waypoint w;

        if (dir <= 1)
        {
            corner = objectLocations[objectLocations.Count - 1];
            w = corner.GetComponent<Waypoint>();
            if (dir == 0)
            {
                spawnPos.Rotate(0, 0, -90);
                if (w != null)
                    w.SetCorner(true);
            }
            if (dir == 1)
            {
                spawnPos.Rotate(0, 0, 90);
                if (w != null)
                    w.SetCorner(false);
            }
        }

        Vector3 checkPos = spawnPos.position + (spawnPos.up * gridSize);


        spawnPos.position = checkPos;
    }

    void StartPlacement()
    {
        spawnPos.position = Vector3.zero;
        startObj = Instantiate(start, spawnPos.position, Quaternion.identity);
        objectLocations.Add(startObj.transform);
        waveSpawn.spawnPoint = startObj.transform;
    }

    void EndPlacement()
    {
        GameObject endObj = Instantiate(end, spawnPos.position, Quaternion.identity);
        objectLocations.Add(endObj.transform);
    }

    void WaypointPlacement()
    {
        waypointGen.SpawnWaypoint(spawnPos);
        waypointCount--;
    }

    void NodePlacement()
    {
        int i = Random.Range(1, objectLocations.Count - 1);
        Transform nodeTran = objectLocations[i];
        Vector3 nodePos;
        int right = 1;
        bool side = (Random.value > 0.5f);
        if (side)
            right = 1;
        else
            right = -1;

        nodePos = nodeTran.position + (nodeTran.right * gridSize * right);

        if(!CheckIfEmptySpot(nodePos))
        {
            NodePlacement();
            return;
        }

        nodeGen.SpawnNode(nodePos);
        nodeCount--;
    }

    bool CheckIfEmptySpot(Vector3 posToCheck)
    {
        for (int x = 0; x < objectLocations.Count; x++)
        {
            if (posToCheck == objectLocations[x].position)
            {
                Debug.Log("Space occupied");
                return false;
            }
        }

        return true;
    }

    void GetWorldBounds()
    {
        for (int x = 0; x < objectLocations.Count; x++)
        {
            if (objectLocations[x].position.x < xMin)
                xMin = objectLocations[x].position.x;
            if (objectLocations[x].position.x > xMax)
                xMax = objectLocations[x].position.x;
            if (objectLocations[x].position.y < yMin)
                yMin = objectLocations[x].position.y;
            if (objectLocations[x].position.y > yMax)
                yMax = objectLocations[x].position.y;
        }
    }

    void SetCameraBounds()
    {
        CameraController.xMin = xMin;
        CameraController.xMax = xMax;
        CameraController.yMin = yMin;
        CameraController.yMax = yMax;
    }

}
