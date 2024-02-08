using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class OtmRecruit_AttackWallState : OttomanRecruitBaseState
{
    private float lastShotTime = 0.0f;
    float distanceOfEnemyAndWall;
    public override void EnterState(OttomanRecruitStateController otmRecruit)
    {
        if (GameObject.FindGameObjectWithTag("PalisadeGate") != null) // if wall is still there
        {
            otmRecruit.otmRecruitAgent.SetDestination(otmRecruit.Wall.transform.position);
        }

        else
        {
            otmRecruit.SwitchState(otmRecruit.otmRecruit_CapturePointState);
        }

    }

    public override void UpdaterState(OttomanRecruitStateController otmRecruit)
    {
        if (GameObject.FindGameObjectWithTag("PalisadeGate") != null) // if wall is still there
        {
            distanceOfEnemyAndWall = Vector3.Distance(otmRecruit.otmRecruitAgent.transform.position, otmRecruit.Wall.transform.position);
            if (distanceOfEnemyAndWall <= 3)
            {
                otmRecruit.otmRecruitAgent.isStopped = true;

                if (Time.time > lastShotTime + otmRecruit.unitStat.unitAttackSpeed)
                {
                    Debug.Log("Starting to attack wall");
                    Attack(otmRecruit);
                    lastShotTime = Time.time;
                }
            }
        }
        else
        {
            otmRecruit.SwitchState(otmRecruit.otmRecruit_CapturePointState);
        }
    }

    public override void OnTriggerStay(OttomanRecruitStateController otmRecruit, Collider coll)
    {
        return;
    }

    public override void OnTriggerExit(OttomanRecruitStateController otmRecruit, Collider coll)
    {
        return;
    }

    public void Attack(OttomanRecruitStateController otmRecruit)
    {
        RaycastHit hit;
        if (Physics.Raycast(otmRecruit.transform.position, otmRecruit.transform.forward, out hit, Mathf.Infinity, otmRecruit.wallLayerMask))
        {
            Debug.DrawRay(otmRecruit.transform.position, otmRecruit.transform.forward * hit.distance, Color.red, 0.9f);
            Debug.Log("Attacking");
            TargetRecieveDamage(otmRecruit, hit);
        }
    }

    public void TargetRecieveDamage(OttomanRecruitStateController otmRecruit, RaycastHit hit) // calculate reciever hp that recieve damage from attacker
    {
        UnitStat recieverStat = hit.collider.GetComponent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (otmRecruit.unitStat.unitDamage - recieverStat.unitMeleeArmor); // HP of reciever = HP of reciever - Damage from attacker  - Ranged Armor of reciever
    }
}
