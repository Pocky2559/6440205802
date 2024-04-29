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
        if(otmGunner.targetPlayerUnitStat.unitHP > 0)
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
        
        if(otmGunner.targetPlayerUnitStat.unitHP <= 0) 
        {
            otmGunner.SwitchState(otmGunner.otmGunner_AttackWallState);
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
        return;
    }

    public override void OnTriggerExit(OttomanGunnerRecruitStateController otmGunner, Collider coll)
    {
        if (coll.CompareTag(otmGunner.targetPlayerUnit.tag))
        {
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
    public override void ExitState(OttomanGunnerRecruitStateController otmGunner)
    {
        Collider colliderOfThisEnemy = otmGunner.transform.parent.GetComponent<Collider>(); // collider of this enemy
        otmGunner.capturePointByEnemy.OnTriggerExit(colliderOfThisEnemy);
        MonoBehaviour.Destroy(otmGunner.transform.parent.gameObject); // Delete Villager from the game
    }
}
