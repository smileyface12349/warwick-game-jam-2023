using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS FILE IS NOT CURRENTLY USED
// DO NOT RELY ON ANYTHING IN THIS FILE

public class TrainMoveTest : MonoBehaviour
{
    private Rigidbody2D rb;
    public  float       speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
