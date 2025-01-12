using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<WaveData> Waves;
    //public Transform[] SpawnPoints;
    public EnemySpawner Spawner;

    private int currentWaveIndex = 0;

    public List<GameObject> activeEnemies = new List<GameObject>();
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    public void Start()
    {
        //StartWave();
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }
    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    public bool AreAllEnemiesDefeated()
    {
        return activeEnemies.Count == 0;
    }

    private IEnumerator SpawnWave()
    {
        if (currentWaveIndex >= Waves.Count)
        {
            Debug.Log("All waves gone");
            yield break;
        }

        WaveData currentWave = Waves[currentWaveIndex];
        //Debug.Log($"Началась волна: {currentWave.WaveName}");

        foreach (var enemyInfo in currentWave.Enemies)
        {
            for (int i = 0; i < enemyInfo.Count; i++)
            {
                EnemyStats enemy = enemyInfo.EnemyStats;
                Spawner.SpawnEnemy(enemy);
                Enemy enemyComponent = enemy.Prefab.GetComponent<Enemy>();
                enemyComponent.OnEnemyDeath += HandleEnemyDeath;
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }
        }

        currentWaveIndex++;
        //yield return new WaitForSeconds(5f);
        //StartCoroutine(SpawnWave());
    }

    public void RegisterEnemy(GameObject enemy)
    {
        activeEnemies.Add(enemy);
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        if (enemyComponent != null)
        {
            enemyComponent.OnEnemyDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            Debug.Log("Removed enemy: " + enemy.name);
        }
        else
        {
            Debug.LogWarning("Enemy not found in activeEnemies: " + enemy.name);
        }

        if (AreAllEnemiesDefeated())
        {
            gameManager.OnWaveCompleted();
        }
    }
}
