using System;
using System.Collections;
using System.Collections.Generic;
using AngouriMath;
using UnityEngine;

public class GraphHandler : MonoBehaviour
{

    public Vector2 bottomLeft;
    public Vector2 topRight;
    public String equation;
    public int nPoints = 100;

    private float pixelsPerX;
    private float pixelsPerY;
    private List<List<Vector3>> pointsList;
    private Func<float, float> theFunction;
    private Vector2 coordinatesBottomLeft;
    
    // Start is called before the first frame update
    private void Start()
    {
        // TODO: Retrieve size of graph and draw lines accordingly
        
        coordinatesBottomLeft = gameObject.transform.position;
        
        ChangeEquation();
        RefreshGraph();
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO: Take equation in input box and plot on graph
        
    }

    private float GetXCoordinate(float x)
    {
        return coordinatesBottomLeft.x - 200 + 400 * ((x - bottomLeft.x) / (topRight.x - bottomLeft.x));
    }
    
    private float GetYCoordinate(float y)
    {
        return coordinatesBottomLeft.y - 200 + 400 * ((y - bottomLeft.y) / (topRight.y - bottomLeft.y));
    }

    private void ChangeEquation()
    {
        Entity expr = equation;
        theFunction = expr.Compile<float, float>("x");
    }

    private List<List<Vector3>> DeterminePoints()
    {
        float increment = (topRight.x - bottomLeft.x) / nPoints;
        List<List<Vector3>> points = new List<List<Vector3>>();
        List<Vector3> currentPoints = new List<Vector3>();
        Vector3 previousPoint = new Vector3(-1000, -1000);
        
        for (float x = bottomLeft.x; x < topRight.x+increment; x += increment)
        {
            bool valid = true;
            float newX = -1000;
            float newY = -1000;
            float y = theFunction(x);
            
            // Bounds Checks
            Debug.Log("Checking bounds for (" + x + ", " + y + ")");
            if (y > topRight.y) {  // ABOVE
                Debug.Log("It's too high! " + topRight.y);
                valid = false;
                // Check if the previous point is valid - if not, we don't need to do anything
                Debug.Log("Previous y: " + previousPoint.y);
                if (previousPoint.y != -1000 && previousPoint.y <= topRight.y) {
                    // This is the first point to step out of bounds
                    // Want to add a point on the boundary
                    // To make it easier, we will add a point that is pretty close to where it should be, using linear interpolation
                    newY = topRight.y;
                    newX = previousPoint.x + (x - previousPoint.x) * ((newY - previousPoint.y) / (y - previousPoint.y));
                    Debug.Log(x);
                    Debug.Log(newX);
                }
            }
            else if (y < bottomLeft.y) {  // BELOW
                valid = false;
                if (previousPoint.x != -1000 && previousPoint.y >= bottomLeft.y) {
                    // (see comments above)
                    // I split this into separate things as I thought it was a good idea but in hindsight it was stupid
                    newY = bottomLeft.y;
                    newX = previousPoint.x + (x - previousPoint.x) * ((newY - previousPoint.y) / (y - previousPoint.y));
                }
            }
            
            float plotX = x;
            float plotY = y;
            
            // TODO: Countinuity check
            if (!valid && newX != -1000 && newY != -1000)
            {
                plotX = newX;
                plotY = newY;
                valid = true;
            }
            
            Vector3 point = new Vector3(GetXCoordinate(plotX), GetYCoordinate(plotY));
                
            if (valid) {
                currentPoints.Add(point);
                Debug.Log(point);
            }
            
            previousPoint = new Vector2(plotX, plotY);
            
        }
        Debug.Log(currentPoints.ToArray().Length);
        points.Add(currentPoints);
        return points;
    }

    private void RefreshGraph()
    {
        pointsList = DeterminePoints();
        RenderGraph();
    }

    private void RenderGraph()
    {
        for (int i = 0; i < pointsList.Count; i++)
        {
            GameObject line = new GameObject("Line");
            line.transform.position = gameObject.transform.position;
            LineRenderer l = line.AddComponent<LineRenderer>();
            l.startWidth = 5f;
            l.endWidth = 5f;
            l.useWorldSpace = true;
            l.startColor = new Color(1, 0, 0);
            l.endColor = l.startColor;
            Vector3[] positions = pointsList[i].ToArray();
            l.positionCount = positions.Length;
            l.SetPositions(positions);
        }
        
    }
}
