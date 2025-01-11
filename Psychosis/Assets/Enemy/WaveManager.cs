using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<WaveData> Waves;
    //public Transform[] SpawnPoints;
    public EnemySpawner Spawner;

    private int currentWaveIndex = 0;

    public void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
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
                Spawner.SpawnEnemy(enemyInfo.EnemyStats);
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }
        }

        currentWaveIndex++;
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnWave());
    }
}
