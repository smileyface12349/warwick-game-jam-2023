using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject track;
    public GameObject background;
    
    public int transitionSpeed;
    public int resetThreshold;

    private double totalDistance;
    private Vector3 backgroundStart;
    private Vector3 trackStart;
    
    // Start is called before the first frame update
    void Start()
    {
        backgroundStart = background.transform.position;
        trackStart = track.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Update track position
        Vector3 position = track.transform.position;
        track.transform.position = new Vector3(position.x - transitionSpeed * Time.deltaTime, position.y, position.z);
        
        // Update background position
        position = background.transform.position;
        background.transform.position = new Vector3(position.x - transitionSpeed * Time.deltaTime, position.y, position.z);

        // Increment counter
        totalDistance += transitionSpeed * Time.deltaTime;
        if (totalDistance >= resetThreshold)
        {
            // Move them back to where they started
            background.transform.position = backgroundStart;
            track.transform.position = trackStart;
            totalDistance = 0;
        }

    }

    public void Level0()
    {
        SceneManager.LoadScene("Level0");
    }
    
    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
