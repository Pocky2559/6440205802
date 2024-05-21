using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeSpawnControllerLevel2 : MonoBehaviour
{
    public GameObject EnemySpawnPoint1;
    public GameObject EnemySpawnPoint2;
    public GameObject EnemySpawnPoint3;
    public GameObject EnemySpawnPoint4;
    public GameObject EnemySpawnPoint5;
    public GameObject EnemySpawnPoint6;
    public GameObject EnemySpawnPoint7;
    public GameObject EnemySpawnPoint8;

    public UnitDatabaseSO unitDatabase;

    public WaveTimer waveTimer;
    public int waveNumber;
    public int maximumWave;
    public float timeInWave;
    public float globalWaveTime;

    private bool IsSubWaveFinised;
    public bool IsWaveBegin;

    private void Awake()
    {
        //timeInWave = 300; //time before first wave
        globalWaveTime = 0;
        maximumWave = 5;
        IsSubWaveFinised = false;
        IsWaveBegin = false;
    }
}
