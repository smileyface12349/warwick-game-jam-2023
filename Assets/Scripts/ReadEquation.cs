using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// THIS FILE IS NOT CURRENTLY USED
// DO NOT RELY ON ANYTHING IN THIS FILE

public class ReadEquation : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.GetComponent<TextMeshPro>().text);
    }
}
