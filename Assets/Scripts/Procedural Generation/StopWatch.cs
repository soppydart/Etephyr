using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class StopWatch : MonoBehaviour
{
    bool stopWatchActive;
    float currentTime;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    void Start()
    {
        currentTime = 0;
        stopWatchActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopWatchActive == true)
            currentTime += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
        ScoreText();
    }
    public void StopTime()
    {
        stopWatchActive = false;
    }
    public void StartTime()
    {
        stopWatchActive = true;
        FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().isPlayerJumpingAllowed = true;
    }
    void ScoreText()
    {
        finalScoreText.text = "You finished the level in " + timeText.text + ".";
    }
}
