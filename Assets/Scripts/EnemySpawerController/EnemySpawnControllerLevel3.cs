using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class EnemySpawnControllerLevel3 : MonoBehaviour
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
    [SerializeField] private SoundEffectController soundEffectController;

    public WaveTimerLevel3 waveTimer;
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
        maximumWave = 6;
        IsSubWaveFinised = false;
        IsWaveBegin = false;

        soundEffectController = GetComponent<SoundEffectController>();
        soundEffectController.soundEffect = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(IsWaveBegin == true)
        {
            globalWaveTime = globalWaveTime + Time.deltaTime;

            #region Wave 1
            
            // Sub Wave 1-1
            if(Mathf.FloorToInt(globalWaveTime) == 0)
            {
                if(IsSubWaveFinised == false)
                {
                    timeInWave = 60;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 1;
                    soundEffectController.PlayWaveBeginSound();

                    //Ottoman Recruit x4
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////

            // Sub Wave 1-2
            else if(Mathf.FloorToInt(globalWaveTime) == 30)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ottoman Recruit x3
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //////////

            // Sub Wave 1-3
            else if(Mathf.FloorToInt(globalWaveTime) == 45)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////
            #endregion

            #region Wave 2

            // Sub Wave 2-1
            else if(Mathf.FloorToInt(globalWaveTime) == 60)
            {
                if(IsSubWaveFinised == true)
                {
                    timeInWave = 90;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 2;
                    soundEffectController.PlayWaveBeginSound();

                    //Ottoman Recruit x3
                    //Ottoman Gunner Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint4.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //////////

            //Sub Wave 2-2
            else if(Mathf.FloorToInt(globalWaveTime) == 90)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////

            //Sub Wave 2-3
            else if(Mathf.FloorToInt(globalWaveTime) == 120)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ottoman Recruit x2
                    //Ottoman Gunner Recruit x1
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //////////

            // Sub Wave 2-4
            else if(Mathf.FloorToInt(globalWaveTime) == 135)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Gunner Recruit x1
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////
            #endregion

            #region Wave 3

            //Sub Wave 3-1
            else if(Mathf.FloorToInt(globalWaveTime) == 150)
            {
                if(IsSubWaveFinised == true)
                {
                    timeInWave = 120;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 3;
                    soundEffectController.PlayWaveBeginSound();

                    // Ranged Janissary x2
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //////////

            // Sub Wave 3-2
            else if(Mathf.FloorToInt(globalWaveTime) == 195)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ranged Janissary x2
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////

            // Sub Wave 3-3
            else if(Mathf.FloorToInt(globalWaveTime) == 220)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ranged Janissary x3
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //////////
            #endregion

            #region Wave 4

            //Sub Wave 4-1
            else if(Mathf.FloorToInt(globalWaveTime) == 270)
            {
                if(IsSubWaveFinised == false)
                {
                    timeInWave = 90;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 4;
                    soundEffectController.PlayWaveBeginSound();

                    //Ottoman Recruit x3
                    //Ranged Janissary x1
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////

            // Sub Wave 4-2
            else if(Mathf.FloorToInt(globalWaveTime) == 320)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ottoman Recruit x4
                    //Ranged Janissary x1
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint4.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //////////

            //Sub Wave 4-3
            else if(Mathf.FloorToInt(globalWaveTime) == 330)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////
            #endregion

            #region Wave 5

            // Sub Wave 5-1
            else if(Mathf.FloorToInt(globalWaveTime) == 360)
            {
                if(IsSubWaveFinised == true)
                {
                    timeInWave = 120;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 5;
                    soundEffectController.PlayWaveBeginSound();

                    //Ranged Janissary x2
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //////////

            // Sub Wave 5-2
            else if(Mathf.FloorToInt(globalWaveTime) == 420)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ranged Janissary x3
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////

            // Sub Wave 5-3
            else if(Mathf.FloorToInt(globalWaveTime) == 440)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ranged Janissary x2
                    //Ottoman Cannon x1
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //////////
            #endregion

            #region Wave 6

            // Sub Wave 6-1
            else if(Mathf.FloorToInt(globalWaveTime) == 480)
            {
                if(IsSubWaveFinised == false)
                {
                    timeInWave = 125;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 6;
                    soundEffectController.PlayWaveBeginSound();

                    //Ottoman Recruit x5
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////

            // Sub Wave 6-2
            else if(Mathf.FloorToInt(globalWaveTime) == 525)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ranged Janissary x3
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            //////////

            // Sub Wave 6-3
            else if(Mathf.FloorToInt(globalWaveTime) == 560)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x4
                    //Ranged Janissary x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint4.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint8.transform.position, Quaternion.identity);
                    IsSubWaveFinised = true;
                }
            }
            //////////
            ///
            // Sub Wave 6-4
            else if(Mathf.FloorToInt(globalWaveTime) == 605)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ottoman Recruit x3
                    //Ranged Janissary x1
                    //Ottoman Cannon x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint8.transform.position, Quaternion.identity);
                    IsSubWaveFinised = false;
                }
            }
            #endregion
        }
    }
}
