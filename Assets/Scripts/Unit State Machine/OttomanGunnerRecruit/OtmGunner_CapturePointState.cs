using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmGunner_CapturePointState : OttomanGunnerRecruitBaseState
{
    bool isDead;
    public override void EnterState(OttomanGunnerRecruitStateController otmGunner)
    {
        //=============================
        //Play animation otmGunner_Walking
        //=============================
        otmGunner.otmGunnerAnimatorController.SetTrigger("Walk");
        otmGunner.rigBuilder.enabled = true;
        //-------------------------------------------------------

        //=============================
        //Gun Holding Position
        //=============================
        otmGunner.Gun.transform.localPosition = new Vector3(0.254000008f, 1.18599999f, 0.324000001f);
        otmGunner.Gun.transform.localRotation = Quaternion.Euler(357.268738f, 122.092773f, 359.583221f);
        //-------------------------------------------------------------------------------------------
    }

    public override void UpdateState(OttomanGunnerRecruitStateController otmGunner)
    {
        if(otmGunner.unitStat.unitHP > 0)
        {
            otmGunner.otmGunnerAgent.isStopped = false;
            otmGunner.otmGunnerAgent.SetDestination(otmGunner.capturePointByEnemy.transform.position);
        }

        #region Switch to ExitState
        if (otmGunner.unitStat.unitHP <= 0 && isDead == false)
        {
            isDead = true;
            otmGunner.soundEffectController.PlayUnitDiedSound(); //Play UnitDie Sound
            ExitState(otmGunner);
        }
        #endregion
    }

    public override void OnTriggerStay(OttomanGunnerRecruitStateController otmGunner, Collider coll)
    {
        if (coll.CompareTag("Gunner")
            || coll.CompareTag("Landsknecht")
            || coll.CompareTag("Villager")
            || coll.CompareTag("Captain")
            || coll.CompareTag("Kartouwe"))
        {
            otmGunner.targetPlayerUnitStat = coll.GetComponentInParent<UnitStat>();
            otmGunner.targetPlayerUnit = coll.gameObject;
            otmGunner.SwitchState(otmGunner.otmGunner_AttackPlayerUnitState);
        }
    }

    public override void OnTriggerExit(OttomanGunnerRecruitStateController otmGunner, Collider coll)
    {
        return;
    }
    public override void ExitState(OttomanGunnerRecruitStateController otmGunner)
    {
        //=============================
        //Play animation otmGunner_Death
        //=============================
        otmGunner.otmGunnerAnimatorController.SetTrigger("Death");
        otmGunner.rigBuilder.enabled = false;
        otmGunner.Gun.SetActive(false);
        otmGunner.otmGunnerAgent.isStopped = true;
        otmGunner.otmGunnerAgent.enabled = false;
        //-------------------------------------------------------

        Collider colliderOfThisEnemy = otmGunner.transform.parent.GetComponent<Collider>(); // collider of this enemy
        colliderOfThisEnemy.enabled = false;
        otmGunner.capturePointByEnemy.OnTriggerExit(colliderOfThisEnemy); //Decrease population
        MonoBehaviour.Destroy(otmGunner.transform.parent.gameObject, 4f); // Delete Ottoman Gunner from the game
    }
}
