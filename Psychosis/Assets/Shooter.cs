using UnityEngine;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Префаб снаряда
    public Transform firePoint;         // Точка, из которой выпускаются снаряды
    public float shootInterval = 1f;    // Интервал между выстрелами
    public float searchRadius = 20f;    // Радиус поиска врагов

    private float nextShootTime = 0f;
    public WaveManager waveManager; 
    private ObjectPool projectilePool;

    public int damage;

    void Start()
    {
        projectilePool = new ObjectPool(projectilePrefab, 10);
    }

    void Update()
    {
        // Проверяем, нужно ли стрелять
        if (Time.time >= nextShootTime)
        {
            ShootAtNearestEnemy();
            nextShootTime = Time.time + shootInterval;
        }
    }

    void ShootAtNearestEnemy()
    {
        GameObject nearestEnemy = GetNearestEnemy();
        if (nearestEnemy != null)
        {
            GameObject projectile = projectilePool.GetObject();
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.SetActive(true);

            projectile.GetComponent<Projectile>().Launch(nearestEnemy.transform, damage);
        }
    }

    GameObject GetNearestEnemy()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in waveManager.activeEnemies)
        {
            if (enemy == null) continue;
            if (enemy.GetComponent<Enemy>().currentHealth <= 0) continue;
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance && distance <= searchRadius)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
