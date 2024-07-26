using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System.Net.WebSockets;
using System.Linq;

public class Villager_GatheringState : VillagerBaseState
{
    private float lastShotTime = 0.0f;
    private GameObject avaliableWaypoint = null;
    private bool conditionMet;
    private int gatheringCapacity;
    private bool startGathering;
    //Upgrade
    //Wood
    private bool isWoodGatheringSpeedUpgrade;
    private bool isWoodGatheringCapacityUpgrade;
    private bool isAlreadyUpgradeWoodCapacity;

    //Gold & Stone
    private bool isGoldStoneGatheringSpeedUpgrade;
    private bool isGoldStoneGatheringCapacityUpgrade;
    private bool isAlreadyUpgradeGoldStoneCapacity;

    //Food
    private bool isFoodGatheringSpeedUpgrade;
    private bool isFoodGatheringCapacityUpgrade;
    private bool isAlreadyUpgradeFoodCapacity;

    //Boolean
    private bool isDead;
    private bool isSoundPlay;
    private bool isStopLooping;

    public override void EnterState(VillagerStateController villager)
    {
        if(villager.unitStat.unitHP > 0)
        {
            villager.Villager.isStopped = false;
            villager.Villager.enabled = true;
        }

        avaliableWaypoint = null;
        conditionMet = false;
        startGathering = false;
        isSoundPlay = false;
        isStopLooping = false;  

        // Play animation Villager_Walking
        villager.villagerAnimator.SetBool("isWalking", true);
        villager.basket.SetActive(true); //show basket
        villager.rigBuilder.enabled = true; //make villager do hollding action

        //Find the closest waypoint
        FindClosestWaypoint(villager);
        ////

        //Is Upgrade (change varible name for easier to look)
        isWoodGatheringSpeedUpgrade = villager.upgradeStatus.isWoodGatheringSpeedUpgrade;
        isWoodGatheringCapacityUpgrade = villager.upgradeStatus.isWoodGatheringCapacityUpgrade;

        isGoldStoneGatheringSpeedUpgrade = villager.upgradeStatus.isGoldStoneGatheringSpeedUpgrade;
        isGoldStoneGatheringCapacityUpgrade = villager.upgradeStatus.isGoldStoneGatheringCapacityUpgrade;

        isFoodGatheringSpeedUpgrade = villager.upgradeStatus.isFoodGatheringSpeedUpgrade;
        isFoodGatheringCapacityUpgrade = villager.upgradeStatus.isFoodGatheringCapacityUpgrade;

        // if capacity upgrade
        //Wood
        if(isWoodGatheringCapacityUpgrade == true && isAlreadyUpgradeWoodCapacity == false)
        {
            villager.woodCarryingCapacity = villager.woodCarryingCapacity + villager.economicUpgradeDatabase.economicUpgrade[0].increaseGatheringCapacity;
            isAlreadyUpgradeWoodCapacity = true;
        }

        //Gold and Stone
        if(isGoldStoneGatheringCapacityUpgrade == true && isAlreadyUpgradeGoldStoneCapacity == false)
        {
            villager.goldStoneCarryingCapacity = villager.goldStoneCarryingCapacity + villager.economicUpgradeDatabase.economicUpgrade[1].increaseGatheringCapacity;
            isAlreadyUpgradeGoldStoneCapacity = true;
        }

        if(isFoodGatheringCapacityUpgrade == true && isAlreadyUpgradeFoodCapacity == false)
        {
            villager.foodCarryingCapacity = villager.foodCarryingCapacity + villager.economicUpgradeDatabase.economicUpgrade[2].increaseGatheringCapacity;
            isAlreadyUpgradeFoodCapacity = true;
        }
    }
    public override void UpdateState(VillagerStateController villager)
    {
        #region Is villager find a available waypoint?

        if (conditionMet == true 
            && villager.Villager.enabled == true) // if there has a available waypoint and NavmeshAgent is enabled
        {
            if (villager.Villager.remainingDistance <= 0 && villager.Villager.pathPending == false) // Is villager reach the destination
            {
                //Play animation Villager_Gathering
                villager.villagerAnimator.SetBool("isReachResources", true);
                villager.rigBuilder.enabled = false;// make hand can move 

                villager.Villager.isStopped = true;
                villager.Villager.enabled = false; // villager will stop
                villager.transform.LookAt(villager.targetResources.transform.position);
                startGathering = true;
            }
            else
            {
                //Stop Play Sound
                villager.soundEffectController.StopPlaySound();
            }
        }

        if (startGathering == true) // start gathering if startGathering = true
        {
            isSoundPlay = true;
            GatherResources(villager);
        }

        if (isSoundPlay == true && isStopLooping == false) // Manage sound to play one time
        {
            //Play Gathering Sound
            villager.soundEffectController.PlayGatheringSound();
            isStopLooping = true;
        }
            
        #endregion

        #region If select another resources or storing point
        if (villager.unitSelection.unitSelected.Contains(villager.gameObject) && Input.GetMouseButtonDown(1)) // if select new target resources
        {
            ChangeTargetResources(villager);
        }
        #endregion

        #region Switch to Storing State

        if (villager.currentCarryingResource == "Wood")
        {
            if (villager.gatheringAmount == villager.woodCarryingCapacity) // if villager finish gather resources, he will change to "vil_StoringState"
            {
                startGathering = false; // stop gathering
                villager.Villager.enabled = true;
                villager.Villager.isStopped = false;
                villager.isStoringManual = false;
                villager.gatheringWaypointForTree.WaypointStatus(avaliableWaypoint, true);
                villager.SwitchState(villager.vil_StoringState);
            }
        }

        if (villager.currentCarryingResource == "Gold")
        {
            if (villager.gatheringAmount == villager.goldStoneCarryingCapacity) // if villager finish gather resources, he will change to "vil_StoringState"
            {
                startGathering = false; // stop gathering
                villager.Villager.enabled = true;
                villager.Villager.isStopped = false;
                villager.isStoringManual = false;
                villager.gatheringWaypointForGold.WaypointStatus(avaliableWaypoint, true);
                villager.SwitchState(villager.vil_StoringState);
            }
        }

        if (villager.currentCarryingResource == "Stone")
        {
            if (villager.gatheringAmount == villager.goldStoneCarryingCapacity) // if villager finish gather resources, he will change to "vil_StoringState"
            {
                startGathering = false; // stop gathering
                villager.Villager.enabled = true;
                villager.Villager.isStopped = false;
                villager.isStoringManual = false;
                villager.gatheringWaypointForStone.WaypointStatus(avaliableWaypoint, true);
                villager.SwitchState(villager.vil_StoringState);
            }
        }

        if (villager.currentCarryingResource == "Food")
        {
            if(villager.gatheringAmount == villager.foodCarryingCapacity)
            {
                startGathering = false; // stop gathering
                villager.Villager.enabled = true;
                villager.Villager.isStopped = false;
                villager.isStoringManual = false;
                villager.gatheringWaypointForFood.WaypointStatus(avaliableWaypoint, true);
                villager.SwitchState(villager.vil_StoringState);
            }
        }
        #endregion

        #region Switch to Exit State
        if (villager.unitStat.unitHP <= 0 && isDead == false)
        {
            isDead = true;
            ExitState(villager);
        }
        #endregion
    }

