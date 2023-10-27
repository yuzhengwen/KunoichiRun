using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 10;
    private float hp = 10;
    private int coins = 0;

    // gameobject, current value, change type (1 for positive, -1 for negative)
    public event Action<GameObject, int, int> onCoinChange;
    public event Action<GameObject, float, int> onHealthChange;
    public Action onDeath;

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
        if (Time.time >= nextDamageTime)
        {
            if (hp - amount <= 0)
            {
                hp = 0;
                onHealthChange?.Invoke(gameObject, hp, -1);
                onDeath?.Invoke();
                AudioManager.Instance.playAudio(AudioManager.AudioType.Death);
            }
            else
            {
                hp -= amount;
                onHealthChange?.Invoke(gameObject, hp, -1);
                nextDamageTime = Time.time + damageInterval;
                AudioManager.Instance.playAudio(AudioManager.AudioType.Damage);
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
