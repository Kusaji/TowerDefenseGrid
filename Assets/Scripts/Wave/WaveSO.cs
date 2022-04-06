using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/EnemyWave", order = 1)]
public class WaveSO : ScriptableObject
{
    public bool randomEnemySelections;
    public List<GameObject> enemyPrefabs;
    public int enemiesToSpawn;

    public float spawnDelay;
    public float waveEndDelay;
}
