using UnityEngine;
using UnityEngine.Analytics;

public class Villager_GatheringState : VillagerBaseState
{
    float lastShotTime = 0.0f;
    private int gatheringCapacity;
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

    public override void EnterState(VillagerStateController villager)
    {
        Debug.Log("Villager is gathering resources");
        villager.Villager.SetDestination(villager.targetResources.transform.position);

        //Is Upgrade
        isWoodGatheringSpeedUpgrade = villager.isLumberCampGatheringSpeedUpgrade;
        isWoodGatheringCapacityUpgrade = villager.isLumberCampGatheringCapacityUpgrade;

        isGoldStoneGatheringSpeedUpgrade = villager.isMiningCartGatheringSpeedUpgrade;
        isGoldStoneGatheringCapacityUpgrade = villager.isMiningCartGatheringCapacityUpgrade;

        isFoodGatheringSpeedUpgrade = villager.isWindMillGatheringSpeedUpgrade;
        isFoodGatheringCapacityUpgrade = villager.isWindMillGatheringCapacityUpgrade;

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
        #region Is villager reach the target resources?
        // if the villager reach the target resources
        if (villager.Villager.remainingDistance < 1)
        {
            villager.Villager.isStopped = true; // villager will stop
            GatherResources(villager);
        }
        #endregion

        #region If select another resources
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
                villager.Villager.isStopped = false; // make villager can move before changing state
                villager.isStoringManual = false;
                villager.SwitchState(villager.vil_StoringState);
            }
        }

        if (villager.currentCarryingResource == "Gold")
        {
            if (villager.gatheringAmount == villager.goldStoneCarryingCapacity) // if villager finish gather resources, he will change to "vil_StoringState"
            {
                villager.Villager.isStopped = false; // make villager can move before changing state
                villager.isStoringManual = false;
                villager.SwitchState(villager.vil_StoringState);
            }
        }

        if (villager.currentCarryingResource == "Stone")
        {
            if (villager.gatheringAmount == villager.goldStoneCarryingCapacity) // if villager finish gather resources, he will change to "vil_StoringState"
            {
                villager.Villager.isStopped = false; // make villager can move before changing state
                villager.isStoringManual = false;
                villager.SwitchState(villager.vil_StoringState);
            }
        }

        if (villager.currentCarryingResource == "Food")
        {
            if(villager.gatheringAmount == villager.foodCarryingCapacity)
            {
                villager.Villager.isStopped = false;
                villager.isStoringManual = false;
                villager.SwitchState(villager.vil_StoringState);
            }
        }
        #endregion
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

    public void ChangeTargetResources(VillagerStateController villager)
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

        #region Change target resources
        /*if (Physics.Raycast(ray, out hit, Mathf.Infinity, villager.resorcesLayerMask))
            {
                if (hit.collider.gameObject) // if select the same game object, it will have nothing happen.
                {
                villager.gatheringAmount = 0;
                }
                if (hit.collider.gameObject.CompareTag("Wood") && hit.collider.gameObject!= villager.targetResources) // if select wood and it not the same object that you clicked.
                {
                    if (villager.currentCarryingResource != "Wood") // if the new target resources not wood it will start new count
                    {
                        villager.gatheringAmount = 0;
                    }

                    villager.targetResources = hit.collider.gameObject; // assign new target resources
                    villager.currentCarryingResource = "Wood"; // assign new name of resources
                    villager.Villager.isStopped = false; // make villager can move
                    villager.Villager.SetDestination(hit.point); // set destination for villager
                }

                if (hit.collider.gameObject.CompareTag("Gold") && villager.currentCarryingResource != villager.currentTask) // if select Gold and it not the same object that you clicked.
                {
                   if (villager.currentCarryingResource != "Gold") // if the new target resources not Gold it will start new count
                {
                     villager.gatheringAmount = 0;
                   }

                   villager.targetResources = hit.collider.gameObject; // assign new target resources
                   villager.currentCarryingResource = "Gold"; // assign new name of resources
                   villager.Villager.isStopped = false;// make villager can move
                villager.Villager.SetDestination(hit.point);// set destination for villager
            }

                if (hit.collider.gameObject.CompareTag("Stone") && hit.collider.gameObject != villager.targetResources) // if select stone and it not the same object that you clicked.
                {
                   if (villager.currentCarryingResource != "Stone") // if the new target resources not stone it will start new count
                {
                     villager.gatheringAmount = 0;
                   }

                   villager.targetResources = hit.collider.gameObject; // assign new target resources
                   villager.currentCarryingResource = "Stone"; // assign new name of resources
                   villager.Villager.isStopped = false;// make villager can move
                villager.Villager.SetDestination(hit.point);// set destination for villager
            }

            if (hit.collider.gameObject.CompareTag("Food") && hit.collider.gameObject != villager.targetResources) // if select food and it not the same object that you clicked.
            {
                if (villager.currentCarryingResource != "Food") // if the new target resources not food it will start new count
                {
                    villager.gatheringAmount = 0;
                }

                villager.targetResources = hit.collider.gameObject; // assign new target resources
                villager.currentCarryingResource = "Food"; // assign new name of resources
                villager.Villager.isStopped = false;// make villager can move
                villager.Villager.SetDestination(hit.point);// set destination for villager
            }

        }*/
        #endregion

        #region Switch to Moving state

        if(villager.unitSelection.unitSelected.Contains(villager.gameObject) && Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, villager.groundLayerMask))
            {
                villager.Villager.isStopped = false; // make villager can move
                villager.selectedPosition = hit.point; // assign the position where village will move to
                villager.SwitchState(villager.vil_MovingState); // change state
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
                    villager.Villager.SetDestination(hit.collider.transform.position); // go to that storing point
                    villager.SwitchState(villager.vil_StoringState);
                }
            }
        }
        #endregion
    }
}
