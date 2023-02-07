using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    public  GameObject   levelController;
    private ExecuteLevel script;

    void Start()
    {
        script = levelController.GetComponent<ExecuteLevel>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log(col.gameObject.name);
        if (col.gameObject.CompareTag("Collectible"))
        {
            Debug.Log("You picked up a collectible! Congratulations!");
            Destroy(col.gameObject);
            script.CollectFrog();
        }
        if (col.gameObject.CompareTag("Wall"))
        {
            script.Fail();
        }
    }
}
