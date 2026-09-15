using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public InputSystem_Actions controls { get; private set; }

    public PlayerHealth playerHealth { get; private set; }
    public PlayerController playerController { get; private set; }

    public PlayerRespawn playerRespawn { get; private set; }

    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        controls = new InputSystem_Actions();
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
        playerRespawn = GetComponent<PlayerRespawn>();

    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
