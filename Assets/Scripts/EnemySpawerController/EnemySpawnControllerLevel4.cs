using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnControllerLevel4 : MonoBehaviour
{
    public GameObject EnemySpawnPoint1;
    public GameObject EnemySpawnPoint2;
    public GameObject EnemySpawnPoint3;
    public GameObject EnemySpawnPoint4;
    public GameObject EnemySpawnPoint5;
    public GameObject EnemySpawnPoint6;
    public GameObject EnemySpawnPoint7;
    public GameObject EnemySpawnPoint8;

    public GameObject EnemySpawnPointExtra1;
    public GameObject EnemySpawnPointExtra2;
    public GameObject EnemySpawnPointExtra3;
    public GameObject EnemySpawnPointExtra4;
    public GameObject EnemySpawnPointExtra5;
    public GameObject EnemySpawnPointExtra6;

    public GameObject oldMedievalWall;

    public UnitDatabaseSO unitDatabase;
    [SerializeField] private SoundEffectController soundEffectController;

    public WaveTimerLevel4 waveTimer;
    public int waveNumber;
    public int maximumWave;
    public float timeInWave;
    public float globalWaveTime;

    private bool IsSubWaveFinised;
    public bool IsWaveBegin;
    public bool isExtraWaveBegin;

    [SerializeField] private OldMedievalWallDestroyed oldWallDestroyed;

    private void Awake()
    {
        //timeInWave = 300; //time before first wave
        globalWaveTime = 0;
        maximumWave = 7;
        IsSubWaveFinised = false;
        IsWaveBegin = false;
        //explodeParticle = GameObject.FindGameObjectWithTag("ParticleController").GetComponent<GateWallExplode>();

        soundEffectController = GetComponent<SoundEffectController>();
        soundEffectController.soundEffect = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(IsWaveBegin == true)
        {
            globalWaveTime = globalWaveTime + Time.deltaTime;

            #region Wave 1 

            //Sub Wave 1-1
            if(Mathf.FloorToInt(globalWaveTime) == 0)
            {
                if(IsSubWaveFinised == false)
                {
                    timeInWave = 60;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 1;
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
            ////////
            ///
            // Sub Wave 1-2
            else if(Mathf.FloorToInt(globalWaveTime) == 30)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ottoman Recruit x4
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint4.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            ////////
            ///
            // Sub Wave 1-3
            else if(Mathf.FloorToInt(globalWaveTime) == 45)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint8.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
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
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            ////////
            ///
            // Sub Wave 2-2
            else if(Mathf.FloorToInt(globalWaveTime) == 75)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Gunner Recruit x3
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
            ///
            // Sub Wave 2-3
            else if(Mathf.FloorToInt(globalWaveTime) == 105)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ottoman Recruit x3
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            ////////
            ///
            // Sub Wave 2-4
            else if(Mathf.FloorToInt(globalWaveTime) == 120)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Gunner Recruit x2
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
            #endregion

            #region Wave 3

            // Sub Wave 3-1
            else if(Mathf.FloorToInt(globalWaveTime) == 150)
            {
                if(IsSubWaveFinised == true)
                {
                    timeInWave = 120;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 3;
                    soundEffectController.PlayWaveBeginSound();

                    //Melee Janissary x3
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            ////////
            ///
            // Sub Wave 3-2
            else if(Mathf.FloorToInt(globalWaveTime) == 180)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Gunner Recruit x2
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
            ///
            // Sub Wave 3-3
            else if(Mathf.FloorToInt(globalWaveTime) == 210)
            {
                if(IsSubWaveFinised == true)
                {
                    //Melee Janissary x2
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            ////////
            ///
            // Sub Wave 3-4
            else if(Mathf.FloorToInt(globalWaveTime) == 240)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Gunner Recruit x3
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
            #endregion

            #region Wave 4

            // Sub Wave 4-1
            else if(Mathf.FloorToInt(globalWaveTime) == 270)
            {
                if(IsSubWaveFinised == true)
                {
                    timeInWave = 90;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 4;
                    soundEffectController.PlayWaveBeginSound();

                    //Ottoman Recruit x3
                    //Ranged Janissary x1
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint8.transform.position, Quaternion.identity);
                     
                    IsSubWaveFinised = false;
                }
            }
            ////////
            ///
            // Sub Wave 4-2
            else if(Mathf.FloorToInt(globalWaveTime) == 315)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x2
                    //Ranged Janissary x1
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
            ///
            // Sub Wave 4-3
            else if(Mathf.FloorToInt(globalWaveTime) == 335)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ottoman Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            ////////
            #endregion

            #region Wave 5

            // Sub Wave 5-1
            else if(Mathf.FloorToInt(globalWaveTime) == 360)
            {
                if(IsSubWaveFinised == false)
                {
                    timeInWave = 100;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 5;
                    soundEffectController.PlayWaveBeginSound();

                    //Melee Janissary x2
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint8.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
            ///
            // Sub Wave 5-2
            else if(Mathf.FloorToInt(globalWaveTime) == 390)
            {
                if(IsSubWaveFinised == true)
                {
                    // Ottoman Gunner Recruit x2
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint8.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            ////////
            ///
            // Sub Wave 5-3
            else if(Mathf.FloorToInt(globalWaveTime) == 410)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Gunner Recruit x2
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint8.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
            ///
            // Sub Wave 5-4
            else if(Mathf.FloorToInt(globalWaveTime) == 430)
            {
                if(IsSubWaveFinised == true)
                {
                    //Melee Janissary x2
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            ////////
            #endregion

            #region Wave 6

            // Sub Wave 6-1
            else if(Mathf.FloorToInt(globalWaveTime) == 460)
            {
                if (IsSubWaveFinised == false)
                {
                    timeInWave = 150;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 6;
                    soundEffectController.PlayWaveBeginSound();

                    //Ottoman Cannon x1
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
            ///
            // Sub Wave 6-2
            else if(Mathf.FloorToInt(globalWaveTime) == 475)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ranged Janissary x2
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint4.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            ////////
            ///
            // Sub Wave 6-3
            else if(Mathf.FloorToInt(globalWaveTime) == 490)
            {
                if(IsSubWaveFinised == false)
                {
                    //Melee Janissary x2
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint4.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
            ///
            // Sub Wave 6-4
            else if(Mathf.FloorToInt(globalWaveTime) == 520)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ranged Janissary x2
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            ////////
            ///
            // Sub Wave 6-5
            else if(Mathf.FloorToInt(globalWaveTime) == 560)
            {
                if(IsSubWaveFinised == false)
                {
                    //Melee Janissary x2
                    //Ranged Janissary x2
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint8.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
            #endregion

            #region Wave 7

            // Sub Wave 7-1
            else if(Mathf.FloorToInt(globalWaveTime) == 610)
            {
                if(IsSubWaveFinised == true)
                {
                    timeInWave = 120;
                    waveTimer.remainingTime = timeInWave;
                    waveNumber = 7;
                    soundEffectController.PlayWaveBeginSound();

                    //Ottoman Cannon x1
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            ////////
            ///
            // Sub Wave 7-2
            else if(Mathf.FloorToInt(globalWaveTime) == 625)
            {
                if(IsSubWaveFinised == false)
                {
                    //Ottoman Recruit x3
                    //Ottoman Gunner Recruit x2
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
            ///
            // Sub Wave 7-3
            else if(Mathf.FloorToInt(globalWaveTime) == 660)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ottoman Cannon x1
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint8.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            ////////
            ///
            // Sub Wave 7-4
            else if(Mathf.FloorToInt(globalWaveTime) == 675)
            {
                if(IsSubWaveFinised == false)
                {
                    //Melee Janissary x2
                    //Ranged Janissary x2
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPoint8.transform.position, Quaternion.identity);

                    IsSubWaveFinised = true;
                }
            }
            ////////
            ///
            // Sub Wave 7-5
            else if(Mathf.FloorToInt(globalWaveTime) == 730)
            {
                if(IsSubWaveFinised == true)
                {
                    //Ottoman Cannon x8
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint4.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint5.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[9].unitPrefab, EnemySpawnPoint8.transform.position, Quaternion.identity);

                    IsSubWaveFinised = false;
                }
            }
            #endregion
        }
    }

    public void BeginExtraWave()
    {
        #region Extra Wave
        if (isExtraWaveBegin == true)
        {
            soundEffectController.PlayWaveBeginSound();
            oldWallDestroyed.StartToDestroyOldWall();

            // Spawn Enemy
            // Melee Janissary x4
            // Ranged Janissary x2
            Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPointExtra1.transform.position, Quaternion.identity);
            Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPointExtra2.transform.position, Quaternion.identity);
            Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPointExtra3.transform.position, Quaternion.identity);
            Instantiate(unitDatabase.unitDetails[7].unitPrefab, EnemySpawnPointExtra4.transform.position, Quaternion.identity);
            Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPointExtra5.transform.position, Quaternion.identity);
            Instantiate(unitDatabase.unitDetails[8].unitPrefab, EnemySpawnPointExtra6.transform.position, Quaternion.identity);
        }
        #endregion
    }
}
