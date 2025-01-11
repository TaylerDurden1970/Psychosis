using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3.5f;

    private NavMeshAgent agent;

    private Animator animator;

    public float attackRange = 10f;

    public int isStatic = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        speed = Random.Range(0.5f, 1);

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent не найден на враге!");
        }

        agent.speed = speed * 3.5f;
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Игрок не задан!");
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
