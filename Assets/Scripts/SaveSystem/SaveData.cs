using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int PlayerHealth = Player.Instance.playerHealth.MaxHealth;
    public int coins;
    public string checkpointID;
    public List<string> collectedCoinIDs = new();
}