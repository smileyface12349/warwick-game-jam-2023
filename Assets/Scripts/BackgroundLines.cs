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
        Debug.Log(canvasBotLef);
        Debug.Log(canvasTopRig);

        //Access graph plotter
        gp = graph.GetComponent<GraphHandler>();
        //Get graph's coordinates of corners
        graphWidth   = gp.topRight.x - gp.bottomLeft.x;
        graphHeight  = gp.topRight.y - gp.bottomLeft.y;

        //Log
        Debug.Log(canvasWidth);
        Debug.Log(canvasHeight);
        Debug.Log(graphWidth);
        Debug.Log(graphHeight);

        //Determine space between lines
        spacingX = canvasWidth / graphWidth;
        spacingY = canvasHeight / graphHeight;

        //Central vector
        centre = new Vector2(canvasBotLef.x + ((float) 0.5) * canvasWidth, canvasBotLef.y + ((float) 0.5) * canvasHeight);

        //Summon small x lines
        for (float i = 0; i <= canvasWidth; i = i + spacingX) {
            Instantiate(smallVer, new Vector2(canvasBotLef.x + i, centre.y), Quaternion.identity);    
        }

        //Summon small y lines
        for (float i = 0; i <= canvasHeight; i = i + spacingY) {
            Instantiate(smallHor, new Vector2(centre.x, canvasBotLef.y + i), Quaternion.identity);    
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
