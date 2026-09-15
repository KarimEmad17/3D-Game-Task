using System;
using UnityEngine;

public class CoinManager : MonoBehaviour , ISaveable
{
    public static CoinManager Instance { get; private set; }

    public int Coins { get; private set; }

    public event EventHandler<int> OnCoinsChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        if (SaveManager.Instance!= null)
        {
            SaveManager.Instance.Register(this);
        }

    }
   
    private void OnDestroy()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.Unregister(this);
        }
    }

    public void AddCoin(int amount)
    {
        Coins += amount;
        OnCoinsChanged?.Invoke(this, Coins);
        Debug.Log($"Coins: {Coins}");
    }

    public void Save(SaveData data)
    {
        data.coins = Coins;
    }

    public void Load(SaveData data)
    {
        Coins = data.coins;
        OnCoinsChanged?.Invoke(this, Coins);
        Debug.Log($"Coins loaded: {Coins}");
    }

}
