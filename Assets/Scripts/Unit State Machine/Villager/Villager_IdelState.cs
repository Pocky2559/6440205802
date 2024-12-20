using UnityEngine;
using UnityEngine.Analytics;
using static UnityEngine.UI.Image;

public class Villager_IdelState : VillagerBaseState
{
    bool isDead;

    public override void EnterState(VillagerStateController villager)
    {
        // Enter IdelState
        villager.Villager.enabled = true;
        villager.Villager.isStopped = true;

        //Stop All Sound
        villager.soundEffectController.StopPlaySound();

        if(villager.gatheringAmount > 0 ) //if villager carrying a resource
        {
            villager.villagerAnimator.SetBool("isWalking", false);
            villager.basket.SetActive(true); //show basket
            villager.rigBuilder.enabled = true; //make villager do hollding action

        }

        else
        {
            villager.villagerAnimator.SetBool("isWalking",false);
            villager.basket.SetActive(false); //hide basket
            villager.rigBuilder.enabled = false; //make villager stop hollding action
        }
    }
    public override void UpdateState(VillagerStateController villager)
    {
        // villager will stand still if they are idel

        // If we select villager and click right mouse button
        if (villager.unitSelection.unitSelected.Contains(villager.gameObject) && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            #region Switch to moving state 
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Ground")
                    && !hit.collider.CompareTag("Wood")
                    && !hit.collider.CompareTag("Food")
                    && !hit.collider.CompareTag("Gold")
                    && !hit.collider.CompareTag("Stone"))
                {
                    villager.SwitchState(villager.vil_MovingState);
                }
            }
            #endregion

            #region Switch to Gathering State 

            // If click on the resources, it will switch to "vil_GatheringState"
            if (Physics.Raycast(ray, out hit , Mathf.Infinity ,villager.resorcesLayerMask)) // if the ray hit the resources layermask
            {
                if (hit.collider.CompareTag("Wood")) // if ray hit the wood
                {
                    villager.gatheringWaypointForTree = hit.collider.GetComponent<GatheringWaypointForTree>();
                    villager.targetResources = hit.collider.gameObject; // target resources game object
                    villager.currentCarryingResource = "Wood";
                    villager.gatheringAmount = 0;
                    villager.SwitchState(villager.vil_GatheringState);
                }

                if (hit.collider.CompareTag("Gold")) // if ray hit the gold
                {
                    villager.gatheringWaypointForGold = hit.collider.GetComponent<GatheringWaypointForGold>();
                    villager.targetResources = hit.collider.gameObject; // target resources game object
                    villager.currentCarryingResource = "Gold";
                    villager.gatheringAmount = 0;
                    villager.SwitchState(villager.vil_GatheringState);
                }

                if (hit.collider.CompareTag("Stone")) // if ray hit the stone
                {
                    villager.gatheringWaypointForStone = hit.collider.GetComponent<GatheringWaypointForStone>();
                    villager.targetResources = hit.collider.gameObject;
                    villager.currentCarryingResource = "Stone";
                    villager.gatheringAmount = 0;
                    villager.SwitchState(villager.vil_GatheringState);
                }

                if (hit.collider.CompareTag("Food")) // if ray hit the food
                {
                    villager.gatheringWaypointForFood = hit.collider.GetComponent<GatheringWaypointForFood>();
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
                    if(villager.currentCarryingResource != "")
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
        //Play animation Villager_Death
        villager.villagerAnimator.SetBool("isDead", true);
        villager.basket.SetActive(false);
        villager.rigBuilder.enabled = false;
        //

        //Play UnitDie Sound
        villager.soundEffectController.PlayUnitDiedSound();

        villager.population.PopulationChanges(-1 * villager.unitStat.unitPopulation); //Decrease population
        villager.villagerCollider.enabled = false; // disable collider to stop enemy detect this unit
        MonoBehaviour.Destroy(villager.gameObject, 4); // Delete Villager from the game
    }
}
