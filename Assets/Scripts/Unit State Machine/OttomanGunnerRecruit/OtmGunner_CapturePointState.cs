using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmGunner_CapturePointState : OttomanGunnerRecruitBaseState
{
    public override void EnterState(OttomanGunnerRecruitStateController otmGunner)
    {
        otmGunner.otmGunnerAgent.isStopped = false;
        otmGunner.otmGunnerAgent.SetDestination(GameObject.FindGameObjectWithTag("CapturedPoint").transform.position);
    }

    public override void UpdateState(OttomanGunnerRecruitStateController otmGunner)
    {
        #region Switch to ExitState
        if (otmGunner.unitStat.unitHP <= 0)
        {
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
            otmGunner.targetPlayerUnitStat = coll.GetComponent<UnitStat>();
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
        Collider colliderOfThisEnemy = otmGunner.transform.parent.GetComponent<Collider>(); // collider of this enemy
        otmGunner.capturePointByEnemy.OnTriggerExit(colliderOfThisEnemy);
        MonoBehaviour.Destroy(otmGunner.transform.parent.gameObject); // Delete Villager from the game
    }
}
