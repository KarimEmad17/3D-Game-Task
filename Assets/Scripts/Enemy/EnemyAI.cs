using UnityEngine;
using UnityEngine.AI;
public enum EnemyState
{
    Patrol,
    Chase
}
public class EnemyAI : Enemy , IChasable
{
    //[Header("Chase")]
    /*[SerializeField]*/ private Transform player;

    [SerializeField] private float collisionTime = 2f;

    private EnemyState currentState;
    

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.Patrol;
        player = Player.Instance.transform;

    }

    protected override void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                Chase();
                break;
        }
    }

    public void Chase()
    {
        if (player == null)
            return;

        agent.SetDestination(player.position);
    }

    public void StartChasing()
    {
        currentState = EnemyState.Chase;
    }

    public void StopChasing()
    {
        currentState = EnemyState.Patrol;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CanDoDamage())
            {
                DoDamage(1);
                
                Debug.Log("Enemy collided with player and dealt damage.");
                
                ToggleCanDoDamage();
                Invoke(nameof(ToggleCanDoDamage), collisionTime);
            }
        }
    }
    
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        DoDamage(1);
    //        Debug.Log("Enemy is colliding with player and dealt damage.");
    //    }
    //}

}