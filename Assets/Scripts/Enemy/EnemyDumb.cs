using UnityEngine;

public class EnemyDumb : Enemy
{
    [SerializeField] private float collisionTime = 2f;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
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
}
