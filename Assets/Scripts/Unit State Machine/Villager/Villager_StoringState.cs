using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Villager_StoringState : VillagerBaseState
{
    public override void EnterState(VillagerStateController villager)
    {
        // Enter MovingState
        villager.Villager.enabled = true;
        villager.Villager.isStopped = false;

        #region Auto find all storing points 
        GameObject[] lumberCamps = GameObject.FindGameObjectsWithTag("Wood Storage"); // all Lumber Camp in game
        GameObject[] townCenters = GameObject.FindGameObjectsWithTag("Town Center"); // all Town Center in game
        GameObject[] miningCart = GameObject.FindGameObjectsWithTag("Gold Stone Storage"); // all Mining Cart in game
        GameObject[] windMill = GameObject.FindGameObjectsWithTag("Food Storage"); // all Wind Mill in game

        if (villager.isStoringManual == false) // if the villager is auto going to store the resources
        {
            #region Wood
            if (villager.currentCarryingResource == "Wood") // if that villager carry wood
            {
                if (GameObject.FindGameObjectWithTag("Wood Storage") != null)
                {
                    GameObject closestLumberCamp = FindClosestStoringPoint(villager.transform.position, lumberCamps); // Find The Game Object of closest Lumber Camp
                    GameObject closestTownCenter = FindClosestStoringPoint(villager.transform.position, townCenters); // Find The Game Object of closest Town Center

                    float closestLumberCampValue = Vector3.Distance(villager.Villager.transform.position, closestLumberCamp.transform.position); //Distance between villager and The closest Lumber Camp
                    float closestTownCenterValue = Vector3.Distance(villager.Villager.transform.position, closestTownCenter.transform.position); // Distance between villager and The closest Town Center

                    if (closestLumberCampValue < closestTownCenterValue) // If Lumber Camp is closer than Town Center
                    {
                        Vector3 targetPosition = closestLumberCamp.transform.position; // the position of that Lumber Camp in Vector 3
                        targetPosition.x = targetPosition.x - 1;    // Set a little bit new position x and z
                        targetPosition.z = targetPosition.z - 1.5f; //
                        villager.Villager.SetDestination(targetPosition); // make villager walk to that Lumber Camp
                        villager.isStoringManual = false;
                    }
                    if (closestTownCenterValue < closestLumberCampValue) //If Town Center is closer than Lumber Camp
                    {
                        Vector3 targetPosition = closestTownCenter.transform.position; // The position of Town Center in Vector 3
                        targetPosition.x = targetPosition.x - 1;   // Set a little bit new position x and z
                        targetPosition.z = targetPosition.z - 1.5f;//
                        villager.Villager.SetDestination(targetPosition); // make villager walk to that Town Center
                        villager.isStoringManual = false;
                    }
                }

                else // if there has no any Lumber Camp in the game
                {
                    GameObject closestTownCenter = FindClosestStoringPoint(villager.transform.position, townCenters); // Find The Game Object of closest Town Center
                    Vector3 townCenterPosition = closestTownCenter.transform.position; //define the target position for villager
                    townCenterPosition.x = townCenterPosition.x - 1;   // Set a liitle bit new position
                    townCenterPosition.z = townCenterPosition.z - 1.5f;//
                    villager.Villager.SetDestination(townCenterPosition); //make villager walk to town center 
                    villager.isStoringManual = false;
                }
            }
            #endregion

            #region Gold
            if (villager.currentCarryingResource == "Gold")
            {

                if (GameObject.FindGameObjectWithTag("Gold Stone Storage") != null)
                {
                    GameObject closestMiningCart = FindClosestStoringPoint(villager.transform.position, miningCart); // Find The Game Object of closest mining cart
                    GameObject closestTownCenter = FindClosestStoringPoint(villager.transform.position, townCenters); // Find The Game Object of closest Town Center

                    float closestMiningCartValue = Vector3.Distance(villager.Villager.transform.position, closestMiningCart.transform.position); //Distance between villager and The closest Lumber Camp
                    float closestTownCenterValue = Vector3.Distance(villager.Villager.transform.position, closestTownCenter.transform.position); // Distance between villager and The closest Town Center

                    if (closestMiningCartValue < closestTownCenterValue) // If Lumber Camp is closer than Town Center
                    {
                        Vector3 targetPosition = closestMiningCart.transform.position; // the position of that Lumber Camp in Vector 3
                        /* targetPosition.x = targetPosition.x - 0.5f;    // Set a little bit new position x and z
                         targetPosition.z = targetPosition.z - 1f; //*/
                        villager.Villager.SetDestination(targetPosition); // make villager walk to that Lumber Camp
                        villager.isStoringManual = false;
                    }
                    if (closestTownCenterValue < closestMiningCartValue) //If Town Center is closer than Lumber Camp
                    {
                        Vector3 targetPosition = closestTownCenter.transform.position; // The position of Town Center in Vector 3
                        targetPosition.x = targetPosition.x - 1;   // Set a little bit new position x and z
                        targetPosition.z = targetPosition.z - 1.5f;//
                        villager.Villager.SetDestination(targetPosition); // make villager walk to that Town Center
                        villager.isStoringManual = false;
                    }
                }

                else
                {
                    GameObject closestTownCenter = FindClosestStoringPoint(villager.transform.position, townCenters); // Find The Game Object of closest Town Center
                    Vector3 townCenterPosition = closestTownCenter.transform.position; //define the target position for villager
                    townCenterPosition.x = townCenterPosition.x - 1;   // Set a liitle bit new position
                    townCenterPosition.z = townCenterPosition.z - 1.5f;//
                    villager.Villager.SetDestination(townCenterPosition); //make villager walk to town center 
                    villager.isStoringManual = false;
                }
            }
            #endregion

            #region Stone
            if (villager.currentCarryingResource == "Stone")
            {
                if (GameObject.FindGameObjectWithTag("Gold Stone Storage") != null)
                {
                    GameObject closestMiningCart = FindClosestStoringPoint(villager.transform.position, miningCart); // Find The Game Object of closest Lumber Camp
                    GameObject closestTownCenter = FindClosestStoringPoint(villager.transform.position, townCenters); // Find The Game Object of closest Town Center

                    float closestMiningCartValue = Vector3.Distance(villager.Villager.transform.position, closestMiningCart.transform.position); //Distance between villager and The closest Lumber Camp
                    float closestTownCenterValue = Vector3.Distance(villager.Villager.transform.position, closestTownCenter.transform.position); // Distance between villager and The closest Town Center

                    if (closestMiningCartValue < closestTownCenterValue) // If Lumber Camp is closer than Town Center
                    {
                        Vector3 targetPosition = closestMiningCart.transform.position; // the position of that Lumber Camp in Vector 3
                        /* targetPosition.x = targetPosition.x - 0.5f;    // Set a little bit new position x and z
                         targetPosition.z = targetPosition.z - 1f; //*/
                        villager.Villager.SetDestination(targetPosition); // make villager walk to that Lumber Camp
                        villager.isStoringManual = false;
                    }
                    if (closestTownCenterValue < closestMiningCartValue) //If Town Center is closer than Lumber Camp
                    {
                        Vector3 targetPosition = closestTownCenter.transform.position; // The position of Town Center in Vector 3
                        targetPosition.x = targetPosition.x - 1;   // Set a little bit new position x and z
                        targetPosition.z = targetPosition.z - 1.5f;//
                        villager.Villager.SetDestination(targetPosition); // make villager walk to that Town Center
                        villager.isStoringManual = false;
                    }
                }

                else
                {
                    GameObject closestTownCenter = FindClosestStoringPoint(villager.transform.position, townCenters); // Find The Game Object of closest Town Center
                    Vector3 townCenterPosition = closestTownCenter.transform.position; //define the target position for villager
                    townCenterPosition.x = townCenterPosition.x - 1;   // Set a liitle bit new position
                    townCenterPosition.z = townCenterPosition.z - 1.5f;//
                    villager.Villager.SetDestination(townCenterPosition); //make villager walk to town center 
                    villager.isStoringManual = false;
                }
            }

            #endregion

            #region Food
            if (villager.currentCarryingResource == "Food")
            {
                if (GameObject.FindGameObjectWithTag("Food Storage") != null)
                {
                    GameObject closestWindMill = FindClosestStoringPoint(villager.transform.position, windMill); // Find The Game Object of closest Lumber Camp
                    GameObject closestTownCenter = FindClosestStoringPoint(villager.transform.position, townCenters); // Find The Game Object of closest Town Center

                    float closestMiningCartValue = Vector3.Distance(villager.Villager.transform.position, closestWindMill.transform.position); //Distance between villager and The closest Lumber Camp
                    float closestTownCenterValue = Vector3.Distance(villager.Villager.transform.position, closestTownCenter.transform.position); // Distance between villager and The closest Town Center

                    if (closestMiningCartValue < closestTownCenterValue) // If Lumber Camp is closer than Town Center
                    {
                        Vector3 targetPosition = closestWindMill.transform.position; // the position of that Lumber Camp in Vector 3
                        /* targetPosition.x = targetPosition.x - 0.5f;    // Set a little bit new position x and z
                         targetPosition.z = targetPosition.z - 1f; //*/
                        villager.Villager.SetDestination(targetPosition); // make villager walk to that Lumber Camp
                        villager.isStoringManual = false;
                    }
                    if (closestTownCenterValue < closestMiningCartValue) //If Town Center is closer than Lumber Camp
                    {
                        Vector3 targetPosition = closestTownCenter.transform.position; // The position of Town Center in Vector 3
                        targetPosition.x = targetPosition.x - 1;   // Set a little bit new position x and z
                        targetPosition.z = targetPosition.z - 1.5f;//
                        villager.Villager.SetDestination(targetPosition); // make villager walk to that Town Center
                        villager.isStoringManual = false;
                    }
                }

                else
                {
                    GameObject closestTownCenter = FindClosestStoringPoint(villager.transform.position, townCenters); // Find The Game Object of closest Town Center
                    Vector3 townCenterPosition = closestTownCenter.transform.position; //define the target position for villager
                    townCenterPosition.x = townCenterPosition.x - 1;   // Set a liitle bit new position
                    townCenterPosition.z = townCenterPosition.z - 1.5f;//
                    villager.Villager.SetDestination(townCenterPosition); //make villager walk to town center 
                    villager.isStoringManual = false;
                }
            }

            #endregion
        }
        #endregion

        if (villager.isStoringManual == true)
        {
            villager.Villager.SetDestination(villager.selectedStoringPoint.transform.position);
        }
    }
    public override void UpdateState(VillagerStateController villager)
    {
        #region If villager reach the storing point Switch to //"vil_GatheringState" // Switch to Idel State

        if(villager.Villager.remainingDistance < 1)
        {
            if(villager.isStoringManual == false)
            {
                villager.resourcesStatus.ResourcesChange(villager.currentCarryingResource, villager.gatheringAmount);
                villager.gatheringAmount = 0;
                villager.SwitchState(villager.vil_GatheringState);
            }

            if(villager.isStoringManual == true)
            {
                villager.resourcesStatus.ResourcesChange(villager.currentCarryingResource, villager.gatheringAmount);
                villager.gatheringAmount = 0;
                villager.currentCarryingResource = null;
                villager.SwitchState(villager.vil_IdelState);
            }
        }

        #endregion

        #region Right click to order villager to store resources manually or Switch villager to Moving State

        if (villager.unitSelection.unitSelected.Contains(villager.gameObject) && Input.GetMouseButtonDown(1)) // if select villager and right click at storing point
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                #region Switch to Moving State
                if (hit.collider.CompareTag("Ground")
                    && !hit.collider.CompareTag("Wood")
                    && !hit.collider.CompareTag("Food")
                    && !hit.collider.CompareTag("Gold")
                    && !hit.collider.CompareTag("Stone"))
                {
                   //villager.selectedPosition = hit.point;
                   villager.SwitchState(villager.vil_MovingState);
                }
                #endregion

                if (hit.collider.CompareTag("Town Center")) //if click on Town Center (Every resources can store in Town Center)
                {
                    villager.Villager.SetDestination(hit.collider.transform.position); // go to that storing point
                    villager.isStoringManual = true; // it is manual
                }

                if (hit.collider.CompareTag("Wood Storage") && villager.currentCarryingResource == "Wood") // if click on lumber camp and that villager carrying woods
                {
                    villager.Villager.SetDestination(hit.collider.transform.position); // go to that storing point
                    villager.isStoringManual = true; // it is manual
                }

                if (hit.collider.CompareTag("Gold Stone Storage") && villager.currentCarryingResource == "Gold") // if click on lumber camp and that villager carrying gold
                {
                    villager.Villager.SetDestination(hit.collider.transform.position); // go to that storing point
                    villager.isStoringManual = true; // it is manual
                }

                if (hit.collider.CompareTag("Gold Stone Storage") && villager.currentCarryingResource == "Stone") // if click on lumber camp and that villager carrying stone
                {
                    villager.Villager.SetDestination(hit.collider.transform.position); // go to that storing point
                    villager.isStoringManual = true; // it is manual
                }


                if (hit.collider.CompareTag("Food Storage") && villager.currentCarryingResource == "Stone") // if click on lumber camp and that villager carrying food
                {
                    villager.Villager.SetDestination(hit.collider.transform.position); // go to that storing point
                    villager.isStoringManual = true; // it is manual
                }
            }
        }
        #endregion

        #region Switch to Exit State
        if (villager.unitStat.unitHP <= 0)
        {
            ExitState(villager);
        }
        #endregion
    }
    public override void ExitState(VillagerStateController villager)
    {
        villager.population.PopulationChanges(-1 * villager.unitStat.unitPopulation); //Decrease population
        MonoBehaviour.Destroy(villager.gameObject); // Delete Villager from the game
    }

    private GameObject FindClosestStoringPoint(Vector3 currentPosition, GameObject[] storingPoint)
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject StoringPoint in storingPoint)
        {
            float distance = Vector3.Distance(currentPosition, StoringPoint.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = StoringPoint;
            }
        }
        return closestObject;
    }
}

