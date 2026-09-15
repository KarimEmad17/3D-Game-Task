using System;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    private void Start()
    {
        Player.Instance.playerHealth.OnPlayerDied += HandlePlayerDied;
    }

    private void HandlePlayerDied(object sender, EventArgs e)
    {
        enemyAI.StopChasing();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (!other.CompareTag("Player"))
            return;

        enemyAI.StartChasing();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        enemyAI.StopChasing();
    }
}