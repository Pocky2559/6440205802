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
        #region Switch to ExitState
        if (otmRecruit.unitStat.unitHP <= 0)
        {
            ExitState(otmRecruit);
        }
        #endregion
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
            otmRecruit.targetPlayerUnitStat = coll.GetComponent<UnitStat>();
            otmRecruit.targetPlayerUnit = target.gameObject;
            otmRecruit.SwitchState(otmRecruit.otmRecruit_AttackPlayerUnitState);
        }
    }

    public override void OnTriggerExit(OttomanRecruitStateController otmRecruit, Collider coll)
    {
        return;
    }

    public override void ExitState(OttomanRecruitStateController otmRecruit)
    {
        Collider colliderOfThisEnemy = otmRecruit.transform.parent.GetComponent<Collider>(); // collider of this enemy
        otmRecruit.capturePointByEnemy.OnTriggerExit(colliderOfThisEnemy);
        MonoBehaviour.Destroy(otmRecruit.transform.parent.gameObject); // Delete Villager from the game
    }
}
