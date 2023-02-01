using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    public Vector2[] waypoints;
    int currentWaypoint = 0;
    public int speed = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPoint = waypoints[currentWaypoint];
        if (Vector2.Distance(this.transform.position, currentPoint) < 2)
            currentWaypoint++;
        
        if (currentWaypoint >= waypoints.Length)
            currentWaypoint = 0;

        this.transform.LookAt(waypoints[currentWaypoint]);
        this.transform.Translate(0, 0, speed * Time.deltaTime);   
    }
}
