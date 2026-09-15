using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour, ISaveable
{
    public static CheckPointManager Instance { get; private set; }

    [SerializeField] private List<Checkpoint> checkpoints;

    private Checkpoint currentCheckpoint;

    public Checkpoint CurrentCheckpoint => currentCheckpoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        SaveManager.Instance.Register(this);
    }

    private void OnDestroy()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.Unregister(this);
        }
    }

    public void RegisterCheckpoint(Checkpoint checkpoint)
    {
        if (!checkpoints.Contains(checkpoint))
        {
            checkpoints.Add(checkpoint);
        }
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint;

        Debug.Log($"Current checkpoint: {checkpoint.CheckpointID}");
    }

    public void Save(SaveData data)
    {
        if (currentCheckpoint == null)
        {
            data.checkpointID = string.Empty;
            return;
        }

        data.checkpointID = currentCheckpoint.CheckpointID;
    }

    public void Load(SaveData data)
    {
        if (string.IsNullOrEmpty(data.checkpointID))
        {
            currentCheckpoint = null;
            return;
        }

        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (checkpoint.CheckpointID == data.checkpointID)
            {
                currentCheckpoint = checkpoint;
                Debug.Log(currentCheckpoint.CheckpointID);
                Player.Instance.playerRespawn.RespawnPlayer();
                break;
            }
        }
    }
}