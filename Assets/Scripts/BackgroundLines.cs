using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLines : MonoBehaviour
{
    public  GameObject graph;
    private RectTransform rect;
    private Vector2 canvasBotLef;
    private Vector2 canvasTopRig;
    private Vector2 containerSize;

    private float canvasWidth;
    private float canvasHeight;
    private float graphWidth;
    private float graphHeight;

    private float spacingX;
    private float spacingY;

    private int smallLineWidth;
    private int bigLineWidth;

    // Start is called before the first frame update
    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        containerSize = rect.sizeDelta;
        canvasBotLef = gameObject.transform.position;
        canvasTopRig = (Vector2) canvasBotLef + containerSize;
        Debug.Log(canvasBotLef);
        Debug.Log(canvasTopRig);

        canvasWidth  = canvasTopRig.x - canvasBotLef.x;
        canvasHeight = canvasTopRig.y - canvasBotLef.y;

        graphWidth   = graph.GetComponent<GraphPlotter>().coordsTopRight.x - graph.GetComponent<GraphPlotter>().coordsBottomLeft.x;
        graphHeight  = graph.GetComponent<GraphPlotter>().coordsTopRight.y - graph.GetComponent<GraphPlotter>().coordsBottomLeft.y;

        spacingX = canvasWidth / graphWidth;
        spacingY = canvasHeight / graphHeight;

        //centreX = canvasBotLef.x - (spacingX * graph.GetComponent<GraphPlotter>().coordsBottomLeft.x);
        //centreY = canvasBotLef.y - (spacingY * graph.GetComponent<GraphPlotter>().coordsBottomLeft.y);

        //centreLineX = new GameObject("centreLineX");
        //xSR = centreLineX.AddComponent<SpriteRenderer>() as SpriteRenderer;
        //sprite = Sprite.Create(texture, new Rect(0, 0, 32, 32), new Vector2(16,16));
        //spriteRenderer.sprite = sprite;

        // for (float i = canvasBotLef.x; i <= canvasTopRig.x; i = i + spacingX) {
        //     
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
