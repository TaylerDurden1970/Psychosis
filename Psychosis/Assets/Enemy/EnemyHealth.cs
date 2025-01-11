using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public delegate void DeathHandler();
    public event DeathHandler OnDeath;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Recieved");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Debug.Log("Death!");
        Animator animator = GetComponent<Animator>();
        animator.Play("SkeletonDeath");
        GetComponent<Enemy>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        //Destroy(gameObject);
    }
}