using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public event Action<GameObject> OnEnemyDeath;

    public Transform player;

    public GameObject itemDrop;
    public string EnemyName { get; private set; }
    public int Health { get; private set; }

    public int currentHealth;
    public int Damage { get; private set; }
    public float Speed { get; private set; }

    private NavMeshAgent agent;

    private Animator animator;

    public float attackRange = 10f;

    public int isStatic = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(EnemyStats stats)
    {
        EnemyName = stats.Name;
        Health = stats.Health;
        currentHealth = Health;
        Damage = stats.Damage;
        Speed = stats.Speed;
        
        Debug.Log($"Enemy {EnemyName} initialized with {Health} HP, {Damage} damage, {Speed} speed.");
    }


    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("No Player");
            return;
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isStatic == 0)
            agent.SetDestination(player.position);
        else
        {

            if (distanceToPlayer <= attackRange)
            {
                agent.SetDestination(player.position);

            }
        }

        if (distanceToPlayer <= 1f)
        {
            Debug.Log("Shas Yeby");
            animator.SetFloat("Atack", 1);
        }
        else
        {
            animator.SetFloat("Atack", 0);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        Animator animator = GetComponent<Animator>();
        Instantiate(itemDrop, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), Quaternion.identity);
        animator.Play("SkeletonDeath");
        GetComponent<Enemy>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        OnEnemyDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }

    public void DealDamage()
    {
        player.GetComponent<PlayerController>().TakeDamage(Damage);
    }
}
