using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public GameObject train;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If we've got no train to follow - don't try and follow a train!
        if (train == null)
        {
            return;
        }
        
        Vector3 position = train.transform.position;
        gameObject.transform.position = new Vector3(position.x + 300, position.y, -50);
    }
}
