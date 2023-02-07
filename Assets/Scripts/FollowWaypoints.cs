using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    public List<Vector3> waypoints;
    public GameObject levelController;
    public int width = 128;
    public float slowSpeed;
    public float fastSpeed;
    public float superFastSpeed;
    public int speedSetting;
    public GameObject failCanvas;
    public GameObject explosion;
    
    private float speed;
    private int pointIndex;
    private ExecuteLevel executeLevel;
    private Vector3 directionVector;

    // Start is called before the first frame update
    void Start()
    {
        speedSetting = 1;
        executeLevel = levelController.GetComponent<ExecuteLevel>();
        pointIndex = 0;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        // Update speed
        switch (speedSetting)
        {
            case 0:
                speed = slowSpeed;
                break;
            case 1:
                speed = fastSpeed;
                break;
            case 2:
                speed = superFastSpeed;
                break;
        }


        // Check if we've run out of waypoints
        if (waypoints != null && pointIndex >= waypoints.Count) {
            // Ask for some more
            pointIndex = 0;
            waypoints = executeLevel.RequestWaypoints();
            if (waypoints == null) {
                if (!executeLevel.IsCompleted())
                {
                    Fail();
                }
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
            // Debug.Log("Hello, I'm a train! I'm gonna try and move towards " + target);
            transform.position = Vector2.MoveTowards(transform.position, target, maxDistance);
            
            // Check if we've reached the target, and increment the point index if so
            if (Vector2.Distance(target, gameObject.transform.position) == 0) {
                pointIndex++;
            }
        
        }
    }

    public void Fail()
    {
        failCanvas.GetComponent<FailLevel>().Derail();
    }

    private void Explode()
    {
        explosion = GameObject.Find("explosion-Sheet_0");
        explosion.transform.position = gameObject.transform.position;

    }

    public void TurboSpeed()
    {
        speedSetting = 2;
    }

    public void ToggleSpeed()
    {
        if (speedSetting == 1)
        {
            speedSetting = 0;
        }
        else
        {
            speedSetting = 1;
        }
    }

    public bool IsFast()
    {
        if (speedSetting == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public Vector3 GetFrontOfTrain()
    {
        // Replace this with the middle of the train
        // Everything is now handled from the middle of the train, so this is not necessary
        return gameObject.transform.position;
        
        // Debug.Log(directionVector);
        // Debug.Log(gameObject.GetComponent<SpriteRenderer>().sprite.rect.width/2);
        // return gameObject.transform.position + directionVector * (width/2);
    }
}
