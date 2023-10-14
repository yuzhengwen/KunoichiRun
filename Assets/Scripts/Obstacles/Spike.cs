using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour, IObstacle
{
    public float damageInterval { get; set; } = 1.0f;
    private float nextDamageTime = 0;

    private PlayerData playerData;

    public void onDamagePlayer(GameObject player)
    {
        Debug.Log("Hit spikes");
        if (playerData == null) 
            playerData = player.GetComponent<PlayerData>();
        if (Time.time >= nextDamageTime)
        {
            playerData.deductHealth(1);
            nextDamageTime = Time.time + damageInterval;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
