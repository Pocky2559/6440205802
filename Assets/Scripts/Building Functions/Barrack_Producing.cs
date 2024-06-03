using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //public Dictionary<string, Vector3> positionsToSpawn = new Dictionary<string, Vector3>();
    public Dictionary<string, GameObject> positionsToSpawn = new Dictionary<string, GameObject>();
    private GameObject clickToShowOBJInfoAssign;
    private GameObject resourcesStatusAssign;
    public HouseList population;
    public PositionToSpawnUnit positionToSpawnUnit;

    //Que UI
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
           && population.currentPopulation + unitDatabase.unitDetails[1].population <= population.currentPopulationCapacity)
        {
            militaryQue.Add(unitDatabase.unitDetails[1].unitPrefab);
            uniqueKey = Guid.NewGuid().ToString();
            //positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedObjectPosition);
            positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedGameObj);
            uniqueKeys.Add(uniqueKey);

            AddQueIcon("Gunner");

            resourcesStatus.food_Amount = resourcesStatus.food_Amount - unitDatabase.unitDetails[1].foodCost;
            resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - unitDatabase.unitDetails[1].goldCost;
            resourcesStatus.food_Text.text = resourcesStatus.food_Amount.ToString();
            resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
        }
    }

    

    public void AddLandsknetchQue()
    {
        if (resourcesStatus.food_Amount >= unitDatabase.unitDetails[2].foodCost
            && resourcesStatus.gold_Amount >= unitDatabase.unitDetails[2].goldCost
            && population.currentPopulation + unitDatabase.unitDetails[2].population <= population.currentPopulationCapacity)
        {
            militaryQue.Add(unitDatabase.unitDetails[2].unitPrefab);
            uniqueKey = Guid.NewGuid().ToString();
            // positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedObjectPosition);
            positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedGameObj);
            uniqueKeys.Add(uniqueKey);

            AddQueIcon("Landsknecht");

            resourcesStatus.food_Amount = resourcesStatus.food_Amount - unitDatabase.unitDetails[2].foodCost;
            resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - unitDatabase.unitDetails[2].goldCost;
            resourcesStatus.food_Text.text = resourcesStatus.food_Amount.ToString();
            resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
        }
    }

    public void AddCaptainQue()
    {
        if (resourcesStatus.food_Amount >= unitDatabase.unitDetails[3].foodCost
            && resourcesStatus.gold_Amount >= unitDatabase.unitDetails[3].goldCost
            && population.currentPopulation + unitDatabase.unitDetails[3].population <= population.currentPopulationCapacity)
        {
            militaryQue.Add(unitDatabase.unitDetails[3].unitPrefab);
            uniqueKey = Guid.NewGuid().ToString();
            //positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedObjectPosition);
            positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedGameObj);
            uniqueKeys.Add(uniqueKey);

            AddQueIcon("Captain");

            resourcesStatus.food_Amount = resourcesStatus.food_Amount - unitDatabase.unitDetails[3].foodCost;
            resourcesStatus.gold_Amount = resourcesStatus.gold_Amount - unitDatabase.unitDetails[3].goldCost;
            resourcesStatus.food_Text.text = resourcesStatus.food_Amount.ToString();
            resourcesStatus.gold_Text.text = resourcesStatus.gold_Amount.ToString();
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
            // if training Gunner
            if(militaryQue[0].name == unitDatabase.unitDetails[1].unitPrefab.name)
            {
                if (Time.time > lastTrainingTime + unitDatabase.unitDetails[1].trainingTime)
                {
                    if (positionsToSpawn.ContainsKey(uniqueKeys[troopNumber]))
                    {
                        //Vector3 spawnPosition = positionsToSpawn[uniqueKeys[troopNumber]];
                        Vector3 spawnPosition = positionsToSpawn[uniqueKeys[troopNumber]].transform.position;
                        positionToSpawnUnit.roundNumber++;
                        Instantiate(militaryQue[troopNumber], spawnPosition, Quaternion.identity);
                        militaryQue.Remove(militaryQue[troopNumber]);
                        lastTrainingTime = Time.time;
                        positionsToSpawn.Remove(uniqueKeys[troopNumber]);
                        uniqueKeys.Remove(uniqueKeys[troopNumber]);
                        population.PopulationChanges(unitDatabase.unitDetails[1].population);

                        RemoveIcon();
                        elapsedTime = 0;
                        queProgress.fillAmount = 1;
                    }
                }
                else
                {
                    elapsedTime = elapsedTime + Time.deltaTime;
                    queProgress.fillAmount = Mathf.Clamp01(1f - (elapsedTime / unitDatabase.unitDetails[1].trainingTime));
                }
            }

            // if training Landsknecht
            if (militaryQue[0].name == unitDatabase.unitDetails[2].unitPrefab.name)
            {
                if (Time.time > lastTrainingTime + unitDatabase.unitDetails[2].trainingTime)
                {
                    if (positionsToSpawn.ContainsKey(uniqueKeys[troopNumber]))
                    {
                        //Vector3 spawnPosition = positionsToSpawn[uniqueKeys[troopNumber]];
                        Vector3 spawnPosition = positionsToSpawn[uniqueKeys[troopNumber]].transform.position;
                        positionToSpawnUnit.roundNumber++;
                        Instantiate(militaryQue[troopNumber], spawnPosition, Quaternion.identity);
                        militaryQue.Remove(militaryQue[troopNumber]);
                        lastTrainingTime = Time.time;
                        positionsToSpawn.Remove(uniqueKeys[troopNumber]);
                        uniqueKeys.Remove(uniqueKeys[troopNumber]);
                        population.PopulationChanges(unitDatabase.unitDetails[2].population);

                        RemoveIcon();
                        elapsedTime = 0;
                        queProgress.fillAmount = 1;
                    }
                }
                else
                {
                    elapsedTime = elapsedTime + Time.deltaTime;
                    queProgress.fillAmount = Mathf.Clamp01(1f - (elapsedTime / unitDatabase.unitDetails[2].trainingTime));
                }
            }

            //if training Captain
            if (militaryQue[0].name == unitDatabase.unitDetails[3].unitPrefab.name)
            {
                if (Time.time > lastTrainingTime + unitDatabase.unitDetails[3].trainingTime)
                {
                    if (positionsToSpawn.ContainsKey(uniqueKeys[troopNumber]))
                    {
                        //Vector3 spawnPosition = positionsToSpawn[uniqueKeys[troopNumber]];
                        Vector3 spawnPosition = positionsToSpawn[uniqueKeys[troopNumber]].transform.position;
                        positionToSpawnUnit.roundNumber++;
                        Instantiate(militaryQue[troopNumber], spawnPosition, Quaternion.identity);
                        militaryQue.Remove(militaryQue[troopNumber]);
                        lastTrainingTime = Time.time;
                        positionsToSpawn.Remove(uniqueKeys[troopNumber]);
                        uniqueKeys.Remove(uniqueKeys[troopNumber]);
                        population.PopulationChanges(unitDatabase.unitDetails[3].population);

                        RemoveIcon();
                        elapsedTime = 0;
                        queProgress.fillAmount = 1;
                    }
                }
                else
                {
                    elapsedTime = elapsedTime + Time.deltaTime;
                    queProgress.fillAmount = Mathf.Clamp01(1f - (elapsedTime / unitDatabase.unitDetails[3].trainingTime));
                }
            }
        }

        else
        {
            lastTrainingTime = Time.time;
        }
    }
}
