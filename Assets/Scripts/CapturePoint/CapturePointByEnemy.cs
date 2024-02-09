using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CapturePointByEnemy : MonoBehaviour // attach this script to the DetectionArea of the Point
{
    public float remainingTime;
    public TMP_Text capturePointTimerText;
    public bool IsEnemyInPoint;
    private void OnTriggerStay(Collider enemy)
    {
        if (enemy.CompareTag("OttomanRecruit")
            || enemy.CompareTag("OttomanGunnerRecruit")
            || enemy.CompareTag("MeleeJanissary")
            || enemy.CompareTag("RangedJanissary")
            || enemy.CompareTag("OttomanCannon"))
        {
            IsEnemyInPoint = true;
            Debug.Log(enemy.name);
        }
        else
        {
            IsEnemyInPoint = false;
        }
        
    }

    private void Update()
    {
        if (IsEnemyInPoint == true)
        {
            remainingTime = remainingTime - Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            capturePointTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            remainingTime = remainingTime + 0;
        }

        /*if(remainingTime < 0)
        {
            remainingTime = 0;
            capturePointTimerText.text = string.Format("{0:00}:{1:00}", "0", "0");
        }*/
    }
}
