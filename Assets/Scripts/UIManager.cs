using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private PlayerData playerData;
    private PlayerMovement playerMovement;

    [SerializeField]
    private TextMeshProUGUI coinCounter;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Image healthFill;
    [SerializeField]
    private Gradient healthGradient;

    // Start is called before the first frame update
    void Start()
    {
        // initialize components
        playerData = player.GetComponent<PlayerData>();
        playerMovement = player.GetComponent<PlayerMovement>();

        // subscribe events
        playerData.onCoinChange += updateCoin;
        playerData.onHealthChange += updateHealth;

        healthSlider.maxValue = playerData.getMaxHealth();
        updateHealth(null, playerData.getHealth(), 0);
    }

    private void updateHealth(GameObject player, float hp, int changeType)
    {
        healthSlider.value = hp;
        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }

    private void updateCoin(GameObject player, int coins, int changeType)
    {
        coinCounter.SetText(coins.ToString());
    }
    private void OnDestroy()
    {
        playerData.onCoinChange -= updateCoin;
        playerData.onHealthChange -= updateHealth;  
    }
}