    public override void ExitState(VillagerStateController villager)
    {
        MakeWayPointAvailable(villager);

        //Play animation Villager_Death
        villager.villagerAnimator.SetBool("isDead", true);
        villager.basket.SetActive(false);
        villager.rigBuilder.enabled = false;
        //

        //Play UnitDie Sound
        villager.soundEffectController.PlayUnitDiedSound();

        villager.Villager.isStopped = true; // Make Villager cannot move
        startGathering = false; // Stop Gathering Resources
        villager.population.PopulationChanges(-1 * villager.unitStat.unitPopulation); //Decrease population
        villager.villagerCollider.enabled = false; // disable collider to stop enemy detect this unit
        MonoBehaviour.Destroy(villager.gameObject, 4); // Delete Villager from the game
    }

    public void GatherResources(VillagerStateController villager)
    {
        #region Gather Wood
        if (villager.currentCarryingResource == "Wood") //if villager is going to gathering wood
        {
            if(isWoodGatheringSpeedUpgrade == true) //if upgrade speed of gathering
            {
                //if upgrade gathering speed (unitAttackSpeed in villager is the speed of gathering resources)
                if (Time.time > lastShotTime + villager.unitStat.unitAttackSpeed - villager.economicUpgradeDatabase.economicUpgrade[0].increaseGatheringSpeed) // villager will start gatering resources with more speed
                {
                    villager.gatheringAmount = villager.gatheringAmount + 1;
                    lastShotTime = Time.time;
                }
            }

            else
            {
                if (Time.time > lastShotTime + villager.unitStat.unitAttackSpeed) // villager will start gatering resources
                {
                    villager.gatheringAmount = villager.gatheringAmount + 1;
                    lastShotTime = Time.time;
                }
            }
        }
        #endregion

        #region Gather Gold or Stone
        if (villager.currentCarryingResource == "Gold" || villager.currentCarryingResource == "Stone")
        {
            if (isGoldStoneGatheringSpeedUpgrade == true) //if upgrade speed of gathering
            {
                //if upgrade gathering speed (unitAttackSpeed in villager is the speed of gathering resources)
                if (Time.time > lastShotTime + villager.unitStat.unitAttackSpeed - villager.economicUpgradeDatabase.economicUpgrade[1].increaseGatheringSpeed) // villager will start gatering resources with more speed
                {
                    villager.gatheringAmount = villager.gatheringAmount + 1;
                    lastShotTime = Time.time;
                }
            }

            else
            {
                if (Time.time > lastShotTime + villager.unitStat.unitAttackSpeed) // villager will start gatering resources normal speed
                {
                    villager.gatheringAmount = villager.gatheringAmount + 1;
                    lastShotTime = Time.time;
                }
            }
        }
        #endregion

        #region Gather Food
        if (villager.currentCarryingResource == "Food")
        {
            if (isFoodGatheringSpeedUpgrade == true) // if upgrade speed of gathering
            {
                //if upgrade gathering speed (unitAttackSpeed in villager is the speed of gathering resources)
                if (Time.time > lastShotTime + villager.unitStat.unitAttackSpeed - villager.economicUpgradeDatabase.economicUpgrade[2].increaseGatheringSpeed) // villager will start gatering resources with more speed
                {
                    villager.gatheringAmount = villager.gatheringAmount + 1;
                    lastShotTime = Time.time;
                }
            }

            else
            {
                if (Time.time > lastShotTime + villager.unitStat.unitAttackSpeed) // villager will start gatering resources normal speed
                {
                    villager.gatheringAmount = villager.gatheringAmount + 1;
                    lastShotTime = Time.time;
                }
            }
        }
        #endregion
    }

