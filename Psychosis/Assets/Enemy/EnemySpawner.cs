using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public WaveData CurrentWave;

    public Transform player;
    public float spawnRadius = 10f;

    public List<GameObject> enemies;

    void Start()
    {
        enemies = new List<GameObject>();
    }

    public void SpawnEnemy(EnemyStats stats)
    {
        Vector3 spawnPosition = GetRandomPointAroundPlayer();

        if (NavMesh.SamplePosition(spawnPosition, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            GameObject newEnemy = Instantiate(stats.Prefab, hit.position, Quaternion.identity);
            newEnemy.transform.parent = transform;
            newEnemy.GetComponent<Enemy>().Initialize(stats);
            newEnemy.GetComponent<Enemy>().isStatic = Random.Range(0,2);

            Enemy enemyAI = newEnemy.GetComponent<Enemy>();
            if (enemyAI != null)
            {
                enemyAI.player = player;
            }
            enemies.Add(newEnemy);
        }
    }

    Vector3 GetRandomPointAroundPlayer()
    {
        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(25f, spawnRadius);

        Vector3 spawnOffset = new Vector3(Mathf.Cos(angle) * distance, 0, Mathf.Sin(angle) * distance);
        return player.position + spawnOffset;
    }
}