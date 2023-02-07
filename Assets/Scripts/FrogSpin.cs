using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogSpin : MonoBehaviour
{
    // Update is called once per frame
    void Update ()
    {
        transform.Rotate (0,0,100*Time.deltaTime); //rotates 50 degrees per second around z axis
    }
}