    private void MakeWayPointAvailable(VillagerStateController villager)
    {
        if (villager.currentCarryingResource == "Wood")
        {
            villager.gatheringWaypointForTree.WaypointStatus(avaliableWaypoint, true); // make the waypoint available
        }

        if (villager.currentCarryingResource == "Gold")
        {
            villager.gatheringWaypointForGold.WaypointStatus(avaliableWaypoint, true); // make the waypoint available
        }

        if (villager.currentCarryingResource == "Food")
        {
            villager.gatheringWaypointForFood.WaypointStatus(avaliableWaypoint, true);// make the waypoint available
        }

        if (villager.currentCarryingResource == "Stone")
        {
            villager.gatheringWaypointForStone.WaypointStatus(avaliableWaypoint, true);// make the waypoint available
        }
    }

    public void ChangeTargetResources(VillagerStateController villager)
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

        #region Change target resources
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, villager.resorcesLayerMask))
        {      
            if (hit.collider.gameObject == villager.targetResources) // if select the same game object, it will have nothing happen.
            {
                   ////Keep Playing animation Villager_Gathering
                   //villager.villagerAnimator.SetBool("isReachResources", true);
                   //villager.rigBuilder.enabled = false;
            }

            else if (hit.collider.gameObject.CompareTag("Wood")
                     && hit.collider.gameObject != villager.targetResources) // if select wood and it not the same object that you clicked.
            {
                //Play animation Villager_Walking
                villager.villagerAnimator.SetBool("isReachResources", false);
                villager.rigBuilder.enabled = true;
                //

                MakeWayPointAvailable(villager);
                startGathering = false; // stop villager from gathering
                villager.Villager.enabled = true;
                villager.Villager.isStopped = false; // make villager can move

                villager.gatheringWaypointForTree = hit.collider.GetComponent<GatheringWaypointForTree>();
                villager.targetResources = hit.collider.gameObject; // target resources game object
                villager.currentCarryingResource = "Wood";
                villager.gatheringAmount = 0;
                villager.SwitchState(villager.vil_GatheringState);
            }

            else if (hit.collider.gameObject.CompareTag("Gold")
                && hit.collider.gameObject != villager.targetResources) // if select Gold and it not the same object that you clicked.
            {
                //Play animation Villager_Walking
                villager.villagerAnimator.SetBool("isReachResources", false);
                villager.rigBuilder.enabled = true;
                //

                MakeWayPointAvailable(villager);
                startGathering = false;
                villager.Villager.enabled = true;
                villager.Villager.isStopped = false;// make villager can move

                villager.gatheringWaypointForGold = hit.collider.GetComponent<GatheringWaypointForGold>();
                villager.targetResources = hit.collider.gameObject; // target resources game object
                villager.currentCarryingResource = "Gold";
                villager.gatheringAmount = 0;
                villager.SwitchState(villager.vil_GatheringState);
            }

            else if (hit.collider.gameObject.CompareTag("Stone") && hit.collider.gameObject != villager.targetResources) // if select stone and it not the same object that you clicked.
            {
                //Play animation Villager_Walking
                villager.villagerAnimator.SetBool("isReachResources", false);
                villager.rigBuilder.enabled = true;
                //

                MakeWayPointAvailable(villager);
                startGathering = false;
                villager.Villager.enabled = true;
                villager.Villager.isStopped = false;// make villager can move

                villager.gatheringWaypointForStone = hit.collider.GetComponent<GatheringWaypointForStone>();
                villager.targetResources = hit.collider.gameObject;
                villager.currentCarryingResource = "Stone";
                villager.gatheringAmount = 0;
                villager.SwitchState(villager.vil_GatheringState);
            }

            else if (hit.collider.gameObject.CompareTag("Food") && hit.collider.gameObject != villager.targetResources) // if select food and it not the same object that you clicked.
            {
                //Play animation Villager_Walking
                villager.villagerAnimator.SetBool("isReachResources", false);
                villager.rigBuilder.enabled = true;
                //

                MakeWayPointAvailable(villager);
                startGathering = false;
                villager.Villager.enabled = true;
                villager.Villager.isStopped = false;// make villager can move

                villager.gatheringWaypointForFood = hit.collider.GetComponent<GatheringWaypointForFood>();
                villager.targetResources = hit.collider.gameObject;
                villager.currentCarryingResource = "Food";
                villager.gatheringAmount = 0;
                villager.SwitchState(villager.vil_GatheringState);
            }
        }
        #endregion

        #region Switch to Moving state

        if(villager.unitSelection.unitSelected.Contains(villager.gameObject) && Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Ground")
                    && !hit.collider.CompareTag("Wood")
                    && !hit.collider.CompareTag("Food")
                    && !hit.collider.CompareTag("Gold")
                    && !hit.collider.CompareTag("Stone"))
                {
                    MakeWayPointAvailable(villager);
                    villager.villagerAnimator.SetBool("isReachResources", false);
                    villager.Villager.enabled = true; //make NavMeshAgent active
                    villager.Villager.isStopped = false; // make villager can move
                    villager.currentCarryingResource = null;
                    villager.SwitchState(villager.vil_MovingState); // change state
                }
            }
        }

        #endregion

        #region if click at storing point it will switch to Storing State
        if (villager.unitSelection.unitSelected.Contains(villager.gameObject) && Input.GetMouseButtonDown(1)) // if select villager and right click at storing point
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Town Center")) //if click on Town Center
                {
                    MakeWayPointAvailable(villager);
                    villager.Villager.isStopped = false; // make villager can move
                    villager.selectedStoringPoint = hit.collider.gameObject;
                    villager.isStoringManual = true; //manual storing resources
                    villager.SwitchState(villager.vil_StoringState);
                }

                else if(hit.collider.CompareTag("Wood Storage") && villager.currentCarryingResource == "Wood") //if click on Lumber Camp and villager is carrying wood
                {
                    villager.gatheringWaypointForTree.WaypointStatus(avaliableWaypoint, true);
                    villager.Villager.isStopped = false; // make villager can move
                    villager.selectedStoringPoint = hit.collider.gameObject;
                    villager.isStoringManual = true; //manual storing resources
                    villager.SwitchState(villager.vil_StoringState);
                }

                else if(hit.collider.CompareTag("Gold Stone Storage") && villager.currentCarryingResource == "Gold") //if click on Mining Cart and villager is carrying gold
                {
                    villager.gatheringWaypointForGold.WaypointStatus(avaliableWaypoint, true);
                    villager.Villager.isStopped = false; // make villager can move
                    villager.selectedStoringPoint = hit.collider.gameObject;
                    villager.isStoringManual = true; //manual storing resources
                    villager.SwitchState(villager.vil_StoringState);
                }

                else if(hit.collider.CompareTag("Gold Stone Storage") && villager.currentCarryingResource == "Stone") //if click on Mining Cart and villager is carrying stone
                {
                    villager.gatheringWaypointForStone.WaypointStatus(avaliableWaypoint, true);
                    villager.Villager.isStopped = false; // make villager can move
                    villager.selectedStoringPoint = hit.collider.gameObject;
                    villager.isStoringManual = true; //manual storing resources
                    villager.SwitchState(villager.vil_StoringState);
                }

                else if(hit.collider.CompareTag("Food Storage") && villager.currentCarryingResource == "Food") 
                {
                    villager.gatheringWaypointForFood.WaypointStatus(avaliableWaypoint, true);
                    villager.Villager.isStopped = false; // make villager can move
                    villager.selectedStoringPoint = hit.collider.gameObject;
                    villager.isStoringManual = true; //manual storing resources
                    villager.SwitchState(villager.vil_StoringState);
                }

                else // such as click on house, barrack and etc.
                {
                    //Nothing Happen
                }
            }
        }
        #endregion
    }

    private void FindClosestWaypoint(VillagerStateController villager)
    {
        #region Find the closest waypoint of the tree
        if (villager.currentCarryingResource == "Wood")
        {
            foreach (KeyValuePair<GameObject, bool> waypoint in villager.gatheringWaypointForTree.waypoints)
            {
                if (waypoint.Value == true)
                {
                    avaliableWaypoint = waypoint.Key;
                    conditionMet = true;
                    break;
                }
            }

            if (conditionMet == true)
            {
                /// Finish Finding the waypoint
                if (villager.gatheringWaypointForTree.waypoints[avaliableWaypoint] == true)
                {
                    villager.gatheringWaypointForTree.WaypointStatus(avaliableWaypoint, false);
                    villager.Villager.SetDestination(avaliableWaypoint.transform.position);
                }
            }
            else
            {
                villager.SwitchState(villager.vil_IdelState);
            }
        }
        #endregion

        #region Find the closest waypoint of the gold
        if (villager.currentCarryingResource == "Gold")
        {
            foreach (KeyValuePair<GameObject, bool> waypoint in villager.gatheringWaypointForGold.waypoints)
            {
                if (waypoint.Value == true)
                {
                    avaliableWaypoint = waypoint.Key;
                    conditionMet = true;
                    break;
                }
            }

            if (conditionMet == true)
            {
                /// Finish Finding the waypoint
                if (villager.gatheringWaypointForGold.waypoints[avaliableWaypoint] == true)
                {
                    villager.gatheringWaypointForGold.WaypointStatus(avaliableWaypoint, false);
                    villager.Villager.SetDestination(avaliableWaypoint.transform.position);
                }
            }
            else
            {
                villager.SwitchState(villager.vil_IdelState);
            }
        }
        #endregion

        #region Find the closest waypoint of the food
        if (villager.currentCarryingResource == "Food")
        {
            foreach (KeyValuePair<GameObject, bool> waypoint in villager.gatheringWaypointForFood.waypoints)
            {
                if (waypoint.Value == true)
                {
                    avaliableWaypoint = waypoint.Key;
                    conditionMet = true;
                    break;
                }
            }

            if (conditionMet == true)
            { 
                /// Finish Finding the waypoint
                if (villager.gatheringWaypointForFood.waypoints[avaliableWaypoint] == true)
                {
                    villager.gatheringWaypointForFood.WaypointStatus(avaliableWaypoint, false);
                    villager.Villager.SetDestination(avaliableWaypoint.transform.position);
                }
            }
            else
            {
                villager.SwitchState(villager.vil_IdelState);
            }
        }
        #endregion

        #region Find the closest waypoint of stone
        if (villager.currentCarryingResource == "Stone")
        {
            foreach (KeyValuePair<GameObject, bool> waypoint in villager.gatheringWaypointForStone.waypoints)
            {
                if (waypoint.Value == true)
                {
                    avaliableWaypoint = waypoint.Key;
                    conditionMet = true;
                    break;
                }
            }

            if (conditionMet == true)
            {
                /// Finish Finding the waypoint
                if (villager.gatheringWaypointForStone.waypoints[avaliableWaypoint] == true)
                {
                    villager.gatheringWaypointForStone.WaypointStatus(avaliableWaypoint, false);
                    villager.Villager.SetDestination(avaliableWaypoint.transform.position);
                }
            }
            else
            {
                villager.SwitchState(villager.vil_IdelState);
            }
        }
        #endregion
    }
}
