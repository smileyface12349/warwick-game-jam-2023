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
    
    // Start is called before the first frame update
    private void Start()
    {
        // TODO: Retrieve size of graph and draw lines accordingly
        
        ChangeEquation();
        RefreshGraph();
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO: Take equation in input box and plot on graph
        
    }

    private List<List<Vector3>> CheckContinuity(List<Vector3> allPoints)
    {
        List<List<Vector3>> pointsOutput = new List<List<Vector3>>();
        pointsOutput.Add(allPoints);
        return pointsOutput;
    }

    private float GetXCoordinate(float x)
    {
        return -200 + 400 * ((x - bottomLeft.x) / (topRight.x - bottomLeft.x));
    }
    
    private float GetYCoordinate(float y)
    {
        return -200 + 400 * ((y - bottomLeft.y) / (topRight.y - bottomLeft.y));
    }

    private void ChangeEquation()
    {
        Entity expr = equation;
        theFunction = expr.Compile<float, float>("x");
    }

    private List<Vector3> DeterminePoints()
    {
        List<Vector3> points = new List<Vector3>();
        float increment = (topRight.x - bottomLeft.x) / nPoints;
        for (float x = bottomLeft.x; x < topRight.x; x += increment)
        {
            float y = theFunction(x);
            // TODO: Simple boundary check here - divide into separate lists
            // TODO: Probably worth checking continuity here as well
            points.Add(new Vector3(GetXCoordinate(x), GetYCoordinate(y)));
            Debug.Log(GetXCoordinate(x) + ", " + GetYCoordinate(y));
        }
        Debug.Log(points);
        return points;
    }

    private void RefreshGraph()
    {
        pointsList = CheckContinuity(DeterminePoints());
        RenderGraph();
    }

    private void RenderGraph()
    {
        for (int i = 0; i < pointsList.Count; i++)
        {
            GameObject line = new GameObject("Line");
            line.transform.position = gameObject.transform.position;
            LineRenderer l = line.AddComponent<LineRenderer>();
            l.startWidth = 1f;
            l.endWidth = 1f;
            l.useWorldSpace = false;
            l.startColor = new Color(1, 0, 0);
            l.endColor = l.startColor;
            Vector3[] positions = pointsList[i].ToArray();
            l.positionCount = positions.Length;
            l.SetPositions(positions);
        }
        
    }
}
