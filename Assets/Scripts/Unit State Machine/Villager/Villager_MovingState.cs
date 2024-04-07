using UnityEngine;
using UnityEngine.Analytics;

public class Villager_MovingState : VillagerBaseState
{
    public override void EnterState(VillagerStateController villager)
    {
        Debug.Log("Villager is Moving");
        //villager.Villager.SetDestination(villager.selectedPosition); // villager will move to the selected position
    }
    public override void UpdateState(VillagerStateController villager)
    {

        // Moving Logic Control by outside script name "UnitFormation"
        
        if (villager.unitSelection.unitSelected.Contains(villager.gameObject) && Input.GetMouseButtonDown(1)) // if we select villager and right clicke
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            #region Switch to Gathering State 

            // If click on the resources, it will switch to "vil_GatheringState"
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, villager.resorcesLayerMask)) // if the ray hit the resources layermask
            {
                if (hit.collider.CompareTag("Wood")) // if ray hit the wood
                {
                    villager.targetResources = hit.collider.gameObject; // target resources game object
                    villager.currentCarryingResource = "Wood";
                    villager.gatheringAmount = 0;
                    villager.SwitchState(villager.vil_GatheringState);
                }

                if (hit.collider.CompareTag("Gold")) // if ray hit the gold
                {
                    villager.targetResources = hit.collider.gameObject; // target resources game object
                    villager.currentCarryingResource = "Gold";
                    villager.gatheringAmount = 0;
                    villager.SwitchState(villager.vil_GatheringState);
                }

                if (hit.collider.CompareTag("Stone")) // if ray hit the stone
                {
                    villager.targetResources = hit.collider.gameObject;
                    villager.currentCarryingResource = "Stone";
                    villager.gatheringAmount = 0;
                    villager.SwitchState(villager.vil_GatheringState);
                }

                if (hit.collider.CompareTag("Food")) // if ray hit the food
                {
                    villager.targetResources = hit.collider.gameObject;
                    villager.currentCarryingResource = "Food";
                    villager.gatheringAmount = 0;
                    villager.SwitchState(villager.vil_GatheringState);
                }
            }
            #endregion

            #region Switch to Storing State
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) // if right click on something
            {
                if (hit.collider.CompareTag("Town Center")) // if it hit Town Center
                {
                    if (villager.currentCarryingResource != "")
                    {
                        villager.isStoringManual = true;
                        villager.selectedStoringPoint = hit.collider.gameObject;
                        villager.SwitchState(villager.vil_StoringState);
                    }
                }

                if (hit.collider.CompareTag("Wood Storage")) // if it hit Lumber Camp
                {
                    if (villager.currentCarryingResource != "")
                    {
                        villager.isStoringManual = true;
                        villager.selectedStoringPoint = hit.collider.gameObject;
                        villager.SwitchState(villager.vil_StoringState);
                    }
                }
            }
            #endregion
        }

        #region Switch to Idel State

        // if he is standing still, they will enter "vil_IdelState"

        if(villager.Villager.pathPending == false) // if unity finish calculating the path
        {
             if (Mathf.RoundToInt(villager.Villager.remainingDistance) == 0) //if distance between villager and destination are 0 
             {
                  Debug.Log("Switching from Moving state to Idel state");
                  villager.SwitchState(villager.vil_IdelState); // Switch to moving state
             }
        }
        #endregion
    }
}
