using UnityEngine;

public class EnemyHeadHit : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        enemy.Die();
    }
}