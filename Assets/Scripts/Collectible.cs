using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void onPlayerCollect(GameObject player);

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Debug.Log(collision.tag+ " collided");
        if (collision.CompareTag("Player"))
        {
            onPlayerCollect(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
