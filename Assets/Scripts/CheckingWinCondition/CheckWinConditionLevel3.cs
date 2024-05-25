using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinConditionLevel3 : MonoBehaviour
{
    public EnemySpawnControllerLevel3 enemy;
    public CapturePointByEnemy enemyCapturePoint;
    public GameObject LostUI;
    public GameObject WinUI;
    public bool resultDecistion = false;
    private void Update()
    {
        if (enemy.globalWaveTime > 605) // if after all the enemy has spawned 
        {
            if (GameObject.FindGameObjectWithTag("OttomanRecruit") == null
                && GameObject.FindGameObjectWithTag("OttomanGunnerRecruit") == null
                && GameObject.FindGameObjectWithTag("RangedJanissary") == null
                && GameObject.FindGameObjectWithTag("OttomanCannon") == null) // check if there is no any enemy in the game alive so player win the game
            {
                WinUI.SetActive(true);
            }
        }

        if (enemyCapturePoint.remainingTime <= 0 && resultDecistion == false)
        {
            LostUI.SetActive(true);
        }
    }
}
