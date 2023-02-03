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
    private static float SILLY_NUMBER = -100000f;
    
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

    private float InterpolatePoint(float y, float x1, float x2, float y1, float y2)
    {
        return x1 + (x2 - x1) * ((y - y1) / (y2 - y1));
    }

    private List<List<Vector3>> DeterminePoints()
    {
        float increment = (topRight.x - bottomLeft.x) / nPoints;
        List<List<Vector3>> points = new List<List<Vector3>>();
        List<Vector3> currentPoints = new List<Vector3>();
        Vector3 previousPoint = new Vector3(SILLY_NUMBER, SILLY_NUMBER);
        
        for (float x = bottomLeft.x; x < topRight.x+increment; x += increment)
        {
            bool valid = true;
            bool startNew = false;
            float newX = SILLY_NUMBER;
            float newY = SILLY_NUMBER;
            float y = theFunction(x);
            
            Debug.Log("Considering point (" + x + ", " + y + ")");
            
            // Check for OUT OF BOUNDS
            if (y > topRight.y) {  // ABOVE
                Debug.Log("It's too high! " + topRight.y);
                valid = false;
                // Check if the previous point is valid - if not, we don't need to do anything
                Debug.Log("Previous y: " + previousPoint.y);
                if (previousPoint.y != SILLY_NUMBER && previousPoint.y < topRight.y && previousPoint.y > bottomLeft.y) {
                    // This is the first point to step out of bounds
                    // Want to add a point on the boundary
                    // To make it easier, we will add a point that is pretty close to where it should be, using linear interpolation
                    newY = topRight.y;
                    newX = InterpolatePoint(newY, previousPoint.x, x, previousPoint.y, y);
                    Debug.Log(x);
                    Debug.Log(newX);
                    startNew = true;
                }
            }
            else if (y < bottomLeft.y) {  // BELOW
                valid = false;
                if (previousPoint.x != SILLY_NUMBER && previousPoint.y > bottomLeft.y && previousPoint.y < topRight.y) {
                    // (see comments above)
                    // I split this into separate things as I thought it was a good idea but in hindsight it was stupid
                    newY = bottomLeft.y;
                    newX = InterpolatePoint(newY, previousPoint.x, x, previousPoint.y, y);
                    startNew = true;
                }
            }
            // Bounds check passed - Now check if it is the first point to step into bounds
            else
            {
                if (previousPoint.y != SILLY_NUMBER && (previousPoint.y > topRight.y || previousPoint.y != SILLY_NUMBER && previousPoint.y < bottomLeft.y))
                {
                    // Previous point was out of bounds
                    // TODO: Could differentiate here to determine more reliably
                    // Determine whether we have come in from the bottom or the top
                    if (y >= bottomLeft.y + (topRight.y - bottomLeft.y) / 2)
                    {
                        newY = topRight.y;
                        Debug.Log("Coming in from the top! New Y: " + newY);
                    }
                    else
                    {
                        Debug.Log("Coming in from the bottom!");
                        newY = bottomLeft.y;
                    }
                    newX = InterpolatePoint(newY, previousPoint.x, x, previousPoint.y, y);
                    Debug.Log("Adding the point: (" + newX + ", " + newY + ")");
                    currentPoints.Add(new Vector3(GetXCoordinate(newX), GetYCoordinate(newY)));
                }
            }
            
            // TODO: Continuity check
            
            float plotX = x;
            float plotY = y;

            if (!valid && newX != SILLY_NUMBER && newY != SILLY_NUMBER)
            {
                plotX = newX;
                plotY = newY;
                valid = true;
            }
            
            Vector3 point = new Vector3(GetXCoordinate(plotX), GetYCoordinate(plotY));
                
            if (valid) {
                currentPoints.Add(point);
                Debug.Log("Adding: " + point);
            }

            if (startNew)
            {
                points.Add(currentPoints);
                currentPoints = new List<Vector3>();
            }
            
            previousPoint = new Vector2(plotX, plotY);
            
        }

        Vector3[] currentPointsArray = currentPoints.ToArray();
        if (currentPointsArray.Length > 0)
        {
            points.Add(currentPoints);
        }
        
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
