using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public int coinValue = 1;
    private PlayerData playerData;

    void ICollectible.onPlayerCollect(GameObject player)
    {
        if (playerData == null) playerData = player.GetComponent<PlayerData>();
        playerData.addCoins(coinValue);
        Destroy(this.gameObject);
    }
}
