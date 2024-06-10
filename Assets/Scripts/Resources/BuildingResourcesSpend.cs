using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingResourcesSpend : MonoBehaviour // this script use to calculate how much resources spend when place this building
{
   public ResourcesStatus resourcesStatus;
   public BuildingCost buildingCost;
   public PlacementSystem placementSystem;

   public void CheckAvailbleResources(int buildingID)
    {
        if(buildingID == 0) // House
        {
            if(resourcesStatus.wood_Amount < buildingCost.buildingCostsData[0].woodRequire)
            {
                placementSystem.canItPlace = false;
            }
            else
            {
                resourcesStatus.wood_Amount = resourcesStatus.wood_Amount - buildingCost.buildingCostsData[0].woodRequire;
                resourcesStatus.wood_Text.text = resourcesStatus.wood_Amount.ToString();
                placementSystem.canItPlace = true;
            }
        }

        if(buildingID == 1) // Town Center
        {
            if(resourcesStatus.wood_Amount < buildingCost.buildingCostsData[1].woodRequire)
            {
                if(resourcesStatus.stone_Amount < buildingCost.buildingCostsData[1].stoneRequire)
                {
                    Debug.Log("Need more resources");
                    placementSystem.canItPlace = false;
                }
                else
                {
                    Debug.Log("Need more resources");
                    placementSystem.canItPlace = false;
                }
            }

            else if (resourcesStatus.stone_Amount < buildingCost.buildingCostsData[1].stoneRequire)
            {
                if (resourcesStatus.wood_Amount < buildingCost.buildingCostsData[1].woodRequire)
                {
                    Debug.Log("Need more resources");
                    placementSystem.canItPlace = false;
                }
                else
                {
                    Debug.Log("Need more resources");
                    placementSystem.canItPlace = false;
                }
            }

            else 
            {
                resourcesStatus.wood_Amount = resourcesStatus.wood_Amount - buildingCost.buildingCostsData[1].woodRequire;
                resourcesStatus.stone_Amount = resourcesStatus.stone_Amount - buildingCost.buildingCostsData[1].stoneRequire;
                resourcesStatus.wood_Text.text = resourcesStatus.wood_Amount.ToString();
                resourcesStatus.stone_Text.text = resourcesStatus.stone_Amount.ToString();
                placementSystem.canItPlace = true;
            }
        }

        if(buildingID == 2) // Lumber Camp
        {
            if (resourcesStatus.wood_Amount < buildingCost.buildingCostsData[2].woodRequire)
            {
                Debug.Log("Need more resources");
                placementSystem.canItPlace = false;
            }
            else
            {
                resourcesStatus.wood_Amount = resourcesStatus.wood_Amount - buildingCost.buildingCostsData[2].woodRequire;
                resourcesStatus.wood_Text.text = resourcesStatus.wood_Amount.ToString();
                placementSystem.canItPlace = true;
            }
        }

        if(buildingID == 3) // Mining Cart
        {
            if (resourcesStatus.wood_Amount < buildingCost.buildingCostsData[3].woodRequire)
            {
                Debug.Log("Need more resources");
                placementSystem.canItPlace = false;
            }
            else
            {
                resourcesStatus.wood_Amount = resourcesStatus.wood_Amount - buildingCost.buildingCostsData[3].woodRequire;
                resourcesStatus.wood_Text.text = resourcesStatus.wood_Amount.ToString();
                placementSystem.canItPlace = true;
            }
        }

        if(buildingID == 4) // Wind Mill
        {
            if (resourcesStatus.wood_Amount < buildingCost.buildingCostsData[4].woodRequire)
            {
                Debug.Log("Need more resources");
                placementSystem.canItPlace = false;
            }
            else
            {
                resourcesStatus.wood_Amount = resourcesStatus.wood_Amount - buildingCost.buildingCostsData[4].woodRequire;
                resourcesStatus.wood_Text.text = resourcesStatus.wood_Amount.ToString();
                placementSystem.canItPlace = true;
            }
        }

        if(buildingID == 5) // Barrack
        {
            if (resourcesStatus.wood_Amount < buildingCost.buildingCostsData[5].woodRequire)
            {
                Debug.Log("Need more resources");
                placementSystem.canItPlace = false;
            }
            else
            {
                resourcesStatus.wood_Amount = resourcesStatus.wood_Amount - buildingCost.buildingCostsData[5].woodRequire;
                resourcesStatus.wood_Text.text = resourcesStatus.wood_Amount.ToString();
                placementSystem.canItPlace = true;
            }
        }

        if(buildingID == 6) // Artillery
        {
            if (resourcesStatus.wood_Amount < buildingCost.buildingCostsData[6].woodRequire)
            {
                Debug.Log("Need more resources");
                placementSystem.canItPlace = false;
            }
            else
            {
                resourcesStatus.wood_Amount = resourcesStatus.wood_Amount - buildingCost.buildingCostsData[6].woodRequire;
                resourcesStatus.wood_Text.text = resourcesStatus.wood_Amount.ToString();
                placementSystem.canItPlace = true;
            }
        }
    }
}
