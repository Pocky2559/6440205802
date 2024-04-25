using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmGunner_AttackWallState : OttomanGunnerRecruitBaseState
{
    float lastShotTime = 0.0f;
    float distanceOfEnemyAndWall;
    public override void EnterState(OttomanGunnerRecruitStateController otmGunner)
    {
        if (GameObject.FindGameObjectWithTag("PalisadeGate") != null) // if wall is still there
        {
            otmGunner.otmGunnerAgent.SetDestination(otmGunner.Wall.transform.position);
            otmGunner.rootGameObject.transform.LookAt(otmGunner.Wall.transform.position);
        }

        else
        {
            otmGunner.SwitchState(otmGunner.otmGunner_CapturePointState);
        }

    }

    public override void UpdateState(OttomanGunnerRecruitStateController otmGunner)
    {
        if (GameObject.FindGameObjectWithTag("PalisadeGate") != null) // if wall is still there
        {
            distanceOfEnemyAndWall = Vector3.Distance(otmGunner.otmGunnerAgent.transform.position, otmGunner.Wall.transform.position);
            if (distanceOfEnemyAndWall <= 5)
            {
                otmGunner.otmGunnerAgent.isStopped = true;

                if (Time.time > lastShotTime + otmGunner.unitStat.unitAttackSpeed)
                {
                    Attack(otmGunner);
                    lastShotTime = Time.time;
                }
            }

            else
            {
                otmGunner.otmGunnerAgent.isStopped = false;
                otmGunner.otmGunnerAgent.SetDestination(otmGunner.Wall.transform.position);
            }
        }

        else
        {
            otmGunner.SwitchState(otmGunner.otmGunner_CapturePointState);
        }

        #region Switch to ExitState
        if (otmGunner.unitStat.unitHP <= 0)
        {
            ExitState(otmGunner);
        }
        #endregion
    }

    public override void OnTriggerStay(OttomanGunnerRecruitStateController otmGunner, Collider coll)
    {
        Collider target = coll;
        if(target.CompareTag("Gunner")
            || target.CompareTag("Landsknecht")
            || target.CompareTag("Villager")
            || target.CompareTag("Captain")
            || target.CompareTag("Kartouwe"))
        {
            otmGunner.targetPlayerUnit = coll.gameObject;
            otmGunner.SwitchState(otmGunner.otmGunner_AttackPlayerUnitState);
        }
    }

    public override void OnTriggerExit(OttomanGunnerRecruitStateController otmGunner, Collider coll)
    {
        return;
    }

    public void Attack(OttomanGunnerRecruitStateController otmGunner)
    {
        RaycastHit hit;
        if (Physics.Raycast(otmGunner.transform.position, otmGunner.transform.forward, out hit, Mathf.Infinity, otmGunner.wallLayerMask))
        {
            Debug.DrawRay(otmGunner.transform.position, otmGunner.transform.forward * hit.distance, Color.red, 0.9f);
            TargetRecieveDamage(otmGunner, hit);
        }
    }

    public void TargetRecieveDamage(OttomanGunnerRecruitStateController otmGunner, RaycastHit hit) // calculate reciever hp that recieve damage from attacker
    {
        UnitStat recieverStat = hit.collider.GetComponent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (otmGunner.unitStat.unitDamage - recieverStat.unitRangedArmor); // HP of reciever = HP of reciever - Damage from attacker  - Ranged Armor of reciever
    }
    public override void ExitState(OttomanGunnerRecruitStateController otmGunner)
    {
        Collider colliderOfThisEnemy = otmGunner.transform.parent.GetComponent<Collider>(); // collider of this enemy
        otmGunner.capturePointByEnemy.OnTriggerExit(colliderOfThisEnemy); //Decrease population
        MonoBehaviour.Destroy(otmGunner.transform.parent.gameObject); // Delete Villager from the game
    }
}
