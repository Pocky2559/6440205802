using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmRecruit_CapturePointState : OttomanRecruitBaseState
{
    bool isDead;
    public override void EnterState(OttomanRecruitStateController otmRecruit)
    {
        //===================================
        //Play animation otmRecruit_Walking
        //===================================
        otmRecruit.otmRecruitAnimatorController.SetTrigger("Walk");
        //-----------------------------------

        if(otmRecruit.capturePointByEnemy != null)
        {
           //otmRecruit.otmRecruitAgent.isStopped = false;
           otmRecruit.otmRecruitAgent.SetDestination(otmRecruit.capturePointByEnemy.transform.position);
        }

        else
        {
            //===================================
            //Play animation otmRecruit_Idle
            //===================================
            otmRecruit.otmRecruitAnimatorController.SetTrigger("Idle");
            //-----------------------------------
        }

        otmRecruit.soundEffectController.StopPlaySound();//Stop Play Sound

    }

    public override void UpdaterState(OttomanRecruitStateController otmRecruit)
    {
        if(otmRecruit.unitStat.unitHP > 0)
        {
            otmRecruit.otmRecruitAgent.isStopped = false;
        }

        #region Switch to ExitState
        if (otmRecruit.unitStat.unitHP <= 0 && isDead == false)
        {
            isDead = true;
            otmRecruit.soundEffectController.PlayUnitDiedSound(); //Play UnitDie Sound
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
        otmRecruit.otmRecruitAgent.enabled = false;
        Collider colliderOfThisEnemy = otmRecruit.transform.parent.GetComponent<Collider>(); // collider of this enemy
        colliderOfThisEnemy.enabled = false;
        if(otmRecruit.capturePointByEnemy != null) otmRecruit.capturePointByEnemy.OnTriggerExit(colliderOfThisEnemy);
        MonoBehaviour.Destroy(otmRecruit.transform.parent.gameObject, 4); // Delete Villager from the game
    }
}
