using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLines : MonoBehaviour
{
    public  GameObject graph;
    private RectTransform rect;
    private Vector2 containerSize;
    private Vector2 canvasBotLef;
    private Vector2 canvasTopRig;

    private float canvasWidth;
    private float canvasHeight;
    private float graphWidth;
    private float graphHeight;

    private float spacingX;
    private float spacingY;

    private int smallLineWidth;
    private int bigLineWidth;

    private float centreX;
    private float centreY;

    public  GameObject smallHor;
    public  GameObject bigHor;
    public  GameObject smallVer;
    public  GameObject bigVer;
    private GraphPlotter gp;

    // Start is called before the first frame update
    void Start()
    {
        //Access RectTransform
        rect = gameObject.GetComponent<RectTransform>();
        //Get size of GraphBackground
        containerSize = rect.sizeDelta;
        //Bottom left coord is just the possiont
        canvasBotLef = rect.position;
        //Top right is bottom left plus size
        canvasTopRig = (Vector2) canvasBotLef + containerSize;
        //Log
        Debug.Log(canvasBotLef);
        Debug.Log(canvasTopRig);

        canvasWidth  = containerSize.x;
        canvasHeight = containerSize.y;

        //Access graph plotter
        gp = graph.GetComponent<GraphPlotter>();
        //Get graph's coordinates of corners
        graphWidth   = gp.XValueToCoordinates(gp.coordsTopRight.x) - gp.XValueToCoordinates(gp.coordsBottomLeft.x);
        graphHeight  = gp.YValueToCoordinates(gp.coordsTopRight.y) - gp.YValueToCoordinates(gp.coordsBottomLeft.y);

        //Log
        Debug.Log(canvasWidth);
        Debug.Log(canvasHeight);
        Debug.Log(graphWidth);
        Debug.Log(graphHeight);

        //Determine space between lines
        spacingX = canvasWidth / graphWidth;
        spacingY = canvasHeight / graphHeight;

        //Make lines that pass through origin
        Instantiate(bigHor, new Vector2(0, 0 + 100), Quaternion.identity);    
        Instantiate(bigVer, new Vector2(0, 0 + 100), Quaternion.identity);    

        //Summon smakll x lines
        for (float i = 0; i <= canvasTopRig.x; i = i + spacingX) {
            if (i != 0) {
                Instantiate(smallVer, new Vector2(i - 200, 0 + 100), Quaternion.identity);    
            }
        }

        //Summon smakll y lines
        for (float i = 100; i <= canvasTopRig.y; i = i + spacingY) {
            if (i != 0) {
                Instantiate(smallHor, new Vector2(0, i - 200), Quaternion.identity);    
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
