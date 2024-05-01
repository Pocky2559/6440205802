using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class IdelState : GunnerBaseState
{
    public override void EnterState(GunnerStateController gunner)
    {
          gunner.Gunner.isStopped = false;
        //Play animation Gunner_Idle
        gunner.gunnerAnimatorControlller.SetBool("isWalking", false);
        gunner.gunnerAnimatorControlller.SetBool("isShooting", false);
        gunner.Gun.transform.localPosition = new Vector3(0.254000008f, 1.18599999f, 0.324000001f);
        gunner.Gun.transform.localRotation = Quaternion.Euler(357.268738f, 122.092773f, 359.583221f);
        gunner.rigBuilder.enabled = true;
        //gunner.gunnerAnimatorControlller.SetBool("isEnemyDeadWhileReload", false);
        //
    }

    public override void UpdateState(GunnerStateController gunner)
    {
        #region Switch to Moving State & Chasing State

        if (gunner.unitSelection.unitSelected.Contains(gunner.rootGameObject) && Input.GetMouseButtonDown(1)) //if Gunner is moving then it will switch to MovingState
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit , Mathf.Infinity ,gunner.groundLayerMask))
            {
                if (hit.collider.CompareTag("Ground")
                   && !hit.collider.CompareTag("OttomanRecruit")
                   && !hit.collider.CompareTag("OttomanGunnerRecruit")
                   && !hit.collider.CompareTag("MeleeJanissary")
                   && !hit.collider.CompareTag("RangedJanissary")
                   && !hit.collider.CompareTag("OttomanCannon"))
                {
                   gunner.SwitchState(gunner.movingState);
                }
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
                    gunner.selectedEnemyStat = hit.collider.GetComponentInParent<UnitStat>();
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
            gunner.selectedEnemyStat = coll.GetComponentInParent<UnitStat>();
            gunner.selectedEnemy = coll.gameObject;
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
        //Play animation Gunner_Death
          gunner.Gunner.enabled = false;
          gunner.gunnerAnimatorControlller.SetBool("isDead" ,true);
          gunner.Gun.SetActive(false);
          gunner.rigBuilder.enabled = false;
          gunner.gunnerCollider.enabled = false;
        //
        gunner.population.PopulationChanges(-1 * gunner.unitStat.unitPopulation); //Decrease population
        MonoBehaviour.Destroy(gunner.transform.parent.gameObject, 4); // Delete Villager from the game
    }

}
