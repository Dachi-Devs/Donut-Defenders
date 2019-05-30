using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public static int enemyCount;

    public Wave[] waves;
    public bool stackWaves = false;
    public Transform spawnPoint;

    public float waveTimer = 5f, spawnDelay = 0.5f;

    public float countdown = 2f;

    public Text timerText;

    public GameManager gameManager;

    private int waveIndex = 0;

    void Start()
    {
        enemyCount = 0;
    }

    void Update()
    {
        if(!stackWaves)
        {
            if(enemyCount > 0)
            {
                return;
            }
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = waveTimer;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        timerText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.roundCount++;

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            int x = Random.Range(0, wave.enemyInfantry.Length - 1);
            SpawnEnemy(wave.enemyInfantry[x]);
            yield return new WaitForSeconds(spawnDelay);
        }
        waveIndex++;

        //End Level
        if(waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        enemyCount++;
    }
}
