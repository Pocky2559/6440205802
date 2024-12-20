using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Landsk_Idel : LandsknetchBaseState
{
    bool isDead;
    public override void EnterState(LandsknetchStateController landsknetch)
    {
        
        landsknetch.landsknetchAgent.isStopped = false;
        landsknetch.neutralSword.SetActive(true);
        landsknetch.attackedSword.SetActive(false);
        landsknetch.soundEffectController.StopPlaySound();//Stop All Sound
    }

    public override void UpdaterState(LandsknetchStateController landsknetch)
    {
        #region Switch to Moving State
        if (landsknetch.unitSelection.unitSelected.Contains(landsknetch.rootGameObject) && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, landsknetch.groundLayerMask))
            {
                //===================================
                //Play animation Landsknecht_Walking
                //===================================
                  landsknetch.landskAnimatorControlller.SetTrigger("Walk");
                //-----------------------------------

                landsknetch.selectedPosition = hit.point;
               landsknetch.SwitchState(landsknetch.land_MovingState);

            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, landsknetch.targetLayerMask))
            {
                ////===================================
                ////Play animation Landsknecht_Walking
                ////===================================
                //landsknetch.landskAnimatorControlller.SetTrigger("Walk");
                ////-----------------------------------

                landsknetch.enemyStat = hit.collider.GetComponent<UnitStat>();
                landsknetch.targetEnemy = hit.collider.gameObject;
                landsknetch.SwitchState(landsknetch.land_ChasingState);
            }
        }
        #endregion

        #region Switch to ExitState
        if (landsknetch.unitStat.unitHP <= 0 && isDead == false)
        {
            isDead = true;
            landsknetch.soundEffectController.PlayUnitDiedSound(); //Play UnitDie SOund
            ExitState(landsknetch);
        }
        #endregion
    }

    public override void OnTriggerStay(LandsknetchStateController landsknetch, Collider coll)
    {
        Collider targetCollider = coll;
        #region Switch to Chasing State
        if (targetCollider.CompareTag("OttomanRecruit")
            || targetCollider.CompareTag("OttomanGunnerRecruit")
            || targetCollider.CompareTag("MeleeJanissary")
            || targetCollider.CompareTag("RangedJanissary")
            || targetCollider.CompareTag("OttomanCannon")) // if the enemy is in a detection area
        {
            landsknetch.enemyStat = targetCollider.GetComponent<UnitStat>();
            landsknetch.targetEnemy = coll.gameObject;
            landsknetch.SwitchState(landsknetch.land_ChasingState); // switch to chasing state
        }
        #endregion
    }

    public override void OnTriggerExit(LandsknetchStateController landsknetch, Collider coll)
    {
        return;
    }

    public override void ExitState(LandsknetchStateController landsknecht)
    {
        //===================================
        //Play animation Landsknecht_Walking
        //===================================
        landsknecht.landskAnimatorControlller.SetTrigger("Death");
        //-----------------------------------

        landsknecht.landskCollider.enabled = false;
        landsknecht.population.PopulationChanges(-1 * landsknecht.unitStat.unitPopulation); //Decrease population
        MonoBehaviour.Destroy(landsknecht.transform.parent.gameObject, 4); // Delete Villager from the game
    }
}
