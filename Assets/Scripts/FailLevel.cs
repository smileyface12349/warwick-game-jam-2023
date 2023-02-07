using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailLevel : MonoBehaviour
{

    public GameObject timer;
    public GameObject train;
    public GameObject explosion;

    private AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Derail()
    {
        try
        {
            Explode();
            Invoke("EndLevel", 1);
        }
        catch (Exception e)
        {
            // As important as the explosion is, if something goes wrong, let's just skip it
            EndLevel();
        }

        source.Play();
    }

    public void EndLevel()
    {
        gameObject.GetComponent<Canvas>().enabled = true;
        timer.GetComponent<Timer>().StopTimer();
    }

    public void Explode()
    {
        explosion = GameObject.Find("explosion-Sheet_0");
        train = GameObject.Find("Train");
        Destroy(train);
        explosion.transform.position = train.transform.position;    
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
