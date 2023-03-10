using System;
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
    private float offsetX;
    private float offsetY;

    private int smallLineWidth;
    private int bigLineWidth;

    private Vector2 centre;

    public  GameObject smallHor;
    public  GameObject bigHor;
    public  GameObject smallVer;
    public  GameObject bigVer;
    private GraphHandler gp;

    // Start is called before the first frame update
    void Start()
    {
        //Access RectTransform
        rect = gameObject.GetComponent<RectTransform>();
        //Get size of GraphBackground
        containerSize = rect.sizeDelta;
        canvasWidth  = containerSize.x;
        canvasHeight = containerSize.y;

        //Bottom left coord is just the possiont
        canvasBotLef = new Vector2(rect.position.x - ((float) 0.5) * canvasWidth, rect.position.y - ((float) 0.5) * canvasHeight);
        //Top right is bottom left plus size
        canvasTopRig = (Vector2) canvasBotLef + containerSize;
        //Log
        // Debug.Log(canvasBotLef);
        // Debug.Log(canvasTopRig);

        //Access graph plotter
        gp = graph.GetComponent<GraphHandler>();
        //Get graph's coordinates of corners
        graphWidth   = gp.topRight.x - gp.bottomLeft.x;
        graphHeight  = gp.topRight.y - gp.bottomLeft.y;

        //Log
        // Debug.Log(canvasWidth);
        // Debug.Log(canvasHeight);
        // Debug.Log(graphWidth);
        // Debug.Log(graphHeight);

        //Determine space between lines
        spacingX = canvasWidth / graphWidth;
        spacingY = canvasHeight / graphHeight;
        
        // Determine offsets (if co-ordinates are not integers)
        if (gp.bottomLeft.x == Math.Floor(gp.bottomLeft.x))
        {
            offsetX = 0;
        } 
        else
        {
            offsetX = (1 - (gp.bottomLeft.x - (float) Math.Floor(gp.bottomLeft.x))) * spacingX;
            canvasWidth -= 1; // a bad workaround for it drawing one extra line if total width is an integer
        }

        if (gp.bottomLeft.y == Math.Floor(gp.bottomLeft.y))
        {
            offsetY = 0;
        }
        else
        {
            offsetY = (1 - (gp.bottomLeft.y - (float) Math.Floor(gp.bottomLeft.y))) * spacingY;
            canvasHeight -= 1; // a bad workaround for it drawing one extra line if total width is an integer
        }
        

        //Central vector
        centre = new Vector2(canvasBotLef.x + ((float) 0.5) * canvasWidth, canvasBotLef.y + ((float) 0.5) * canvasHeight);

        //Summon small x lines
        for (float i = 0; i <= canvasWidth; i += spacingX) {
            Instantiate(smallVer, new Vector2(offsetX + canvasBotLef.x + i, centre.y), Quaternion.identity);    
        }

        //Summon small y lines
        for (float i = 0; i <= canvasHeight; i = i + spacingY) {
            Instantiate(smallHor, new Vector2(centre.x, offsetY + canvasBotLef.y + i), Quaternion.identity);    
        }

        //Make lines that pass through origin
        Instantiate(bigHor, centre, Quaternion.identity);    
        Instantiate(bigVer, centre, Quaternion.identity);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
