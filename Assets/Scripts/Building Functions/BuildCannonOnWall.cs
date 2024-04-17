using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildCannonOnWall : MonoBehaviour
{
    public GameObject kartaune;
    public GameObject positionToPlace;
    public ResourcesStatus resourcesStatus;
    public UnitDatabaseSO unitDatabase;
    public HouseList population;

    public void BuildCannon()
    {
        if(resourcesStatus.food_Amount >= unitDatabase.unitDetails[4].foodCost
           && resourcesStatus.gold_Amount >= unitDatabase.unitDetails[4].goldCost
           && population.currentPopulation + unitDatabase.unitDetails[4].population <= population.currentHouseCapacity
           && GameObject.FindGameObjectWithTag("Artillary") == true)
        {
            resourcesStatus.ResourcesChange("Food", -(unitDatabase.unitDetails[4].foodCost));
            resourcesStatus.ResourcesChange("Gold", -(unitDatabase.unitDetails[4].goldCost));
            population.PopulationChanges(unitDatabase.unitDetails[4].population);
            // Clone Cannon at position of "positionToPlace"
            Debug.Log("Built Cannon");
        }
    }
}
