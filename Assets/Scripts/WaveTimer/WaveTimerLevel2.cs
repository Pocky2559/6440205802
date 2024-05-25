using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveTimerLevel2 : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text waveNumber;
    public float remainingTime;
    public EnemeSpawnControllerLevel2 enemySpawnController;

    private void Start()
    {
        remainingTime = enemySpawnController.timeInWave;
    }
    void Update()
    {
        if (enemySpawnController.timeInWave > 0)
        {
            enemySpawnController.timeInWave -= Time.deltaTime;
            remainingTime = remainingTime - Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            waveNumber.text = string.Format("{0}/{1}", enemySpawnController.waveNumber, enemySpawnController.maximumWave);
        }

        else if (enemySpawnController.timeInWave < 0)
        {
            remainingTime = 0;
            timeText.text = string.Format("{0:00}:{1:00}", "00", "00");
            enemySpawnController.IsWaveBegin = true;
            waveNumber.text = string.Format("{0}/{1}", enemySpawnController.waveNumber, enemySpawnController.maximumWave);
        }
    }
}