using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class TownCenter_ProduceVillager : MonoBehaviour
{
    [Header("Required Component")]
    public ResourcesStatus resourcesStatus;
    public UpgradeStatus upgradeStatus;
    public UnitDatabaseSO unitDatabase;
    public ClickToShowOBJInfo clickToShowOBJInfo;
    public TownCenterUpgradeDatabase townCenterUpgradeDatabase;
    public BuildingFunctionNotify buildingFunctionNotify;
    public HouseList population;
    public PositionToSpawnUnit positionToSpawnUnit;

    [Header("Ques System")]
    public float lastTrainingTime = 0f;
    private int villagerNumber = 0;
    private string uniqueKey;
    public Vector3 positionToSpawn;
    private GameObject clickToShowOBJInfoAssign;
    private GameObject upgradeStatusAssign;
    private GameObject resourcesStatusAssign;
    public Dictionary<string, GameObject> positionsToSpawn = new Dictionary<string, GameObject>();
    public List<string> uniqueKeys;
    public List<GameObject> villagersQue;

    [Header("Que UI")]
    public GameObject queIcon;
    public Transform queIconInstantiateTarget;
    public List<GameObject> queIconList;
    public Image queProgress;
    private float elapsedTime = 0f;

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
        buildingFunctionNotify = GameObject.FindGameObjectWithTag("Notification").GetComponent<BuildingFunctionNotify>();

        //villagerQueCanvas.transform.position = positionOfQueUI.transform.position;
    }

    public void AddVillagerQue() //if click button to train villager
    {
        if (resourcesStatus.food_Amount >= 50
            && population.currentPopulation + unitDatabase.unitDetails[0].population <= population.currentPopulationCapacity
            && queIconList.Count <= 9) // if resources is more than 50 , you can train villager and que is less or equal 10
        {
            GameObject newPrefabVillager = unitDatabase.unitDetails[0].unitPrefab;

            uniqueKey = Guid.NewGuid().ToString();
            //positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedObjectPosition);
            positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedGameObj);
            villagersQue.Add(newPrefabVillager);
            uniqueKeys.Add(uniqueKey);
            AddQueIcon(); //Add Que Icon
            population.PopulationChanges(unitDatabase.unitDetails[0].population);

            //villagers.Add(unitDatabase.unitDetails[0].unitPrefab); // add villager to que (reference object from database)
            resourcesStatus.food_Amount = resourcesStatus.food_Amount - unitDatabase.unitDetails[0].foodCost;
            resourcesStatus.food_Text.text = resourcesStatus.food_Amount.ToString();
            // positionToSpawn = clickToShowOBJInfo.selectedObjectPosition;
        }
        else if(resourcesStatus.food_Amount < 50)
        {
            //Noity Not Enough Resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }
        else if(population.currentPopulation + unitDatabase.unitDetails[0].population >= population.currentPopulationCapacity)
        {
            //Need more houses
            buildingFunctionNotify.NotifyNeedMoreHouses();
        }
    }

    public void AddQueIcon()
    {
        GameObject createQueIcon = Instantiate(queIcon, queIconInstantiateTarget); //instantiate que icon
        queIconList.Add(createQueIcon); //Add que icon into list
    }

    public void RemoveQueIcon()
    {
        Destroy(queIconList[0]);
        queIconList.RemoveAt(0);
    }

    private void Update()
    {
        if(queIconInstantiateTarget.gameObject.activeSelf == false || villagersQue.Count == 0) 
        { 
            queProgress.gameObject.SetActive(false);
        }
        
        if(queIconInstantiateTarget.gameObject.activeSelf == true && villagersQue.Count > 0)
        {
            queProgress.gameObject.SetActive(true);
        }

        if(villagersQue.Count > 0 )
        {
            float traningTime = unitDatabase.unitDetails[0].trainingTime;

            if(upgradeStatus.isTownCenterUpgrade == true)
            {
                traningTime = traningTime - townCenterUpgradeDatabase.townCenterUpgrades[0].reduceTrainingTime;
            }
            
            if(population.currentPopulation <= population.currentPopulationCapacity)
            {

                elapsedTime += Time.deltaTime;

                if (elapsedTime > traningTime)
                {
                    GameObject villagerPrefab = villagersQue[villagersQue.Count - 1];
                    // Check if the prefab has a stored spawn position
                    if (positionsToSpawn.ContainsKey(uniqueKeys[villagerNumber]))
                    {
                        Vector3 spawnPosition = positionsToSpawn[uniqueKeys[villagerNumber]].transform.position;
                        positionToSpawnUnit.roundNumber++;
                        Instantiate(villagerPrefab, spawnPosition, Quaternion.identity);
                        villagersQue.Remove(villagerPrefab);

                        RemoveQueIcon();
                        queProgress.fillAmount = 1f;
                        elapsedTime = 0;

                        lastTrainingTime = Time.time;

                        // Remove the stored spawn position for this prefab
                        positionsToSpawn.Remove(uniqueKeys[villagerNumber]);
                        uniqueKeys.Remove(uniqueKeys[villagerNumber]);
                        //population.PopulationChanges(unitDatabase.unitDetails[0].population);
                    }

                    else
                    {
                        villagerNumber++;
                    }
                }

                else
                {
                    //elapsedTime = elapsedTime + Time.deltaTime;
                    queProgress.fillAmount = Mathf.Clamp01(1f - (elapsedTime / traningTime));
                }
            }
        }

        else
        {
            //lastTrainingTime = Time.time - elapsedTime;
            queProgress.fillAmount = 0f;
        }
    }
}
