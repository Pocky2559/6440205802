using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack_Producing : MonoBehaviour // Attach this script to one game object
{
    public float lastTrainingTime = 0.0f;
    public UnitDatabaseSO unitDatabase;
    public ClickToShowOBJInfo clickToShowOBJInfo;

    public ResourcesStatus resourcesStatus;
    public List<GameObject> militaryQue;

    public string uniqueKey;
    public List<string> uniqueKeys;
    public int troopNumber = 0;
    public Dictionary<string, Vector3> positionsToSpawn = new Dictionary<string, Vector3>();
    private GameObject clickToShowOBJInfoAssign;
    private GameObject resourcesStatusAssign;

    private void Awake()
    {
        // creat game object that contain component
        clickToShowOBJInfoAssign = GameObject.FindGameObjectWithTag("ClickToShowOBJInfo");
        resourcesStatusAssign = GameObject.FindGameObjectWithTag("ResourcesStatus");

        // assign component
        clickToShowOBJInfo = clickToShowOBJInfoAssign.GetComponent<ClickToShowOBJInfo>();
        resourcesStatus = resourcesStatusAssign.GetComponent<ResourcesStatus>();
    }

    public void AddGunnerQue()
    {
        if(resourcesStatus.food_Amount >= 120 && resourcesStatus.gold_Amount >= 40)
        {
            militaryQue.Add(unitDatabase.unitDetails[1].unitPrefab);
            uniqueKey = Guid.NewGuid().ToString();
            positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedObjectPosition);
            uniqueKeys.Add(uniqueKey);

            resourcesStatus.food_Amount = resourcesStatus.food_Amount - unitDatabase.unitDetails[1].foodCost;
            resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - unitDatabase.unitDetails[1].goldCost;
            resourcesStatus.food_Text.text = resourcesStatus.food_Amount.ToString();
            resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
        }
    }

    public void AddLandsknetchQue()
    {
        if (resourcesStatus.food_Amount >= 100 && resourcesStatus.gold_Amount >= 20)
        {
            militaryQue.Add(unitDatabase.unitDetails[2].unitPrefab);
            uniqueKey = Guid.NewGuid().ToString();
            positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedObjectPosition);
            uniqueKeys.Add(uniqueKey);

            resourcesStatus.food_Amount = resourcesStatus.food_Amount - unitDatabase.unitDetails[1].foodCost;
            resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - unitDatabase.unitDetails[1].goldCost;
            resourcesStatus.food_Text.text = resourcesStatus.food_Amount.ToString();
            resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
        }
    }

    public void Update()
    {
        if(militaryQue.Count > 0)
        {
            if(Time.time > lastTrainingTime + unitDatabase.unitDetails[1].trainingTime)
            {
                if (positionsToSpawn.ContainsKey(uniqueKeys[troopNumber]))
                {
                    Vector3 spawnPosition = positionsToSpawn[uniqueKeys[troopNumber]];
                    Instantiate(militaryQue[troopNumber],spawnPosition,Quaternion.identity);
                    militaryQue.Remove(militaryQue[troopNumber]);
                    lastTrainingTime = Time.time;
                    positionsToSpawn.Remove(uniqueKeys[troopNumber]);
                    uniqueKeys.Remove(uniqueKeys[troopNumber]);
                }
            }
        }

        else
        {
            lastTrainingTime = Time.time;
        }
    }
}