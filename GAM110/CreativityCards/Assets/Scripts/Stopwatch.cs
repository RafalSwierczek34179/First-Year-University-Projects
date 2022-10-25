using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Stopwatch : MonoBehaviour
{
    bool stopwatchActive = false;
    public float currentTime;
    public Text currentTimeText;

    //Score
    int score;
    public Text scoreText;
    public float multiplier = 5;

    private void Start()
    {
        currentTime = 0;
        stopwatchActive = true;
    }

    private void Update()
    {
        if (stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;            
        }

        score = Mathf.RoundToInt(currentTime * multiplier);
        scoreText.text = score.ToString();
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
    }

    public void StartStopwatch()
    {
        stopwatchActive = true;
    }

    public void StopStopwatch()
    {
        stopwatchActive = false;
    }
}
