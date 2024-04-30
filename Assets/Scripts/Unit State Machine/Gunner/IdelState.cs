using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class IdelState : GunnerBaseState
{
    public override void EnterState(GunnerStateController gunner)
    {
             gunner.Gunner.isStopped = false;
    }

    public override void UpdateState(GunnerStateController gunner)
    {
        #region Switch to Moving State & Chasing State

        if (gunner.unitSelection.unitSelected.Contains(gunner.rootGameObject) && Input.GetMouseButtonDown(1)) //if Gunner is moving then it will switch to MovingState
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit , Mathf.Infinity, gunner.groundLayerMask))
            {
                //Play animation Gunner_Walking
                gunner.gunnerAnimatorControlller.SetBool("isWalking", true);
                //
                gunner.SwitchState(gunner.movingState);
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("OttomanRecruit")
                      || hit.collider.CompareTag("OttomanGunnerRecruit")
                      || hit.collider.CompareTag("MeleeJanissary")
                      || hit.collider.CompareTag("RangedJanissary")
                      || hit.collider.CompareTag("OttomanCannon")) // if right click on the enemy
                {
                    gunner.selectedEnemy = hit.collider.gameObject;
                    Debug.Log("Switching from Idel State to Chasing state");
                    gunner.SwitchState(gunner.chasingState);
                }
            }
        }
        #endregion

        #region Switch to ExitState
        if(gunner.unitStat.unitHP <= 0)
        {
            ExitState(gunner);
        }
        #endregion
    }

    public override void OnTriggerStay(GunnerStateController gunner, Collider coll)
    {
        Collider targetCollider = coll;
        #region Switch to Shooting State
        if (targetCollider.CompareTag("OttomanRecruit")
            || targetCollider.CompareTag("OttomanGunnerRecruit")
            || targetCollider.CompareTag("MeleeJanissary")
            || targetCollider.CompareTag("RangedJanissary")
            || targetCollider.CompareTag("OttomanCannon")) // if the enemy is in a detection area
        {
            //Play animation Gunner_Shooting
            gunner.gunnerAnimatorControlller.SetBool("isShooting", true);
            //
            gunner.SwitchState(gunner.shootingState); // switch to shooting state
        }
        #endregion
    }
    public override void OnTriggerExit(GunnerStateController gunner, Collider coll)
    {
        return;
    }

    public override void ExitState(GunnerStateController gunner)
    {
        gunner.population.PopulationChanges(-1 * gunner.unitStat.unitPopulation); //Decrease population
        MonoBehaviour.Destroy(gunner.transform.parent.gameObject); // Delete Villager from the game
    }

}
