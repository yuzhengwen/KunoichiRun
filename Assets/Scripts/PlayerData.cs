using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData: MonoBehaviour
{
    public float hp = 10;
    public int coins = 0;

    public void addHealth(float addHP)
    {
        if (hp + addHP > 10)
            hp = 10;
        else
            hp += addHP; 
    }
}
