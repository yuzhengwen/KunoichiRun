using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour, IObstacle
{

    private PlayerData playerData;

    public void onDamagePlayer(GameObject player, Vector2 relVel)
    {
        if (playerData == null)
            playerData = player.GetComponent<PlayerData>();
        playerData.deductHealth(1);
        Debug.Log("Damage");
    }
}
