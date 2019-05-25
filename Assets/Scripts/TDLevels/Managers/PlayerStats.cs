using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int energy;
    public int startEnergy = 5;

    public static int Lives;
    public int startLives;

    public static int roundCount;

    void Start()
    {
        energy = startEnergy;
        Lives = startLives;

        roundCount = 0;
    }
}
