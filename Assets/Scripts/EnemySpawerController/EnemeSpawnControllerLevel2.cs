using Mono.Cecil.Cil;
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

    public WaveTimerLevel2 waveTimer;
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
        if (IsWaveBegin == true)
        {
            globalWaveTime = globalWaveTime + Time.deltaTime;

            #region Wave 1

            //Sub Wave 1-1//
            if (Mathf.FloorToInt(globalWaveTime) == 0)
            {
                if (IsSubWaveFinised == false)
                {
                    timeInWave = 60;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 1;

                    // Melee Janissary x2
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////////

            //Sub Wave 1-2
            else if (Mathf.FloorToInt(globalWaveTime) == 30)
            {
                if (IsSubWaveFinised == true)
                {
                    //Melee Janissary x2
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            ////////////// End Wave 1
            #endregion

            #region Wave 2

            //Sub Wave 2-1
            else if (Mathf.FloorToInt(globalWaveTime) == 60)
            {
                if (IsSubWaveFinised == false)
                {
                    timeInWave = 90;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 2;

                    //Melee Janissary x2
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////////

            ///Sub Wave 2-2
            else if (Mathf.FloorToInt(globalWaveTime) == 90)
            {
                if (IsSubWaveFinised == true)
                {
                    //Melee Janissary x2
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //////////////

            ///Sub Wave 2-3
            else if (Mathf.FloorToInt(globalWaveTime) == 120)
            {
                if (IsSubWaveFinised == false)
                {
                    //Melee Janissary x4
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint4.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            ////////////// End Wave 2
            #endregion

            #region Wave 3

            //Sub Wave 3-1
            else if (Mathf.FloorToInt(globalWaveTime) == 150)
            {
                if (IsSubWaveFinised == true)
                {
                    timeInWave = 90;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 3;

                    //Ranged Janissary x2
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //////////////

            // Sub Wave 3-2
            else if (Mathf.FloorToInt(globalWaveTime) == 180)
            {
                if (IsSubWaveFinised == false)
                {
                    //Ranged Janissary x1
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////////

            // Sub Wave 2-3
            else if (Mathf.FloorToInt(globalWaveTime) == 195)
            {
                if (IsSubWaveFinised == true)
                {
                    //Ranged Janissary x1
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //////////////

            // Sub Wave 3-4
            else if (Mathf.FloorToInt(globalWaveTime) == 215)
            {
                if (IsSubWaveFinised == false)
                {
                    //Ranged Janissary x2
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            ////////////// End Wave 3
            #endregion

            #region Wave 4

            //Sub Wave 4-1
            else if (Mathf.FloorToInt(globalWaveTime) == 240)
            {
                if (IsSubWaveFinised == true)
                {
                    timeInWave = 90;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 4;

                    //Melee Janissary x2
                    //Ranged Janissary x1
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            /////////////

            // Sub Wave 4-2
            else if (Mathf.FloorToInt(globalWaveTime) == 285)
            {
                if (IsSubWaveFinised == false)
                {
                    //Melee Janissary x2
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            /////////////

            // Sub Wave 4-3
            else if (Mathf.FloorToInt(globalWaveTime) == 305)
            {
                if (IsSubWaveFinised == true)
                {
                    //Melee Janissary x1
                    //Ranged Janissary x1
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            ///////////// End Wave 4
            #endregion

            #region Wave 5 (Final)

            // Sub Wave 5-1
            else if (Mathf.FloorToInt(globalWaveTime) == 330)
            {
                if (IsSubWaveFinised == false)
                {
                    timeInWave = 70;
                    waveNumber = 5;
                    waveTimer.remainingTime = timeInWave;
                    
                    //Melee Janissary x2
                    //Ranged Janissary x1
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            /////////////

            // Sub Wave 5-2
            else if (Mathf.FloorToInt(globalWaveTime) == 375)
            {
                if (IsSubWaveFinised == true)
                {
                    //Melee Janissary x2
                    //Ranged Janissary x1
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            /////////////

            // Sub Wave 5-3
            else if (Mathf.FloorToInt(globalWaveTime) == 400)
            {
                if (IsSubWaveFinised == false)
                {
                    //Melee Janissary x2
                    //Ranged Janissary x3
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            ///////////// End Wave 5
            #endregion
        }
    }
}
