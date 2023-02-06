using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailLevel : MonoBehaviour
{
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
        gameObject.GetComponent<Canvas>().enabled = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
