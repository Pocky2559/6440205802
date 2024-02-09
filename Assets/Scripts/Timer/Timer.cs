using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timeText;
    public float remainingTime;

    void Update()
    { 
        if (remainingTime > 0)
        {
            remainingTime = remainingTime - Time.deltaTime;
        }

        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }

        remainingTime = remainingTime - Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

       
    }
}
