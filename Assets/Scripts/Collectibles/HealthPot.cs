using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : MonoBehaviour, ICollectible
{
    public float addHP = 1;
    void ICollectible.onPlayerCollect(GameObject player)
    {
        PlayerData data = player.GetComponent<PlayerData>();
        data.addHealth(addHP);
        Destroy(this.gameObject);
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
