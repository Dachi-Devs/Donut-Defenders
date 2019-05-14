using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 500;

    public static int Lives;
    public int startLives;

    public static int roundCount;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;

        roundCount = 0;
    }
}
