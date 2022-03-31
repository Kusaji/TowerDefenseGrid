using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Spawnpoints;
    //public List<GameObject> enemyPrefabs;

    public bool logDebugs;

    [Header("Wave Spawnrate Settings")]
    public float spawnRate;
    public float waveEndDelay;

    [Header("Don't Change This")]
    public int wave;

    [Header("Design Levels waves here")]
    public List<WaveSO> levelWaves;


    //public List<int> enemyWaves;
    //public int enemiesLeftInWave;
    //public bool noEnemiesLeft;
    // Start is called before the first frame update
    void Start()
    {
        /*        noEnemiesLeft = false;
                enemiesLeftInWave = enemyWaves[0];*/
        wave = 0;
        spawnRate = 1 / spawnRate;

        StartCoroutine(SpawnRoutine(levelWaves[wave]));
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator SpawnRoutine(WaveSO currentWave)
    {
        var enemiesToSpawn = currentWave.enemiesToSpawn;
        var enemiesSpawned = 0;

        while (enemiesSpawned < enemiesToSpawn)
        {
            if (logDebugs)
            {
                Debug.Log($"Enemies Remaining {enemiesToSpawn - enemiesSpawned}");
            }

            if (currentWave.randomEnemySelections == true)
            {
                Instantiate(
                    currentWave.enemyPrefabs[Random.Range(0, currentWave.enemyPrefabs.Count)],
                    Spawnpoints[0].transform.position,
                    Quaternion.identity,
                    GameObject.Find("Enemies").transform);
                enemiesSpawned++;
                yield return new WaitForSeconds(spawnRate);
            }
            else
            {
                Instantiate(
                    currentWave.enemyPrefabs[0],
                    Spawnpoints[0].transform.position,
                    Quaternion.identity,
                    GameObject.Find("Enemies").transform);
                enemiesSpawned++;
                yield return new WaitForSeconds(spawnRate);

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

    //Scrapped Code I'm emotionally attached to.
    /*    IEnumerator SpawnRoutine()
        {
            while (noEnemiesLeft == false)
            {
                if (enemiesLeftInWave > 0)
                {
                    Instantiate(enemyPrefabs[0], Spawnpoints[0].transform.position, Quaternion.identity, GameObject.Find("Enemies").transform);
                    enemiesLeftInWave--;
                    yield return new WaitForSeconds(spawnRate);
                }
                else if (currentWave < enemyWaves.Count - 1)
                {
                    currentWave++;
                    enemiesLeftInWave = enemyWaves[currentWave];
                    yield return new WaitForSeconds(waveEndDelay);
                }
                else
                {
                    Debug.Log("All waves spawned.");
                    noEnemiesLeft = true;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }*/
}
