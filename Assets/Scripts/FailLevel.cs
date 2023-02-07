using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailLevel : MonoBehaviour
{

    public GameObject timer;
    public GameObject train;
    public GameObject explosion;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Derail()
    {
        Explode();
        // Wait for a second or so after exploding
        //gameObject.GetComponent<Canvas>().enabled = true;
        //timer.GetComponent<Timer>().StopTimer();
        
    }

    public void Explode()
    {
        explosion = GameObject.Find("explosion-Sheet_0");
        train = GameObject.Find("Train");
        explosion.transform.position = train.transform.position;    
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
