using System;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public event EventHandler<Checkpoint> OnPlayerRespawned;
    void OnEnable()
    {
        OnPlayerRespawned += HandlePlayerRespawned;
    }

    private void HandlePlayerRespawned(object sender, Checkpoint e)
    {
        Respawn(e);
    }

    private void Respawn(Checkpoint checkpoint)
    {
         
            

        Vector3 respawnPosition = checkpoint != null
            ? checkpoint.transform.position
            : Vector3.zero;

        

        Player.Instance.playerController.Teleport(respawnPosition);
    }


    public void RespawnPlayer()
    {
        OnPlayerRespawned.Invoke(this, CheckPointManager.Instance.CurrentCheckpoint);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
