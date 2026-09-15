using System;
using UnityEngine;

public class Coin : MonoBehaviour, ISaveable
{
    [SerializeField] private int value = 1;
    [SerializeField] private string coinID;

    private bool isCollected;

    private void Awake()
    {
        if(SaveManager.Instance != null)
        {
            SaveManager.Instance.Register(this);
        }
    }

    private void OnDestroy()
    {
        SaveManager.Instance.Unregister(this);
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward , 50f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected)
            return;

        if (!other.CompareTag("Player"))
            return;

        Collect();
    }

    private void Collect()
    {
        isCollected = true;

        CoinManager.Instance.AddCoin(value);

        gameObject.SetActive(false);

    }

    public void Save(SaveData data)
    {
        if (!data.collectedCoinIDs.Contains(coinID) && isCollected)
        {
            data.collectedCoinIDs.Add(coinID);
        }
    }

    public void Load(SaveData data)
    {
        if (data.collectedCoinIDs.Contains(coinID))
        {
            isCollected = true;
            gameObject.SetActive(false);
        }
    }
}