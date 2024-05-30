using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinCondition : MonoBehaviour //For Level 1
{
    public EnemySpawnController enemy;
    public CapturePointByEnemy enemyCapturePoint;
    public GameObject LostUI;
    public GameObject WinUI;
    private bool isLose;

    private void Awake()
    {
        isLose = false;
    }

    private void Update() 
    {
        if (enemy.globalWaveTime > 390) // if after all the enemy has spawned 
        {
            if (GameObject.FindGameObjectWithTag("OttomanRecruit") == null
                && GameObject.FindGameObjectWithTag("OttomanGunnerRecruit") == null
                && isLose == false) // check if there is no any enemy in the game alive so player win the game
            {
                WinUI.SetActive(true);
            }
        }

        if (enemyCapturePoint.remainingTime <= 0)
        {
            LostUI.SetActive(true);
            isLose = true;
        }

    }
}
