using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinConditionLevel3 : MonoBehaviour
{
    public EnemySpawnControllerLevel3 enemy;
    public CapturePointByEnemy enemyCapturePoint;
    public GameObject LostUI;
    public GameObject WinUI;
    private bool isLose;
    [SerializeField] private SaveGame saveGame;
    private bool isGameSave;
    [SerializeField] private ChangeMusic changeMusic;
    [SerializeField] private SummariseGatheredResources summariseResources;

    private void Awake()
    {
        isLose = false;
        isGameSave = false;
    }
    private void Update()
    {
        if (enemy.globalWaveTime > 607) // if after all the enemy has spawned 
        {
            if (GameObject.FindGameObjectWithTag("OttomanRecruit") == null
                && GameObject.FindGameObjectWithTag("OttomanGunnerRecruit") == null
                && GameObject.FindGameObjectWithTag("RangedJanissary") == null
                && GameObject.FindGameObjectWithTag("OttomanCannon") == null
                && isLose == false) // check if there is no any enemy in the game alive so player win the game
            {
                WinUI.SetActive(true);

                if (isGameSave == false)
                {
                    saveGame.SaveLevel3();
                    changeMusic.TransitionMusicToVictoryTheme();
                    summariseResources.SummariseResourcesAfterWin();
                    isGameSave = true;
                }
            }
        }

        if (enemyCapturePoint.remainingTime <= 0 && isLose == false)
        {
            LostUI.SetActive(true);
            changeMusic.TransitionMusicToLoseTheme();
            summariseResources.SummariseResourcesAfterLose();
            isLose = true;
        }
    }
}
