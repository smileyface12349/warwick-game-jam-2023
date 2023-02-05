using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayPoints : MonoBehaviour
{

    public GameObject levelController;
    
    private ExecuteLevel levelHandler;
    private TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        levelHandler = levelController.GetComponent<ExecuteLevel>();
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = levelHandler.GetPoints().ToString("00000");
    }
}
