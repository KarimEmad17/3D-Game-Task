using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour , ISaveable
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 3;

    [SerializeField]private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    public event EventHandler<float> OnHealthChanged;
    public event EventHandler OnPlayerDied;
    //Health 


    private void Awake()
    {
        currentHealth = maxHealth;

        if (SaveManager.Instance!= null)
        {
            SaveManager.Instance.Register(this);
        }
    }
    private void Start()
    {
       
    }


    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        currentHealth -= damage;
        OnHealthChanged?.Invoke(this, (float)currentHealth / maxHealth);
        Debug.Log($"Player took {damage} damage. HP: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnPlayerDied?.Invoke(this, EventArgs.Empty);
        Player.Instance.playerRespawn.RespawnPlayer();
        Reset();
        Debug.Log("Player Died");

        // TODO:
        // Disable player
        // Play death animation
        // Show Game Over
        // Restart level
    }
    

    public void Save(SaveData data)
    {
        data.PlayerHealth = currentHealth;
    }

    public void Load(SaveData data)
    {
        currentHealth = data.PlayerHealth;
        OnHealthChanged?.Invoke(this, (float)currentHealth / maxHealth);
        Debug.Log($"Player health loaded: {currentHealth}/{maxHealth}");
    }

    private void Reset()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(this, (float)currentHealth / maxHealth);
        Debug.Log($"Player health reset: {currentHealth}/{maxHealth}");
    }
}