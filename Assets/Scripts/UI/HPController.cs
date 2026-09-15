using System;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    [SerializeField] private Image HpBar;

    

    
    private void Start()
    {
        Player.Instance.playerHealth.OnHealthChanged += UpdateHealthUI;
    }
    private void UpdateHealthUI(object sender, float e)
    {
        Debug.Log(e);
        HpBar.fillAmount = e;
    }

    
}
