using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifetime = 2f;
    private Transform target;
    private float spawnTime;

    public void Launch(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
        spawnTime = Time.time;
    }

    void Update()
    {
        if (target == null)
        {
            DestroyProjectile();
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            DestroyProjectile();
        }

        if (Time.time - spawnTime > lifetime)
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}
