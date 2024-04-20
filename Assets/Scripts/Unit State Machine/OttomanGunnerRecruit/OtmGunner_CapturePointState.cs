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
        return;
    }

    public override void OnTriggerStay(OttomanGunnerRecruitStateController otmGunner, Collider coll)
    {
        if (coll.CompareTag("Gunner")
            || coll.CompareTag("Landsknecht")
            || coll.CompareTag("Villager")
            || coll.CompareTag("Captain")
            || coll.CompareTag("Kartouwe"))
        {
            otmGunner.targetPlayerUnit = coll.gameObject;
            otmGunner.SwitchState(otmGunner.otmGunner_AttackPlayerUnitState);
        }
    }

    public override void OnTriggerExit(OttomanGunnerRecruitStateController otmGunner, Collider coll)
    {
        return;
    }
}
