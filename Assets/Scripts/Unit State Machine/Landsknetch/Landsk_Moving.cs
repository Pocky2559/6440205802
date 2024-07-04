using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Landsk_Moving : LandsknetchBaseState
{
    bool isDead;
    public override void EnterState(LandsknetchStateController landsknetch)
    {
        landsknetch.landsknetchAgent.isStopped = false;
        landsknetch.neutralSword.SetActive(true);
        landsknetch.attackedSword.SetActive(false);
        landsknetch.soundEffectController.PlayWalkingSound();//Play Walking Sound
    }

    public override void UpdaterState(LandsknetchStateController landsknetch)
    {
        #region Switch to Idel State
        if ( landsknetch.landsknetchAgent.pathPending == false) // if Unity finish calculating the path
        {
            if (Mathf.RoundToInt(landsknetch.landsknetchAgent.remainingDistance) == 0) //if remaining distance of landskn = 0
            {
                //===================================
                //Play animation Landsknecht_Idle
                //===================================
                landsknetch.landskAnimatorControlller.SetTrigger("Idle");
                //-----------------------------------

                landsknetch.SwitchState(landsknetch.land_IdelState); // Switch to moving state
            }
        }
        #endregion

        #region Change moving or Switch to Chasing State
        if (landsknetch.unitSelection.unitSelected.Contains(landsknetch.rootGameObject) && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Moving Logic control by outside script name "UnitFormation"

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, landsknetch.targetLayerMask))
            {
                //===================================
                //Play animation Landsknecht_Walking
                //===================================
                landsknetch.landskAnimatorControlller.SetTrigger("Walk");
                //-----------------------------------

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
            landsknetch.soundEffectController.PlayUnitDiedSound(); // PLay UnitDie Sound
            ExitState(landsknetch);
        }
        #endregion
    }

    public override void OnTriggerStay(LandsknetchStateController landsknetch, Collider coll)
    {
        return;
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

        landsknecht.landsknetchAgent.isStopped = true;
        landsknecht.landskCollider.enabled = false;
        landsknecht.population.PopulationChanges(-1 * landsknecht.unitStat.unitPopulation); //Decrease population
        MonoBehaviour.Destroy(landsknecht.transform.parent.gameObject,4); // Delete Villager from the game
    }
}
