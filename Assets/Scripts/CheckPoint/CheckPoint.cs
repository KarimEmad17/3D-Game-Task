using UnityEngine;

public class Checkpoint : MonoBehaviour, ICheckpoint
{
    [SerializeField] private string checkpointID;

    public string CheckpointID => checkpointID;

    private bool isActivated;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Activate();
    }

    public void Activate()
    {
        if (isActivated)
            return;

        isActivated = true;

        CheckPointManager.Instance.SetCheckpoint(this);

        Debug.Log($"Checkpoint activated: {checkpointID}");
    }
}