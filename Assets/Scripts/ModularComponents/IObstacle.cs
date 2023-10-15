using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacle 
{
    float damageInterval {  get; set; }
    void onDamagePlayer(GameObject player, Vector2 relativeVelocity);
}
