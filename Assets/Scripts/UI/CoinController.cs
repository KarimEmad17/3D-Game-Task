using TMPro;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CoinText;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        CoinManager.Instance.OnCoinsChanged += (sender, coins) =>
        {
            UpdateCoinUI(coins);
            Debug.Log($"Coins: {coins}");
        };
    }
    // Update is called once per frame
    void UpdateCoinUI(int coins)
    {
        CoinText.text =$"Coins : {coins}";
    }
}
