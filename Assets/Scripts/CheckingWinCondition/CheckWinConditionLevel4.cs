using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinConditionLevel4 : MonoBehaviour
{
    public EnemySpawnControllerLevel4 enemy;
    public CapturePointByEnemy enemyCapturePoint;
    public GameObject LostUI;
    public GameObject WinUI;
    public bool resultDecistion = false;
    public GameObject palisadeDoor;
    private void Update()
    {
        if (enemy.globalWaveTime > 730) // if after all the enemy has spawned 
        {
            if (enemy.isExtraWaveBegin == false
                && GameObject.FindGameObjectWithTag("OttomanRecruit") == null
                && GameObject.FindGameObjectWithTag("OttomanGunnerRecruit") == null
                && GameObject.FindGameObjectWithTag("MeleeJanissary") == null
                && GameObject.FindGameObjectWithTag("RangedJanissary") == null
                && GameObject.FindGameObjectWithTag("OttomanCannon") == null
               ) // check if there is no any enemy in the game alive so player win the game
            {
                enemy.isExtraWaveBegin = true;

                if(palisadeDoor != null) // if door is still there
                {
                    palisadeDoor.tag = "Untagged"; // Change tag of the door to make enemy ignore the door
                }
                
                enemy.BeginExtraWave();
            }

            if (enemy.isExtraWaveBegin == true
                && GameObject.FindGameObjectWithTag("OttomanRecruit") == null
                && GameObject.FindGameObjectWithTag("OttomanGunnerRecruit") == null
                && GameObject.FindGameObjectWithTag("MeleeJanissary") == null
                && GameObject.FindGameObjectWithTag("RangedJanissary") == null
                && GameObject.FindGameObjectWithTag("OttomanCannon") == null) //if can eliminate all enemy in extra wave
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
