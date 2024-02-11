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
    public float remainingTime;

    public bool IsWave1Begin;
    private bool isSpawnAt0Second;
    private bool isSpawnAt15Second;
    private bool isSpawnAt30Second;
    private bool isSpawnAt50Second;

    private void Awake()
    {
        isSpawnAt0Second = true;
        isSpawnAt15Second = true;
        isSpawnAt30Second = true;
        isSpawnAt50Second = true;
    }
    private void Update()
    {
        #region Spawning at 0 S
        if (waveTimer.remainingTime <= 0)
        {
            IsWave1Begin = true;

            if (IsWave1Begin == true)
            {
                remainingTime = remainingTime + Time.deltaTime;

                if (Mathf.FloorToInt(remainingTime) == 0 && isSpawnAt0Second == true)
                {
                    //Spawn Ottoman Recruit
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint3.transform.position, Quaternion.identity);
                    //

                    isSpawnAt0Second = false;
                }

                if (Mathf.FloorToInt(remainingTime) == 15 && isSpawnAt15Second == true)
                {
                    //Spwan Ottoman Recruit
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    //

                    //Spawn Ottoman Gunner Recruit
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    //

                    isSpawnAt15Second = false;
                }

                if (Mathf.FloorToInt(remainingTime) == 30 && isSpawnAt30Second == true)
                {
                    //Spawn Ottoman Gunner Recruit
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint6.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[6].unitPrefab, EnemySpawnPoint7.transform.position, Quaternion.identity);
                    //

                    isSpawnAt30Second = false;
                }

                if (Mathf.FloorToInt(remainingTime) == 50 && isSpawnAt50Second == true)
                {
                    //Spwan Ottoman Recruit
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint1.transform.position, Quaternion.identity);
                    Instantiate(unitDatabase.unitDetails[5].unitPrefab, EnemySpawnPoint2.transform.position, Quaternion.identity);
                    //

                    isSpawnAt50Second = false;
                }
            }
        }
        #endregion
    }
}
