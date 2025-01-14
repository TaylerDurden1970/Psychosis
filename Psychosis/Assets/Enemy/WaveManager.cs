using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<WaveData> Waves;
    //public Transform[] SpawnPoints;
    public EnemySpawner Spawner;

    public GameObject xpItemPrefab;

    private ObjectPool xpItemsPool;

    private int currentWaveIndex = 0;

    public List<GameObject> activeEnemies = new List<GameObject>();
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    public void Start()
    {
        int enemiesCount = 0;
        foreach (var wave in Waves)
        {
            enemiesCount += wave.Enemies.Count;
        }

        xpItemsPool = new ObjectPool(xpItemPrefab, enemiesCount);
        Debug.LogWarning(enemiesCount);
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
            for (int i = 1; i < enemyInfo.Count; i++)
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
            spawnXPItem(enemy);
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

    private void spawnXPItem(GameObject enemy)
    {
        GameObject xpItem = xpItemsPool.GetObject(); 
        xpItem.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1, enemy.transform.position.z);
        xpItem.transform.rotation = enemy.transform.rotation;
        xpItem.SetActive(true);
    }
}
