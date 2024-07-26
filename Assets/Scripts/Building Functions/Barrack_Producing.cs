using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrack_Producing : MonoBehaviour // Attach this script to one game object
{
    [Header("Required Component")]
    public UnitDatabaseSO unitDatabase;
    public ClickToShowOBJInfo clickToShowOBJInfo;
    public ResourcesStatus resourcesStatus;
    public HouseList population;
    public PositionToSpawnUnit positionToSpawnUnit;
    [SerializeField] private BuildingFunctionNotify buildingFunctionNotify;

    [Header("Ques System")]
    public List<GameObject> militaryQue;
    public List<string> uniqueKeys;
    public int troopNumber = 0;
    public string uniqueKey;
    public Dictionary<string, GameObject> positionsToSpawn = new Dictionary<string, GameObject>();
    private GameObject clickToShowOBJInfoAssign;
    private GameObject resourcesStatusAssign;

    [Header("Training Time")]
    public float lastTrainingTime = 0.0f;

    [Header("Que UI")]
    public GameObject gunnerQueIcon;
    public GameObject landskQueIcon;
    public GameObject captainQueIcon;
    public Transform queIconInstantiateTarget;
    public List<GameObject> queIconList;
    public Image queProgress;
    private float elapsedTime = 0f;

    private void Awake()
    {
        // creat game object that contain component
        clickToShowOBJInfoAssign = GameObject.FindGameObjectWithTag("ClickToShowOBJInfo");
        resourcesStatusAssign = GameObject.FindGameObjectWithTag("ResourcesStatus");

        // assign component
        clickToShowOBJInfo = clickToShowOBJInfoAssign.GetComponent<ClickToShowOBJInfo>();
        resourcesStatus = resourcesStatusAssign.GetComponent<ResourcesStatus>();
        population = GameObject.FindGameObjectWithTag("PopulationController").GetComponent<HouseList>();
        positionToSpawnUnit = GetComponentInChildren<PositionToSpawnUnit>();
        buildingFunctionNotify = GameObject.FindGameObjectWithTag("Notification").GetComponent<BuildingFunctionNotify>();
    }
    public void AddQueIcon(string unitName)
    {
        if(unitName == "Gunner")
        {
            GameObject createQueIcon = Instantiate(gunnerQueIcon, queIconInstantiateTarget); //instantiate que icon
            queIconList.Add(createQueIcon); //Add que icon into list
        }

        if(unitName == "Landsknecht")
        {
            GameObject createQueIcon = Instantiate(landskQueIcon, queIconInstantiateTarget); //instantiate que icon
            queIconList.Add(createQueIcon); //Add que icon into list
        }

        if(unitName == "Captain")
        {
            GameObject createQueIcon = Instantiate(captainQueIcon, queIconInstantiateTarget); //instantiate que icon
            queIconList.Add(createQueIcon); //Add que icon into list
        }
    }

    public void RemoveIcon()
    {
        Destroy(queIconList[0]);
        queIconList.RemoveAt(0);
    }

    public void AddGunnerQue()
    {
        if(resourcesStatus.food_Amount >= unitDatabase.unitDetails[1].foodCost
           && resourcesStatus.gold_Amount >= unitDatabase.unitDetails[1].goldCost
           && population.currentPopulation + unitDatabase.unitDetails[1].population <= population.currentPopulationCapacity
           && queIconList.Count <= 9)
        {
            militaryQue.Add(unitDatabase.unitDetails[1].unitPrefab);
            uniqueKey = Guid.NewGuid().ToString();
            positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedGameObj);
            uniqueKeys.Add(uniqueKey);
            population.PopulationChanges(unitDatabase.unitDetails[1].population);

            AddQueIcon("Gunner");

            resourcesStatus.food_Amount = resourcesStatus.food_Amount - unitDatabase.unitDetails[1].foodCost;
            resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - unitDatabase.unitDetails[1].goldCost;
            resourcesStatus.food_Text.text = resourcesStatus.food_Amount.ToString();
            resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
        }

        else if (resourcesStatus.food_Amount < unitDatabase.unitDetails[1].foodCost
                 || resourcesStatus.gold_Amount < unitDatabase.unitDetails[1].goldCost) //if not enough resources
        {
            //Notify Not Enough Resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }

        else if (population.currentPopulation + unitDatabase.unitDetails[1].population > population.currentPopulationCapacity) //if not enough population capacity
        {
            //Noity Need More Houses
            buildingFunctionNotify.NotifyNeedMoreHouses();
        }
    }

    public void AddLandsknetchQue()
    {
        if (resourcesStatus.food_Amount >= unitDatabase.unitDetails[2].foodCost
            && resourcesStatus.gold_Amount >= unitDatabase.unitDetails[2].goldCost
            && population.currentPopulation + unitDatabase.unitDetails[2].population <= population.currentPopulationCapacity
            && queIconList.Count <= 9)
        {
            militaryQue.Add(unitDatabase.unitDetails[2].unitPrefab);
            uniqueKey = Guid.NewGuid().ToString();          
            positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedGameObj);
            uniqueKeys.Add(uniqueKey);
            population.PopulationChanges(unitDatabase.unitDetails[2].population);

            AddQueIcon("Landsknecht");

            resourcesStatus.food_Amount = resourcesStatus.food_Amount - unitDatabase.unitDetails[2].foodCost;
            resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - unitDatabase.unitDetails[2].goldCost;
            resourcesStatus.food_Text.text = resourcesStatus.food_Amount.ToString();
            resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
        }

        else if (resourcesStatus.food_Amount < unitDatabase.unitDetails[2].foodCost
                || resourcesStatus.gold_Amount < unitDatabase.unitDetails[2].goldCost)
        {
            //Noity Not Enough Resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }

        else if (population.currentPopulation + unitDatabase.unitDetails[2].population > population.currentPopulationCapacity)
        {
            //Notify Need More Houses
            buildingFunctionNotify.NotifyNeedMoreHouses();
        }
    }

    public void AddCaptainQue()
    {
        if (resourcesStatus.food_Amount >= unitDatabase.unitDetails[3].foodCost
            && resourcesStatus.gold_Amount >= unitDatabase.unitDetails[3].goldCost
            && population.currentPopulation + unitDatabase.unitDetails[3].population <= population.currentPopulationCapacity
            && queIconList.Count <= 9)
        {
            militaryQue.Add(unitDatabase.unitDetails[3].unitPrefab);
            uniqueKey = Guid.NewGuid().ToString();          
            positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedGameObj);
            uniqueKeys.Add(uniqueKey);
            population.PopulationChanges(unitDatabase.unitDetails[3].population);

            AddQueIcon("Captain");

            resourcesStatus.food_Amount = resourcesStatus.food_Amount - unitDatabase.unitDetails[3].foodCost;
            resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - unitDatabase.unitDetails[3].goldCost;
            resourcesStatus.food_Text.text = resourcesStatus.food_Amount.ToString();
            resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
        }

        else if (resourcesStatus.food_Amount < unitDatabase.unitDetails[3].foodCost
                || resourcesStatus.gold_Amount < unitDatabase.unitDetails[3].goldCost)
        {
            //Notify Not Enough Resources
            buildingFunctionNotify.NotifyNotEnoughResources();
        }

        else if (population.currentPopulation + unitDatabase.unitDetails[3].population > population.currentPopulationCapacity)
        {
            //Notify Need More Houses
            buildingFunctionNotify.NotifyNeedMoreHouses();
        }
    }

    public void Update()
    {
        if (queIconInstantiateTarget.gameObject.activeSelf == false || militaryQue.Count == 0)
        {
            queProgress.gameObject.SetActive(false);
        }

        if (queIconInstantiateTarget.gameObject.activeSelf == true && militaryQue.Count > 0)
        {
            queProgress.gameObject.SetActive(true);
        }

        if (militaryQue.Count > 0)
        {
            if (population.currentPopulation <= population.currentPopulationCapacity)
            {
                // if training Gunner
                if (militaryQue[0].name == unitDatabase.unitDetails[1].unitPrefab.name)
                {
                    TrainTroop(unitDatabase.unitDetails[1].trainingTime);
                }

                // if training Landsknecht
                else if (militaryQue[0].name == unitDatabase.unitDetails[2].unitPrefab.name)
                {
                    TrainTroop(unitDatabase.unitDetails[2].trainingTime);
                }

                //if training Captain
                else if (militaryQue[0].name == unitDatabase.unitDetails[3].unitPrefab.name)
                {
                    TrainTroop(unitDatabase.unitDetails[3].trainingTime);
                }
            }           
        }

        else
        {
            queProgress.fillAmount = 0f;
        }
    }

    private void TrainTroop(float unitTrainingTime)
    {
        float trainingTime = unitTrainingTime;

        elapsedTime += Time.deltaTime;

        if (elapsedTime > trainingTime)
        {
            if (positionsToSpawn.ContainsKey(uniqueKeys[troopNumber]))
            {
                Vector3 spawnPosition = positionsToSpawn[uniqueKeys[troopNumber]].transform.position;
                positionToSpawnUnit.roundNumber++;
                Instantiate(militaryQue[troopNumber], spawnPosition, Quaternion.identity);
                militaryQue.Remove(militaryQue[troopNumber]);
                positionsToSpawn.Remove(uniqueKeys[troopNumber]);
                uniqueKeys.Remove(uniqueKeys[troopNumber]);

                RemoveIcon();
                elapsedTime = 0;
                queProgress.fillAmount = 1;
            }
        }
        else
        {
            queProgress.fillAmount = Mathf.Clamp01(1f - (elapsedTime / trainingTime));
        }
    }
}
