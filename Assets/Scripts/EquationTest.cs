using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// THIS FILE IS NOT CURRENTLY USED
// DO NOT RELY ON ANYTHING IN THIS FILE

public class EquationTest : MonoBehaviour
{
    public GameObject textInput;
    public TMP_Text textInputText;

    // Start is called before the first frame update
    void Start()
    {
        textInputText = textInput.GetComponent<TextMeshPro>();
        textInput = GameObject.FindWithTag("EquationInputBox");
        Debug.Log(textInput);
    }

    // Update is called once per frame
    void Update()
    {
        textInputText.text = "Haha funny";
        Debug.Log(gameObject.GetComponent<TextMeshPro>().text);
        Debug.Log(textInput.GetComponent<TextMeshPro>().text);
        gameObject.GetComponent<TextMeshPro>().text = textInput.GetComponent<TextMeshPro>().text;
    }
}
