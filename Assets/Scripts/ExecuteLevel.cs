using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteLevel : MonoBehaviour
{

    public static List<Vector3> GRAPH_REFERENCE = new List<Vector3>(); // TODO: Something unique here
    
    public float DERAIL_THRESHOLD = 100;
    public float GRAPH_INPUT_DISTANCE = 1000;
    
    public GameObject train;
    public GameObject[] graphs;
    public List<Vector3> waypoints1;
    public List<Vector3> waypoints2;
    public List<Vector3> waypoints3;
    public List<Vector3> waypoints4;
    public List<Vector3> waypoints5;
    public List<Vector3> waypointsEnd;

    private List<List<Vector3>> waypoints;
    private int waypointIndex;
    private int graphIndex;
    private Nullable<Vector2> graphEnd;
    private bool enterPressed;

    // Start is called before the first frame update
    void Start()
    {
        // I have done this because it allows easy editing of waypoints in the unity editor.
        // Yes, it looks stupid. It makes it easier to edit the levels.
        waypoints = new List<List<Vector3>>();
        if (waypoints1.Count > 0) {
            waypoints.Add(waypoints1);
            waypoints.Add(GRAPH_REFERENCE);
        }
        if (waypoints2.Count > 0) {
            waypoints.Add(waypoints2);
            waypoints.Add(GRAPH_REFERENCE);
        }
        if (waypoints3.Count > 0) {
            waypoints.Add(waypoints3);
            waypoints.Add(GRAPH_REFERENCE);
        }
        if (waypoints4.Count > 0) {
            waypoints.Add(waypoints4);
            waypoints.Add(GRAPH_REFERENCE);
        }
        if (waypoints5.Count > 0) {
            waypoints.Add(waypoints5);
            waypoints.Add(GRAPH_REFERENCE);
        }
        waypoints.Add(waypointsEnd);

        // Initialise values
        waypointIndex = 0;
        graphEnd = null;
        enterPressed = false;
    }
    
    private GameObject GetClosestGraph() {
        GameObject closestGraph = null;
        float distance = 999999999;
        for (int i=0; i<graphs.Length; i++) {
            GameObject graph = graphs[i];
            GraphHandler handler = graph.GetComponent<GraphHandler>();
            if (handler.IsReadOnly()) {
                continue;
            }
            float newDistance = Vector2.Distance(graph.transform.position, train.transform.position);
            if (newDistance < distance) {
                closestGraph = graph;
                distance = newDistance;
            }
        }
        if ((distance) < GRAPH_INPUT_DISTANCE)
        {
            return closestGraph;
        } else {
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        // Enter toggles speed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!enterPressed)
            {
                train.GetComponent<FollowWaypoints>().ToggleSpeed();
            }
            enterPressed = true;
        }
        else
        {
            enterPressed = false;
        }
        
        // Turbo speed
        if (Input.GetKeyDown(KeyCode.F1))
        {
            // Note it is disabled by pressing ENTER, so no need for the extra variable
            train.GetComponent<FollowWaypoints>().TurboSpeed();
        }
        
        // Find the nearest graph (has to be nearby)
        GameObject graph = GetClosestGraph();
        
        // Update it with whatever the user types
        if (graph != null) {
            GraphHandler handler = graph.GetComponent<GraphHandler>();
            bool modified = false;
            foreach (char c in Input.inputString)
            {
                switch (c)
                {
                    case '\b': // Backspace
                        if (handler.equation.Length > 0)
                        {
                            handler.equation = handler.equation.Substring(0, handler.equation.Length - 1);
                            modified = true;
                        }
                        break;
                    case '\n': // Enter
                    case '\r': // Return
                        // Handled elsewhere
                        // This condition ensures if the key is held, it only triggers once
                        break;
                    default:
                        handler.equation += c;
                        modified = true;
                        break;
                }
            }

            
        
            // Control wipes the current input
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                handler.equation = "";
                modified = true;
            }

            // Update the graph
            if (modified)
            {
                handler.UpdateGraph();
            }
        }
        
    }
    
    /**
     * Complete the level!
     */
    public void CompleteLevel() {
        // TODO
        Debug.Log("You did it! Congratulations!");
    }
    
    /**
     * The train should call this whenever it has run out of waypoints to follow.
     *
     * Will return either:
     *  - A list of waypoints
     *  - null: Signals the train to blow itself up and derail
     */
    public List<Vector3> RequestWaypoints() {
        // Check if we've reached the end of the level
        if (waypointIndex >= waypoints.Count /*|| waypoints[waypointIndex+1].Count == 0*/) {
            CompleteLevel();
            return null; 
        }
        
        // Get the next waypoints
        List<Vector3> waypointsOutput = waypoints[waypointIndex++];
        
        // Check if we were previously following a graph
        if (graphEnd != null) {
            // The train was following a graph - we need to check it can safely move from the graph to the fixed section
            float distance = Vector2.Distance((Vector2) graphEnd, waypointsOutput[0]);
            graphEnd = null;
            Debug.Log("Distance is " + distance);
            if (distance > DERAIL_THRESHOLD) {
                return null;
            }
        }
        
        // Check if we need to follow a graph now
        if (waypointsOutput == GRAPH_REFERENCE) {
            // A special value - signals that there is a graph here that should be used instead
            // The following function handles whether the train can reach the graph or not, and will return null if the
            // train is unable to safely 
            GameObject nextGraph = GetNextGraph();
            if (nextGraph == null) {
                // We're at the end! Hooray!
                CompleteLevel();
                return null;
            } else {
                waypointsOutput = GetGraphWaypoints(nextGraph);
                nextGraph.GetComponent<GraphHandler>().MarkReadOnly();
            }
        }
        
        // Output the waypoints for the train to follow
        return waypointsOutput;
    }
    
    private GameObject GetNextGraph() {
        if (graphIndex >= graphs.Length) {
            return null;
        }
        return graphs[graphIndex++];
    }
    
    private List<Vector3> GetGraphWaypoints(GameObject graph) {
        GraphHandler handler = graph.GetComponent<GraphHandler>();
        List<List<Vector3>> segments = handler.RequestWaypoints();
        
        // Ensure the graph is actually valid, otherwise derail instantly
        if (segments == null || segments.Count == 0) {
            Debug.Log("Invalid graph! Derailing...");
            return null;
        }
        
        // Obtain the 'best' segment (the one that starts the closest to the train's position)
        List<Vector3> bestSegment = segments[0];
        Vector3 trainFront = train.GetComponent<FollowWaypoints>().GetFrontOfTrain();
        Debug.Log("Train front is: " + trainFront);
        float distance = Vector2.Distance(trainFront, bestSegment[0]);
        for (int i = 1; i < segments.Count; i++) {
            List<Vector3> segment = segments[i];
            float newDistance = Vector2.Distance(trainFront, segment[0]);
            if (newDistance <= distance) {
                bestSegment = segment;
                distance = newDistance;
            }
        }
        
        // Check this distance is actually close enough, otherwise derail
        if (distance > DERAIL_THRESHOLD) {
            Debug.Log("Not close enough! Distance: " + distance);
            return null;
        }
        
        // All good - tell the train to follow these waypoints. We'll decide if it's okay at the end
        graphEnd = bestSegment[bestSegment.Count-1];
        return bestSegment;
    }
}
