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
    [SerializeField] private SaveGame saveGame;
    private bool isGameSave;
    [SerializeField] private ChangeMusic changeMusic;

    private void Awake()
    {
        isLose = false;
        isGameSave = false;
    }

    private void Update() 
    {
        if (enemy.globalWaveTime > 392) // if after all the enemy has spawned 
        {
            if (GameObject.FindGameObjectWithTag("OttomanRecruit") == null
                && GameObject.FindGameObjectWithTag("OttomanGunnerRecruit") == null
                && isLose == false) // check if there is no any enemy in the game alive so player win the game
            {
                WinUI.SetActive(true);
               

                if(isGameSave == false)
                {
                    saveGame.SaveLevel1(); 
                    changeMusic.TransitionMusicToVictoryTheme();
                    isGameSave = true;
                }
            }
        }

        if (enemyCapturePoint.remainingTime <= 0 && isLose == false)
        {
            LostUI.SetActive(true);
            changeMusic.TransitionMusicToLoseTheme();
            isLose = true;
        }

    }
}
