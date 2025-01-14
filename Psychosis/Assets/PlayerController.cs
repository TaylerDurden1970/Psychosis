using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;

    public Animator animator;

    public int maxHealth;
    public int currentHealth;
    public int maxXP = 10;
    public int currentXP = 0;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent не найден на объекте!");
        }

        currentHealth = maxHealth;

        UIController.instance.OverrideHealthFiller(currentHealth,maxHealth);
        XPChange();
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

    public void XPChange()
    {
        currentXP = UIController.instance.OverrideXPFiller(currentXP, maxXP);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UIController.instance.OverrideHealthFiller(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("XPItem"))
        {
            other.gameObject.SetActive(false);
            XPChange();
        }
    }

    private void Die()
    {
        Debug.Log("GG");
        Animator animator = GetComponent<Animator>();
        //animator.Play("SkeletonDeath");
    }
}
