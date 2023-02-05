using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastForward : MonoBehaviour
{

    public GameObject train;
    
    private FollowWaypoints moveScript;
    private Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        moveScript = train.GetComponent<FollowWaypoints>();
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveScript.IsFast())
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }
    }
}
