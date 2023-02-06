using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private double startTime;
    private double currentTime;
    private double elapsedTimeMS;
    private double minutes;
    private double seconds;
    private double milliseconds;
    private TextMeshProUGUI text;
    private bool isActive;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            elapsedTimeMS = currentTime - startTime;
            minutes = Math.Floor(elapsedTimeMS / 60000);
            seconds = Math.Floor((elapsedTimeMS - minutes * 60000) / 1000);
            milliseconds = elapsedTimeMS % 1000;
            text.text = minutes.ToString("0") + ":" + seconds.ToString("00") + "." + milliseconds.ToString("000");
        }
    }

    public double GetElapsedTime()
    {
        return elapsedTimeMS;
    }

    public void StopTimer()
    {
        isActive = false;
    }
}
