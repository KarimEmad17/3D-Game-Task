using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour , IDamageable
{
    [Header("Patrol")]
    [SerializeField] protected Transform pointA;
    [SerializeField] protected Transform pointB;

    protected NavMeshAgent agent;

    
    protected Transform currentPatrolPoint;
    protected bool canDoDamage = true;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        currentPatrolPoint = pointA;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Patrol();
    }

    protected void Patrol()
    {
        agent.SetDestination(currentPatrolPoint.position);

        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            ChangePatrolPoint();
        }
    }

    private void ChangePatrolPoint()
    {
        currentPatrolPoint =
            currentPatrolPoint == pointA ? pointB : pointA;
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    public void DoDamage(int damage)
    {
        
        Player.Instance.playerHealth.TakeDamage(damage);
    }

    public bool CanDoDamage()
    {
        return canDoDamage;
    }
    public void ToggleCanDoDamage()
    {
        canDoDamage = !canDoDamage;
    }
}
