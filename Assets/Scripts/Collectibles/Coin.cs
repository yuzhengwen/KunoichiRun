using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public int coinValue = 1;

    void ICollectible.onPlayerCollect(GameObject player)
    {
        PlayerData data = player.GetComponent<PlayerData>();
        data.coins += coinValue;
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
