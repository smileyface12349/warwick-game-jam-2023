using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    public  GameObject   LevelController;
    private ExecuteLevel Script;

    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "Collectible")
        {
            Destroy(col.gameObject);
            Script = LevelController.GetComponent<ExecuteLevel>();
            Script.CollectFrog();
        }
        else if (col.gameObject.name == "Wall")
        {
            Script = LevelController.GetComponent<ExecuteLevel>();
            Script.Fail();
        }
    }
}
