using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{

    public GameObject graphText;
    public GameObject timeText;
    public GameObject frogText;
    public GameObject graphPoints;
    public GameObject timePoints;
    public GameObject frogPoints;
    public GameObject score;
    public GameObject timer;
    public String nextLevel;
    public int expectedTimeInSeconds;
    
    // Start is called before the first frame update
    void Start()
    {
        // Haven't done this yet - hide it for now
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Complete(int points, int collectedFrogs, int totalFrogs, int nGraphs)
    {
        // NOTE: Frogs and Graphs are added to points in real-time. Time bonus added at end.
        
        // Display the menu
        gameObject.GetComponent<Canvas>().enabled = true;
        
        // Graphs
        graphText.GetComponent<TextMeshProUGUI>().text = "Graphs: " + nGraphs + "/" + nGraphs;
        graphPoints.GetComponent<TextMeshProUGUI>().text = "+" + nGraphs * 250;
        
        // Frogs
        frogText.GetComponent<TextMeshProUGUI>().text = "Frogs: " + collectedFrogs + "/" + totalFrogs;
        frogPoints.GetComponent<TextMeshProUGUI>().text = "+" + collectedFrogs * 1000;
        
        // Time
        double timeInMs = timer.GetComponent<Timer>().GetElapsedTime();
        timer.GetComponent<Timer>().StopTimer();
        double minutes = Math.Floor(timeInMs / 60000);
        double seconds = Math.Floor((timeInMs - minutes * 60000) / 1000);
        timeText.GetComponent<TextMeshProUGUI>().text = "Time: " + minutes.ToString("0") + ":" + seconds.ToString("00");
        int timeBonus = CalculateTimeBonus(timeInMs);
        points += timeBonus;
        timePoints.GetComponent<TextMeshProUGUI>().text = "+" + timeBonus.ToString("0");
        
        // Score
        score.GetComponent<TextMeshProUGUI>().text = "Score: " + points;
        
    }

    public int CalculateTimeBonus(double timeInMs)
    {
        // int bonus = (int)Math.Floor((expectedTimeInSeconds * 1000) / timeInMs * 5000);
        double expected = expectedTimeInSeconds * 1000;
        if (timeInMs > 2 * expected)
        {
            return 0;
        }
        double delta = Math.Floor(expected - timeInMs);
        return 5000 + (int) Math.Floor(delta / expected * 5000);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
