using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;
    private int index = 0;
    public float velocity = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToWaypoint = Vector2.Distance(waypoints[index].transform.position, transform.position);
        Vector2 direction = (waypoints[index].transform.position - transform.position).normalized;
        if (distanceToWaypoint > 0.1)
        {
            transform.Translate(direction * velocity * Time.deltaTime);
        }
        else
        {
            if (!(index + 1 >= waypoints.Length))
                index++; 
            else 
                index = 0;
        }
    }
}
