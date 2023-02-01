using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using AngouriMath;

public class GraphPlotter : MonoBehaviour
{

    public GameObject graphContainer;
    private RectTransform rect;
    private Vector2 coordsBottomLeft;
    private Vector2 coordsTopRight;
    public Vector2 graphMin;
    public Vector2 graphMax;
    private Vector2 graphSize;
    private Vector2 containerSize;
    public int points = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        rect = graphContainer.GetComponent<RectTransform>();
        containerSize = rect.sizeDelta;
        graphSize = graphMax - graphMin;
        coordsBottomLeft = graphContainer.transform.position;
        coordsTopRight = (Vector2) coordsBottomLeft + containerSize;
        PlotPoints();
        Debug.Log(coordsBottomLeft);
        Debug.Log(coordsTopRight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject PlotPoint(Vector2 position)
    {
        if (position.y > coordsTopRight.y || position.y < coordsBottomLeft.y)
        {
            Debug.Log(position.y);
            return null;
        }
        GameObject point = new GameObject("point", typeof(Image));
        point.transform.SetParent(rect, false);
        RectTransform pointRect = point.GetComponent<RectTransform>();
        point.transform.position = position;
        pointRect.sizeDelta = new Vector2(0, 0);
        pointRect.anchorMin = new Vector2(0, 0);
        pointRect.anchorMax = new Vector2(0, 0);
        Debug.Log("Plotted: " + position);
        return point;
    }

    private float XValueToCoordinates(float x)
    {
        return coordsBottomLeft.x + containerSize.x / graphSize.x * (x - graphMin.x);
    }

    private float YValueToCoordinates(float y)
    {
        return coordsBottomLeft.y + containerSize.y / graphSize.y * (y - graphMin.y);
    }

    private void PlotPoints()
    {
        GameObject point = null;
        GameObject previousPoint = null;
        float increment = graphSize.x / points;
        Entity expr = "ln(x)"; // Here is where you can change the equation
        var theFunction = expr.Compile<float, float>("x");
        for (float x = graphMin.x; x < graphMax.x; x += increment)
        {
            previousPoint = point;
            
            point = PlotPoint(new Vector2(XValueToCoordinates(x), YValueToCoordinates(theFunction(x))));
            if (point && previousPoint)
            {
                ConnectPoints(previousPoint.transform.position, point.transform.position);
            }
        }
    }

    private void ConnectPoints(Vector2 position1, Vector2 position2)
    {
        GameObject connection = new GameObject("connection", typeof(Image));
        connection.transform.SetParent(rect, false);
        connection.GetComponent<Image>().color = new UnityEngine.Color(0.5f, 1, 0, .5f);
        RectTransform pointRect = connection.GetComponent<RectTransform>();
        Vector2 directionVector = (position2 - position1).normalized;
        float angle = -Vector2.SignedAngle(directionVector, new Vector2(1, 0));
        float distance = Vector2.Distance(position1, position2);
        pointRect.anchorMin = new Vector2(0, 0);
        pointRect.anchorMax = new Vector2(0, 0);
        pointRect.sizeDelta = new Vector2(distance, 3f);
        connection.transform.position = position1 + directionVector * distance * .5f;
        pointRect.localEulerAngles = new Vector3(0, 0, angle);
    }
}
