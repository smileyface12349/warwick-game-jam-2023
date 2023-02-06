using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailLevel : MonoBehaviour
{

    public GameObject timer;
    
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
        // TODO: Wait for a second or so after exploding
        gameObject.GetComponent<Canvas>().enabled = true;
        timer.GetComponent<Timer>().StopTimer();
        
    }

    public void Explode()
    {
        // TODO: Here is where the super cool explosion animation goes
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
