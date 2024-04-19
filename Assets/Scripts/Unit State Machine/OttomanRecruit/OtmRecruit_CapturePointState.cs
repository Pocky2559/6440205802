using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmRecruit_CapturePointState : OttomanRecruitBaseState
{
    public override void EnterState(OttomanRecruitStateController otmRecruit)
    {
        Debug.Log("Go capture point");
        otmRecruit.otmRecruitAgent.isStopped = false;
        otmRecruit.otmRecruitAgent.SetDestination(GameObject.FindGameObjectWithTag("CapturedPoint").transform.position);
    }

    public override void UpdaterState(OttomanRecruitStateController otmRecruit)
    {
        return;
    }

    public override void OnTriggerStay(OttomanRecruitStateController otmRecruit, Collider coll)
    {
        Collider target = coll;
        if( target.CompareTag("Gunner")
            || target.CompareTag("Landsknecht")
            || target.CompareTag("Villager")
            || target.CompareTag("Captain")
            || target.CompareTag("Kartouwe"))
        {
            otmRecruit.targetPlayerUnit = target.gameObject;
            otmRecruit.SwitchState(otmRecruit.otmRecruit_AttackPlayerUnitState);
        }
    }

    public override void OnTriggerExit(OttomanRecruitStateController otmRecruit, Collider coll)
    {
        return;
    }
}
