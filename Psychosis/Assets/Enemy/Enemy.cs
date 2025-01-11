using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public string EnemyName { get; private set; }
    public int Health { get; private set; }
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

        if (isStatic == 0)
            agent.SetDestination(player.position);
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                agent.SetDestination(player.position);

            }
        }
    }
}