using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;


public class TownCenter_ProduceVillager : MonoBehaviour
{
    public float lastTrainingTime = 0f;
    private int villagerNumber = 0;
    private string uniqueKey;
    public Vector3 positionToSpawn;
    private GameObject clickToShowOBJInfoAssign;
    private GameObject upgradeStatusAssign;
    private GameObject resourcesStatusAssign;
  
    public ResourcesStatus resourcesStatus;
    public UpgradeStatus upgradeStatus;
    public UnitDatabaseSO unitDatabase;
    public ClickToShowOBJInfo clickToShowOBJInfo;
    public TownCenterUpgradeDatabase townCenterUpgradeDatabase;

    // public Dictionary<string, Vector3> positionsToSpawn = new Dictionary<string, Vector3>();
    public Dictionary<string, GameObject> positionsToSpawn = new Dictionary<string, GameObject>();
    public List<string> uniqueKeys;
    public List<GameObject> villagersQue;
    public HouseList population;
    public PositionToSpawnUnit positionToSpawnUnit;

    public void Awake()
    {
        // creat game object that contain component
        clickToShowOBJInfoAssign = GameObject.FindGameObjectWithTag("ClickToShowOBJInfo");
        upgradeStatusAssign = GameObject.FindGameObjectWithTag("UpgradeStatus");
        resourcesStatusAssign = GameObject.FindGameObjectWithTag("ResourcesStatus");
        
        // assign component
        clickToShowOBJInfo = clickToShowOBJInfoAssign.GetComponent<ClickToShowOBJInfo>();
        upgradeStatus = upgradeStatusAssign.GetComponent<UpgradeStatus>();
        resourcesStatus= resourcesStatusAssign.GetComponent<ResourcesStatus>();
        population = GameObject.FindGameObjectWithTag("PopulationController").GetComponent<HouseList>();
        positionToSpawnUnit = GetComponentInChildren<PositionToSpawnUnit>();
    }
    public void AddVillagerQue() //if click button to train villager
    {
        if (resourcesStatus.food_Amount >= 50 && population.currentPopulation + unitDatabase.unitDetails[0].population <= population.currentHouseCapacity) // if resources is more than 50 , you can train villager
        {
            GameObject newPrefabVillager = unitDatabase.unitDetails[0].unitPrefab;

            uniqueKey = Guid.NewGuid().ToString();
            //positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedObjectPosition);
            positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedGameObj);
            villagersQue.Add(newPrefabVillager);
            uniqueKeys.Add(uniqueKey);

            //villagers.Add(unitDatabase.unitDetails[0].unitPrefab); // add villager to que (reference object from database)
            resourcesStatus.food_Amount = resourcesStatus.food_Amount - unitDatabase.unitDetails[0].foodCost;
            resourcesStatus.food_Text.text = resourcesStatus.food_Amount.ToString();
            // positionToSpawn = clickToShowOBJInfo.selectedObjectPosition;
        }
        else
        {
            Debug.Log("Not Enough Food");
        }
    }

    private void Update()
    {
        if(villagersQue.Count > 0)
        {
            if (upgradeStatus.isTownCenterUpgrade == true)
            {
                if (Time.time > lastTrainingTime + unitDatabase.unitDetails[0].trainingTime - townCenterUpgradeDatabase.townCenterUpgrades[0].reduceTrainingTime)
                {
                    GameObject villagerPrefab = villagersQue[villagersQue.Count - 1];
                    // Check if the prefab has a stored spawn position
                    if (positionsToSpawn.ContainsKey(uniqueKeys[villagerNumber]))
                    {
                        //Vector3 spawnPosition = positionsToSpawn[uniqueKeys[villagerNumber]];
                        Vector3 spawnPosition = positionsToSpawn[uniqueKeys[villagerNumber]].transform.position;
                        positionToSpawnUnit.roundNumber++;
                        Instantiate(villagerPrefab, spawnPosition, Quaternion.identity);
                        villagersQue.Remove(villagerPrefab);
                        lastTrainingTime = Time.time;

                        // Remove the stored spawn position for this prefab
                        positionsToSpawn.Remove(uniqueKeys[villagerNumber]);
                        uniqueKeys.Remove(uniqueKeys[villagerNumber]);
                        population.PopulationChanges(unitDatabase.unitDetails[0].population);
                    }
                    else
                    {
                        villagerNumber++;
                    }
                }
            }

            else
            {
                if (Time.time > lastTrainingTime + unitDatabase.unitDetails[0].trainingTime)
                {
                    GameObject villagerPrefab = villagersQue[villagersQue.Count - 1];
                    // Check if the prefab has a stored spawn position
                    if (positionsToSpawn.ContainsKey(uniqueKeys[villagerNumber]))
                    {
                        //Vector3 spawnPosition = positionsToSpawn[uniqueKeys[villagerNumber]];
                        Vector3 spawnPosition = positionsToSpawn[uniqueKeys[villagerNumber]].transform.position;
                        positionToSpawnUnit.roundNumber++;
                        Instantiate(villagerPrefab, spawnPosition, Quaternion.identity);
                        villagersQue.Remove(villagerPrefab);
                        lastTrainingTime = Time.time;

                        // Remove the stored spawn position for this prefab
                        positionsToSpawn.Remove(uniqueKeys[villagerNumber]);
                        uniqueKeys.Remove(uniqueKeys[villagerNumber]);
                        population.PopulationChanges(unitDatabase.unitDetails[0].population);
                    }
                    else
                    {
                        villagerNumber++;
                    }
                }
            }
        }
 
        else
        {
            lastTrainingTime = Time.time;
        }
    }
}
