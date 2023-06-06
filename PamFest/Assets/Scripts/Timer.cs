using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    public bool run;
    public TMP_Text timerText;
    public float startTime;
    public float time;
    public float min;
    public float sec;
    public float totalTime;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;  
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            time = Time.time - startTime;
            sec = 0;
            min = totalTime - TimeSpan.FromSeconds(time).Seconds;
            // sets the time to be in the correct format for the game
            timerText.text = string.Format("{0:00}:{1:00}", sec, min);

            if (min <= 0)
            {
                // end the game
                gameManager.endRoundTime();
            }
        }
    }

    public void startTimer()
    {
        startTime = Time.time;
        run = true;
    }

    public void stopTimer()
    {
        timerText.text = "00:10";
        startTime = Time.time;
        run = false;
    }
}
