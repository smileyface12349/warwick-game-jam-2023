using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    public Vector2[] waypoints;
    public Vector2 position;
    public float speed = 3.0f;
    public int pointIndex = 0;
    public bool currentlyMoving = false;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[pointIndex];
        position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (pointIndex <= (waypoints.Length - 1)) {
            target = waypoints[pointIndex];
            position = gameObject.transform.position;
            if (Vector2.Distance(target, position) == 0) {
                pointIndex++;
            }

            // rotate towards the current target
            gameObject.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle((target - position), new Vector2(1, 0)));

            // move towards the target
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }
    }
}
