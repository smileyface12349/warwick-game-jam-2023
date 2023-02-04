using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    public List<Vector3> waypoints;
    public float speed;
    public GameObject levelController;
    public int width = 128;
    
    private int pointIndex;
    private ExecuteLevel executeLevel;
    private Vector3 directionVector;

    // Start is called before the first frame update
    void Start()
    {
        executeLevel = levelController.GetComponent<ExecuteLevel>();
        pointIndex = 0;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if we've run out of waypoints
        if (waypoints != null && pointIndex >= waypoints.Count) {
            // Ask for some more
            waypoints = executeLevel.RequestWaypoints();
            if (waypoints == null) {
                // TODO: Insert cool blowing up / derailing animation here
                Debug.Log("Insert cool explosion / derailing animation here");
                // Destroy(gameObject); // NOTE: This destroys the camera along with it
            }
        }
        
        // If there's still waypoints left, follow them
        if (waypoints != null) {
            // Define some variables
            float maxDistance = speed * Time.deltaTime;
            Vector3 target = waypoints[pointIndex];
            
            // Rotate the train to face the target
            // TODO: Linear interpolation here to deal with funky lines
            directionVector = Vector3.Normalize(target - gameObject.transform.position);
            gameObject.transform.eulerAngles = new Vector3(0, 0, -Vector2.SignedAngle(directionVector, new Vector2(1, 0)));

            // Move towards the target
            Debug.Log("Hello, I'm a train! I'm gonna try and move towards " + target);
            transform.position = Vector2.MoveTowards(transform.position, target, maxDistance);
            
            // Check if we've reached the target, and increment the point index if so
            if (Vector2.Distance(target, gameObject.transform.position) == 0) {
                pointIndex++;
            }
        
        }
    }
    
    public Vector3 GetFrontOfTrain() {
        Debug.Log(directionVector);
        Debug.Log(gameObject.GetComponent<SpriteRenderer>().sprite.rect.width/2);
        return gameObject.transform.position + directionVector * (width/2);
    }
}
