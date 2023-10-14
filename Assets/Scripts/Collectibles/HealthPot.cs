using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : MonoBehaviour, ICollectible
{
    public float addHP = 1;
    private PlayerData playerData;
    void ICollectible.onPlayerCollect(GameObject player)
    {
        if (playerData == null) playerData = player.GetComponent<PlayerData>();
        playerData.addHealth(addHP);
        Destroy(this.gameObject);
    }
}
