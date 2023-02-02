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

            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, (Vector2.SignedAngle((target - position), new Vector2(1, 0))));
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }
    }
}
