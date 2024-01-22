using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter_ProduceVillager : MonoBehaviour
{

    public List<GameObject> villagers;
    public List<GameObject> militaryUnits;
    public float lastTrainingTime = 0f;
    public UnitDatabaseSO unitDatabase;
    public ClickToShowOBJInfo clickToShowOBJInfo;
    public ResourcesStatus resourcesStatus;
    public Vector3 positionToSpawn;

    public Dictionary<string, Vector3> positionsToSpawn = new Dictionary<string, Vector3>();
    public List<string> uniqueKeys;
    private string uniqueKey;
    private int i = 0;

    public void AddVillagerQue() //if click button to train villager
    {
        if (resourcesStatus.food_Amount >= 50) // if resources is more than 50 , you can train villager
        {
            GameObject newPrefabVillager = unitDatabase.unitDetails[0].unitPrefab;

            uniqueKey = Guid.NewGuid().ToString();
            positionsToSpawn.Add(uniqueKey, clickToShowOBJInfo.selectedObjectPosition);
            villagers.Add(newPrefabVillager);
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

    public void AddMilitaryUnitsQue(string unitName) // if click button to train military units
    {
        if (unitName == "Gunner")
        {
            militaryUnits.Add(gameObject); // add military units to que
        }

        if (unitName == "LandSknecht")
        {

        }
    }

    private void Update()
    {
        #region Villagers Que
        if (villagers.Count != 0)
        {
            if (Time.time > lastTrainingTime + unitDatabase.unitDetails[0].trainingTime)
            {
                GameObject villagerPrefab = villagers[villagers.Count - 1];
                // Check if the prefab has a stored spawn position
                if (positionsToSpawn.ContainsKey(uniqueKeys[i]))
                {
                    Vector3 spawnPosition = positionsToSpawn[uniqueKeys[i]];
                    Instantiate(villagerPrefab, spawnPosition, Quaternion.identity);
                    villagers.Remove(villagerPrefab);
                    lastTrainingTime = Time.time;

                    // Remove the stored spawn position for this prefab
                    positionsToSpawn.Remove(uniqueKeys[i]);
                    uniqueKeys.Remove(uniqueKeys[i]);
                }
                else
                {
                    i++;
                }
                /////
                /*Instantiate(villagers[villagers.Count - 1], clickToShowOBJInfo.selectedObjectPosition, Quaternion.identity);
                Instantiate(villagers[villagers.Count - 1], positionToSpawn, Quaternion.identity);
                villagers.Remove(villagers[villagers.Count - 1]);
                lastTrainingTime = Time.time;*/
            }
        }

        else
        {
            lastTrainingTime = Time.time;
        }

        #endregion
    }
}
