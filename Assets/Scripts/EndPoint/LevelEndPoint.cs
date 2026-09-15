using UnityEngine;

public class LevelEndPoint : MonoBehaviour
{
    private bool levelEnded;
   
    private void OnTriggerEnter(Collider other)
    {
        if (levelEnded)
            return;

        if (!other.CompareTag("Player"))
            return;

        levelEnded = true;

        GameManager.Instance.CompleteLevel();
    }
}