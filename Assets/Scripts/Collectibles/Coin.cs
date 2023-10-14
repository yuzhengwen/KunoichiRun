using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public int coinValue = 1;

    void ICollectible.onPlayerCollect(GameObject player)
    {
        PlayerData data = player.GetComponent<PlayerData>();
        data.addCoins(coinValue);
        Destroy(this.gameObject);
    }
}
