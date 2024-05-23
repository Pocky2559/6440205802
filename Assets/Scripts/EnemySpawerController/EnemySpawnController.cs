using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour 
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
    private void Update()
    {
        if (IsWaveBegin == true) // Wave Time run out
        {
            //===============================================
            //Start count global wave time
            //===============================================
            globalWaveTime = globalWaveTime + Time.deltaTime;
            //-----------------------------------------------

             #region Wave 1
                        
             //=============
             //Sub-Wave 1-1
             //=============
             if (Mathf.FloorToInt(globalWaveTime) == 0)
             {
                if(IsSubWaveFinised == false)
                {
                  timeInWave = 60;
                  waveTimer.remainingTime = timeInWave;
                  waveNumber = 1;

                  // Ottoman Recruit x3
                  Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                  Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                  Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                  IsSubWaveFinised = true;
                }
             }
            //------------- End sub Wave 1 if global time = 30 seconds

            //=============
            //Sub-Wave 1-2
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 30) 
            {
                if (IsSubWaveFinised == true)
                {
                   // Ottoman Recruit x2
                   Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                   Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                   IsSubWaveFinised = false;
                }
            }
            //------------- End Wave 1 if global time = 60 seconds
            #endregion

             #region Wave 2

            //=============
            //Sub-Wave 2-1
            //=============
            if (Mathf.FloorToInt(globalWaveTime) == 60)
            {
                if(IsSubWaveFinised == false)
                {
                    timeInWave = 90;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 2;

                    //Ottoman Recruit x3
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 2-2
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 75)
            {
                if (IsSubWaveFinised == true)
                {
                    //Ottoman Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 2-3
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 90)
            {
                if (IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 2-4
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 120)
            {
                if (IsSubWaveFinised == true)
                {
                    //Ottoman Recruit x3
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 2-5
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 135)
            {
                if (IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //---------------------------------------
            #endregion

             #region Wave 3

            //=============
            //Sub-Wave 3-1
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 150)
            {
                timeInWave = 90;
                waveTimer.remainingTime = timeInWave;
                waveNumber = 3;

                if (IsSubWaveFinised == true)
                {
                    //Ottoman Recruit x4
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint4.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 3-2
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 170)
            {
                if (IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x2
                    //Ottoman Gunner Recruit x3
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 3-3
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 200)
            {
                if (IsSubWaveFinised == true)
                {
                    //Ottoman Gunner Recruit x3
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 3-4
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 225)
            {
                if (IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //---------------------------------------
            #endregion

             #region Wave 4

            //=============
            //Sub-Wave 4-1
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 240)
            {
                timeInWave = 90;
                waveTimer.remainingTime = timeInWave;
                waveNumber = 4;

                if (IsSubWaveFinised == true)
                {
                    //Ottoman Recruit x5
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint4.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 4-2
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 255)
            {
                if (IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x3
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 4-3
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 280)
            {
                if (IsSubWaveFinised == true)
                {
                    //Ottoman Gunner Recruit x3
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 4-4
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 310)
            {
                if (IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x3
                    //Ottoman Gunner Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //---------------------------------------
            #endregion

             #region Wave 5 (Final Wave)

            //=============
            //Sub-Wave 5-1
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 330)
            {
                if (IsSubWaveFinised == true)
                {
                    timeInWave = 60;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 5;

                    //Ottoman Recruit x4
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint4.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 5-2
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 345)
            {
                if (IsSubWaveFinised == false)
                {
                    //Ottoman Gunner Recruit x3
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 5-3
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 370)
            {
                if (IsSubWaveFinised == true)
                {
                    //Ottoman Recruit x3
                    //Ottoman Gunner Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //---------------------------------------

            //=============
            //Sub-Wave 5-4
            //=============
            else if (Mathf.FloorToInt(globalWaveTime) == 390)
            {
                if (IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x3
                    //Ottoman Gunner Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //---------------------------------------
            #endregion
        }
    }
}
