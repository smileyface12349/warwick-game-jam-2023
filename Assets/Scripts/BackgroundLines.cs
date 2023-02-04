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
    private GraphHandler gp;

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

        //Summon small x lines
        for (float i = 0; i <= canvasTopRig.x; i = i + spacingX) {
            Instantiate(smallVer, new Vector2(i - 200, 0 + 100), Quaternion.identity);    
        }

        //Summon small y lines
        for (float i = 100; i <= canvasTopRig.y; i = i + spacingY) {
            Instantiate(smallHor, new Vector2(0, i - 200), Quaternion.identity);    
        }
        
        //Make lines that pass through origin
        Instantiate(bigHor, new Vector2(0, 0 + 100), Quaternion.identity);    
        Instantiate(bigVer, new Vector2(0, 0 + 100), Quaternion.identity);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
