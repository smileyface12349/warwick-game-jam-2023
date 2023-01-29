using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputTest : MonoBehaviour
{

    public string equation;
    public GameObject inputField;
    public GameObject output;

    public void UpdateEquation()
    {
        equation = inputField.GetComponent<TextMeshProUGUI>().text;
        output.GetComponent<TextMeshProUGUI>().text = "y = " + equation;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEquation();
    }
}
