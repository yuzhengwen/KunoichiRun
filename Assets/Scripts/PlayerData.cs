using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 10;
    private float hp = 5;
    private int coins = 0;

    // gameobject, current value, change type (1 for positive, -1 for negative)
    public event Action<GameObject, int, int> onCoinChange;
    public event Action<GameObject, float, int> onHealthChange;
    public event Action onDeath;

    [SerializeField]
    private float damageInterval = 1.0f;
    private float nextDamageTime = 0;

    public void addCoins(int amount)
    {
        coins += amount;
        onCoinChange?.Invoke(gameObject, coins, 1);
    }
    public void deductCoins(int amount)
    {
        coins = coins -= amount < 0 ? 0 : coins -= amount;
        onCoinChange?.Invoke(gameObject, coins, -1);
    }
    public void addHealth(float amount)
    {
        if (hp + amount > maxHp)
            hp = maxHp;
        else
            hp += amount;
        onHealthChange?.Invoke(gameObject, hp, 1);
    }

    // only deduct health once per second
    public void deductHealth(float amount)
    {
        Debug.Log(Time.time);
        if (Time.time >= nextDamageTime)
        {
            if (hp - amount <= 0)
            {
                hp = 0;
                onHealthChange?.Invoke(gameObject, hp, -1);
                onDeath?.Invoke();
            }
            else
            {
                hp -= amount;
                onHealthChange?.Invoke(gameObject, hp, -1);
                nextDamageTime = Time.time + damageInterval;
            }
        }
        }
    public float getHealth()
    {
        return hp;
    }
    public float getMaxHealth()
    {
        return maxHp;
    }
}
