using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Spawnpoints;

    public bool logDebugs;
    public bool allWavesSpawned;

    [Header("Wave Spawnrate Settings")]
    public float spawnDelay;
    public float waveEndDelay;

    [Header("Don't Change This")]
    public int wave;

    [Header("Design Levels waves here")]
    public List<WaveSO> levelWaves;

    public float healthMultiplier;
    private Text waveText;
    public bool beatLevel;

    public ActiveEnemeisList enemyList;

    private Economy playerEconomy;
    private AudioSource speaker;
    public AudioClip spawnSound;

    void Start()
    {
        //Find necessary objects
        enemyList = GameObject.Find("ActiveEnemies").GetComponent<ActiveEnemeisList>();
        playerEconomy = GameObject.Find("Economy").GetComponent<Economy>();
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        speaker = GetComponent<AudioSource>();
        
        //Set variables
        wave = 0;
        beatLevel = false;
        allWavesSpawned = false;

        //Start Coroutines
        StartCoroutine(DelayFirstWave());
    }

    IEnumerator DelayFirstWave()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnRoutine(levelWaves[wave]));
    }

    IEnumerator CheckIfLevelBeat()
    {
        while (gameObject && beatLevel == false)
        {
            if (allWavesSpawned)
            {
                if (enemyList.activeEnemies.Count == 0 && beatLevel == false)
                {
                    beatLevel = true;
                    GameObject.Find("UI").GetComponent<LevelController>().LevelBeat();
                    yield break;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void PlaySpawnSound()
    {
        speaker.pitch = Random.Range(0.90f, 1.10f);
        speaker.PlayOneShot(spawnSound, Random.Range(0.25f, 0.35f));
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

                enemyList.activeEnemies.Add(newEnemy);
                newEnemy.GetComponent<EnemyNavigation>().enemySpawner = this;
                newEnemy.GetComponent<EnemyNavigation>().playerEconomy = playerEconomy;
                newEnemy.GetComponent<EnemyHealth>().playerEconomy = playerEconomy;


                PlaySpawnSound();

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

                enemyList.activeEnemies.Add(newEnemy);
                newEnemy.GetComponent<EnemyNavigation>().enemySpawner = this;
                newEnemy.GetComponent<EnemyNavigation>().playerEconomy = playerEconomy;
                newEnemy.GetComponent<EnemyHealth>().playerEconomy = playerEconomy;

                PlaySpawnSound();

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
            allWavesSpawned = true;
            StartCoroutine(CheckIfLevelBeat());

            if (logDebugs)
            {
                Debug.Log("All waves spawned");
            }

            yield break;
        }
    }
}

