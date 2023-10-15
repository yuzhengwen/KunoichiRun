using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacle 
{
    void onDamagePlayer(GameObject player, Vector2 relativeVelocity);
}
