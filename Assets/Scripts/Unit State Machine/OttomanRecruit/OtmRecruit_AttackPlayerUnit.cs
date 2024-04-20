using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using static UnityEngine.GraphicsBuffer;

public class OtmRecruit_AttackPlayerUnit : OttomanRecruitBaseState
{
    float lastShotTime = 0.0f;
    public override void EnterState(OttomanRecruitStateController otmRecruit)
    {
        otmRecruit.otmRecruitAgent.SetDestination(otmRecruit.targetPlayerUnit.transform.position);
    }

    public override void UpdaterState(OttomanRecruitStateController otmRecruit)
    {
        if(otmRecruit.targetPlayerUnit!= null)
        {
            float distanceOfOttomanRecruitAndTargetPlayerUnit = Vector3.Distance(otmRecruit.otmRecruitAgent.transform.position, otmRecruit.targetPlayerUnit.transform.position); // the distance between Ottoman recruit and selected player unit

            if (distanceOfOttomanRecruitAndTargetPlayerUnit <= 2.0f)
            {
                otmRecruit.otmRecruitAgent.isStopped = true;

                #region Attack
                otmRecruit.transform.parent.LookAt(otmRecruit.targetPlayerUnit.transform.root); // make gunner face at target enemy

                if (Time.time > lastShotTime + otmRecruit.unitStat.unitAttackSpeed)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(otmRecruit.transform.position, (otmRecruit.targetPlayerUnit.transform.position - otmRecruit.transform.position).normalized, out hit, Mathf.Infinity, otmRecruit.targetLayerMask)) // cast ray
                    {
                        Debug.Log("Cast Ray");
                        Debug.DrawRay(otmRecruit.transform.position, otmRecruit.transform.forward * hit.distance, Color.red, 2);
                        lastShotTime = Time.time;

                        TargetRecieveDamage(otmRecruit, hit);
                    }
                }
                #endregion
            }

            if (distanceOfOttomanRecruitAndTargetPlayerUnit > 2.0f) // if the enemy is far than 9.8 f
            {
                otmRecruit.otmRecruitAgent.isStopped = false;
                otmRecruit.otmRecruitAgent.SetDestination(otmRecruit.targetPlayerUnit.transform.position);
            }
        }

        #region Switch to Capture point state
        if (otmRecruit.targetPlayerUnit == null) // if the selected enemy is dead
        {
            otmRecruit.otmRecruitAgent.isStopped = false; // make the gunner stop
            otmRecruit.otmRecruitAgent.SetDestination(otmRecruit.rootGameObject.transform.position);
            otmRecruit.targetPlayerUnit = null; // set the selected enemy as null
            Debug.Log("Switch from Chasing state to Idel state");
            otmRecruit.SwitchState(otmRecruit.otmRecruit_CapturePointState); // switch to idel state
        }
        #endregion
    }
    public override void OnTriggerStay(OttomanRecruitStateController otmRecruit, Collider coll)
    {
        return;
    }

    public override void OnTriggerExit(OttomanRecruitStateController otmRecruit, Collider coll)
    {
       /* if (coll.CompareTag("Gunner") || coll.CompareTag("Landsknecht") || coll.CompareTag("Villager"))
        {
            otmRecruit.SwitchState(otmRecruit.otmRecruit_CapturePointState);
        }*/
    }

    public void TargetRecieveDamage(OttomanRecruitStateController otmRecruit, RaycastHit hit) // calculate reciever hp that recieve damage from attacker
    {
        UnitStat recieverStat = hit.collider.GetComponent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (otmRecruit.unitStat.unitDamage - recieverStat.unitRangedArmor); // HP of reciever = HP of reciever - Damage from attacker  - Ranged Armor of reciever
    }
}
