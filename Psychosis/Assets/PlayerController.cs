using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;

    public Animator animator;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent не найден на объекте!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        float speed = agent.velocity.magnitude;

        animator.SetFloat("Speed",speed);
        animator.SetBool("isMoving", speed > 0.1f);
    }
}
