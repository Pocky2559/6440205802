using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CapturePointByEnemy : MonoBehaviour // attach this script to the DetectionArea of the Point
{
    public float remainingTime;
    public TMP_Text capturePointTimerText;
    public bool IsEnemyInPoint;
    [SerializeField] private SoundEffectController soundEffectController;
    private bool isLosing;
    [SerializeField] private GameObject warningIndicatorUI;
    [SerializeField] private GameObject warningIndicatorAtPoint;


    private void OnTriggerStay(Collider enemy)
    {
        if (enemy.CompareTag("OttomanRecruit")
            || enemy.CompareTag("OttomanGunnerRecruit")
            || enemy.CompareTag("MeleeJanissary")
            || enemy.CompareTag("RangedJanissary")
            || enemy.CompareTag("OttomanCannon"))
        {
            IsEnemyInPoint = true;
            if(isLosing == false)
            {
                soundEffectController.PlayDefendedPointAlarm();
                warningIndicatorUI.SetActive(true);
                warningIndicatorAtPoint.SetActive(true);
                isLosing = true;
            }
        }
    }

    public void OnTriggerExit(Collider enemy)
    {
        if (enemy.CompareTag("OttomanRecruit")
            || enemy.CompareTag("OttomanGunnerRecruit")
            || enemy.CompareTag("MeleeJanissary")
            || enemy.CompareTag("RangedJanissary")
            || enemy.CompareTag("OttomanCannon"))
        {
            IsEnemyInPoint = false;
            if (isLosing == true)
            {
                soundEffectController.StopPlaySound();
                warningIndicatorUI.SetActive(false);
                warningIndicatorAtPoint.SetActive(false);
                isLosing = false;
            }
        }
    }

    private void Update()
    {
        if (remainingTime > 0)
        {
            if (IsEnemyInPoint == true)
            {
                remainingTime = remainingTime - Time.deltaTime;
                int minutes = Mathf.FloorToInt(remainingTime / 60);
                int seconds = Mathf.FloorToInt(remainingTime % 60);
                capturePointTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }

            if (IsEnemyInPoint == false)
            {
                remainingTime = remainingTime + 0;
            }
        }

        if(remainingTime < 0)
        {
            remainingTime = 0;
            capturePointTimerText.text = string.Format("{0:00}:{1:00}", "00", "00");
        }
    }
}
