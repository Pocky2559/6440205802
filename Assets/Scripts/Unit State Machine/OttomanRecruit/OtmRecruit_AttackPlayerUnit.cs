using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using static UnityEngine.GraphicsBuffer;

public class OtmRecruit_AttackPlayerUnit : OttomanRecruitBaseState
{
    float lastShotTime = 0.0f;
    float distanceOfOttomanRecruitAndTargetPlayerUnit;
    public override void EnterState(OttomanRecruitStateController otmRecruit)
    {
        otmRecruit.otmRecruitAgent.SetDestination(otmRecruit.targetPlayerUnit.transform.position);
    }

    public override void UpdaterState(OttomanRecruitStateController otmRecruit)
    {
        if (otmRecruit.targetPlayerUnitStat.unitHP > 0  && otmRecruit.unitStat.unitHP > 0)
        {
            distanceOfOttomanRecruitAndTargetPlayerUnit = Vector3.Distance(otmRecruit.transform.parent.position, otmRecruit.targetPlayerUnit.transform.position); // the distance between Ottoman recruit and selected player unit
            if (distanceOfOttomanRecruitAndTargetPlayerUnit <= 1.5f) // if enemy is in attack ranged
            {
                otmRecruit.otmRecruitAgent.isStopped = true;

                //===================================
                //Play animation otmRecruit_Attacking
                //===================================
                otmRecruit.otmRecruitAnimatorController.ResetTrigger("Walk");
                otmRecruit.otmRecruitAnimatorController.SetBool("Attack", true);
                //-----------------------------------

                #region Attack
                Vector3 positionToAim = otmRecruit.targetPlayerUnit.transform.position;
                positionToAim.y = otmRecruit.transform.parent.position.y;
                otmRecruit.transform.parent.LookAt(positionToAim); // make gunner face at target enemy

                if (Time.time > lastShotTime + otmRecruit.unitStat.unitAttackSpeed)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(otmRecruit.transform.position, (otmRecruit.targetPlayerUnit.transform.position - otmRecruit.transform.position).normalized, out hit, Mathf.Infinity, otmRecruit.targetLayerMask)) // cast ray
                    {
                        Debug.DrawRay(otmRecruit.transform.position, otmRecruit.transform.forward * hit.distance, Color.red, 2);
                        lastShotTime = Time.time;

                        TargetRecieveDamage(otmRecruit, hit);
                    }
                }
                #endregion
            }

            else if (distanceOfOttomanRecruitAndTargetPlayerUnit > 1.5f) // if the enemy is out of attack ranged
            {
                //===================================
                //Play animation otmRecruit_Walking
                //===================================
                otmRecruit.otmRecruitAnimatorController.SetTrigger("Walk");
                otmRecruit.otmRecruitAnimatorController.SetBool("Attack", false);
                //-----------------------------------

                otmRecruit.otmRecruitAgent.isStopped = false;
                otmRecruit.otmRecruitAgent.SetDestination(otmRecruit.targetPlayerUnit.transform.position);
            }
        }

        #region Switch to Capture point state
        if (otmRecruit.targetPlayerUnitStat.unitHP <= 0) // if the selected enemy is dead
        {
            //===================================
            //Play animation otmRecruit_Idle
            //===================================
            otmRecruit.otmRecruitAnimatorController.SetBool("Attack", false);
            //-----------------------------------

            otmRecruit.otmRecruitAgent.isStopped = false; // make the gunner stop
            otmRecruit.otmRecruitAgent.SetDestination(otmRecruit.rootGameObject.transform.position);
            otmRecruit.targetPlayerUnit = null; // set the selected enemy as null
            otmRecruit.SwitchState(otmRecruit.otmRecruit_CapturePointState); // switch to idel state
        }
        #endregion

        #region Switch to ExitState
        if (otmRecruit.unitStat.unitHP <= 0)
        {
            ExitState(otmRecruit);
        }
        #endregion
    }
    public override void OnTriggerStay(OttomanRecruitStateController otmRecruit, Collider coll)
    {
        return;
    }

    public override void OnTriggerExit(OttomanRecruitStateController otmRecruit, Collider coll)
    {
       //if (coll.gameObject == otmRecruit.targetPlayerUnit.gameObject)
       // {
       //     otmRecruit.SwitchState(otmRecruit.otmRecruit_CapturePointState);
       // }
    }

    public void TargetRecieveDamage(OttomanRecruitStateController otmRecruit, RaycastHit hit) // calculate reciever hp that recieve damage from attacker
    {
        UnitStat recieverStat = hit.collider.GetComponent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (otmRecruit.unitStat.unitDamage - recieverStat.unitRangedArmor); // HP of reciever = HP of reciever - Damage from attacker  - Ranged Armor of reciever
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
        if(otmRecruit.capturePointByEnemy != null) otmRecruit.capturePointByEnemy.OnTriggerExit(colliderOfThisEnemy);
        MonoBehaviour.Destroy(otmRecruit.transform.parent.gameObject, 4); // Delete Villager from the game
    }
}
