using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmGunner_AttackPlayerUnitState : OttomanGunnerRecruitBaseState
{
    float lastShotTime = 0.0f;
    public override void EnterState(OttomanGunnerRecruitStateController otmGunner)
    {
        otmGunner.otmGunnerAgent.isStopped = true;
    }

    public override void UpdateState(OttomanGunnerRecruitStateController otmGunner)
    {
        if(otmGunner.targetPlayerUnit != null)
        {
            otmGunner.rootGameObject.transform.LookAt(otmGunner.targetPlayerUnit.transform.position);

            #region Shooting at player
            if (Time.time > lastShotTime + otmGunner.unitStat.unitAttackSpeed)
            {
                RaycastHit hit;
                if (Physics.Raycast(otmGunner.transform.position, otmGunner.transform.forward, out hit, Mathf.Infinity, otmGunner.targetLayerMask)) // cast ray
                {
                    Debug.DrawRay(otmGunner.transform.position, otmGunner.transform.forward * hit.distance, Color.red, 0.9f);
                    lastShotTime = Time.time;
                    TargetRecieveDamage(otmGunner, hit);
                }
            }
            #endregion
        }
        else
        {
            otmGunner.SwitchState(otmGunner.otmGunner_AttackWallState);
        }
    }

    public override void OnTriggerStay(OttomanGunnerRecruitStateController otmGunner, Collider coll)
    {
        return;
    }

    public override void OnTriggerExit(OttomanGunnerRecruitStateController otmGunner, Collider coll)
    {
        if (coll.CompareTag(otmGunner.targetPlayerUnit.tag))
        {
            Debug.Log(otmGunner.targetPlayerUnit.tag);
            otmGunner.targetPlayerUnit = null;
            otmGunner.otmGunnerAgent.isStopped = false;
            otmGunner.SwitchState(otmGunner.otmGunner_AttackWallState);
        }
    }
    public void TargetRecieveDamage(OttomanGunnerRecruitStateController otmGunner, RaycastHit hit) // calculate reciever hp that recieve damage from attacker
    {
        UnitStat recieverStat = hit.collider.GetComponent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (otmGunner.unitStat.unitDamage - recieverStat.unitRangedArmor); // HP of reciever = HP of reciever - Damage from attacker  - Ranged Armor of reciever
    }
}
