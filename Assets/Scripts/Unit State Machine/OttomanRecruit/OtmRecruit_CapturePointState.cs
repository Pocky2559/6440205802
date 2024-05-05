using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmRecruit_CapturePointState : OttomanRecruitBaseState
{
    public override void EnterState(OttomanRecruitStateController otmRecruit)
    {
        //===================================
        //Play animation otmRecruit_Walking
        //===================================
        otmRecruit.otmRecruitAnimatorController.SetTrigger("Walk");
        //-----------------------------------

        otmRecruit.otmRecruitAgent.isStopped = false;
        otmRecruit.otmRecruitAgent.SetDestination(GameObject.FindGameObjectWithTag("CapturedPoint").transform.position);
    }

    public override void UpdaterState(OttomanRecruitStateController otmRecruit)
    {
        #region Switch to ExitState
        if (otmRecruit.unitStat.unitHP <= 0)
        {
            ExitState(otmRecruit);
        }
        #endregion
    }

    public override void OnTriggerStay(OttomanRecruitStateController otmRecruit, Collider coll)
    {
        if( coll.CompareTag("Gunner")
            || coll.CompareTag("Landsknecht")
            || coll.CompareTag("Villager")
            || coll.CompareTag("Captain")
            || coll.CompareTag("Kartouwe"))
        {
            otmRecruit.targetPlayerUnit = coll.transform.gameObject;
            otmRecruit.targetPlayerUnitStat = otmRecruit.targetPlayerUnit.GetComponent<UnitStat>();
            otmRecruit.SwitchState(otmRecruit.otmRecruit_AttackPlayerUnitState);
        }
    }

    public override void OnTriggerExit(OttomanRecruitStateController otmRecruit, Collider coll)
    {
        return;
    }

    public override void ExitState(OttomanRecruitStateController otmRecruit)
    {
        //===============================
        //Play animation otmRecruit_Death
        //===============================
        otmRecruit.otmRecruitAnimatorController.SetTrigger("Death");
        //-------------------------------

        otmRecruit.otmRecruitAgent.isStopped = true;
        Collider colliderOfThisEnemy = otmRecruit.transform.parent.GetComponent<Collider>(); // collider of this enemy
        colliderOfThisEnemy.enabled = false;
        otmRecruit.capturePointByEnemy.OnTriggerExit(colliderOfThisEnemy);
        MonoBehaviour.Destroy(otmRecruit.transform.parent.gameObject, 4); // Delete Villager from the game
    }
}
