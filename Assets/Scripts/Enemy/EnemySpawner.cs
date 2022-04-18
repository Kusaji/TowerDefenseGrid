using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Spawnpoints;

    public bool logDebugs;

    [Header("Wave Spawnrate Settings")]
    public float spawnDelay;
    public float waveEndDelay;

    [Header("Don't Change This")]
    public int wave;

    [Header("Design Levels waves here")]
    public List<WaveSO> levelWaves;

    public float healthMultiplier;
    private Text waveText;

    void Start()
    {
        wave = 0;
        StartCoroutine(DelayFirstWave());
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
    }

    IEnumerator DelayFirstWave()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnRoutine(levelWaves[wave]));
    }

    IEnumerator SpawnRoutine(WaveSO currentWave)
    {
        var enemiesToSpawn = currentWave.enemiesToSpawn;
        var enemiesSpawned = 0;

        waveText.text = $"Wave {wave + 1} | {levelWaves.Count}";

        spawnDelay = currentWave.spawnDelay;
        waveEndDelay = currentWave.waveEndDelay;
        healthMultiplier = currentWave.healthMultiplier;

        while (enemiesSpawned < enemiesToSpawn)
        {
            if (logDebugs)
            {
                Debug.Log($"Enemies Remaining {enemiesToSpawn - enemiesSpawned}");
            }

            if (currentWave.randomEnemySelections == true)
            {
                var newEnemy = Instantiate(
                       currentWave.enemyPrefabs[Random.Range(0, currentWave.enemyPrefabs.Count)],
                       Spawnpoints[0].transform.position,
                       Quaternion.identity,
                       GameObject.Find("Enemies").transform);
                enemiesSpawned++;

                newEnemy.GetComponent<EnemyNavigation>().enemySpawner = this;

                yield return new WaitForSeconds(spawnDelay);
            }
            else
            {
                var newEnemy = Instantiate(
                         currentWave.enemyPrefabs[0],
                         Spawnpoints[0].transform.position,
                         Quaternion.identity,
                         GameObject.Find("Enemies").transform);
                enemiesSpawned++;

                newEnemy.GetComponent<EnemyNavigation>().enemySpawner = this;

                yield return new WaitForSeconds(spawnDelay);

            }
            yield return new WaitForSeconds(0.1f);
        }

        if (logDebugs)
        {
            Debug.Log("Wave Ended");
        }

        wave++;
        yield return new WaitForSeconds(waveEndDelay);

        if (wave < levelWaves.Count)
        {
            if (logDebugs)
            {
                Debug.Log($"Starting {levelWaves[wave].name}");
            }

            StartCoroutine(SpawnRoutine(levelWaves[wave]));
        }
        else
        {
            if (logDebugs)
            {
                Debug.Log("All waves spawned");
            }
        }
    }
}
