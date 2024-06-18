using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (otmGunner.targetPlayerUnitStat.unitHP > 0 && otmGunner.unitStat.unitHP > 0)
        {
            Vector3 positionToAim = otmGunner.targetPlayerUnit.transform.position;
            positionToAim.y = otmGunner.transform.parent.position.y;
            otmGunner.transform.parent.LookAt(positionToAim);

            #region Shooting at player
            if (Time.time > lastShotTime + otmGunner.unitStat.unitAttackSpeed)
            {
                //=============================
                //Play animation otmGunner_Shoot
                //=============================
                otmGunner.otmGunnerAnimatorController.ResetTrigger("Walk");
                otmGunner.otmGunnerAnimatorController.SetTrigger("Shoot");
                otmGunner.rigBuilder.enabled = true;
                //-------------------------------------------------------

                //=============
                //Aiming a gun
                //=============
                otmGunner.Gun.transform.localPosition = new Vector3(0.31400001f, 1.41400003f, 0.160999998f); //Position
                otmGunner.Gun.transform.localRotation = Quaternion.Euler(357.268738f, 177.078262f, 359.583221f); //Rotation
                //----------------------------------------------------------------------------------------------------------

                RaycastHit hit;
                if (Physics.Raycast(otmGunner.transform.position,
                    (otmGunner.targetPlayerUnit.transform.position - otmGunner.transform.parent.position).normalized,
                    out hit,
                    Mathf.Infinity,
                    otmGunner.targetLayerMask)) // cast ray
                {
                    Debug.DrawRay(otmGunner.transform.position, (otmGunner.targetPlayerUnit.transform.position - otmGunner.transform.parent.position).normalized * hit.distance, Color.red, 0.9f);
                    lastShotTime = Time.time;
                    otmGunner.firearmsParticle.StartPlayParticle(otmGunner.firePoint.position);
                    TargetRecieveDamage(otmGunner, hit);
                }
            }

            //Play animation Gunner_Reload
            else if (otmGunner.otmGunnerAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("OtmGunner_Shoot"))
            {
                otmGunner.rigBuilder.enabled = false;
                otmGunner.otmGunnerAnimatorController.SetTrigger("Reload");
                otmGunner.Gun.transform.localPosition = new Vector3(0.425000012f, 0.88499999f, 0.593999982f); //Position
                otmGunner.Gun.transform.localRotation = Quaternion.Euler(67.0930557f, 92.2190628f, 273.481873f); //Rotation
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
        //=============================
        //Play animation otmGunner_Death
        //=============================
        otmGunner.otmGunnerAnimatorController.SetTrigger("Death");
        otmGunner.rigBuilder.enabled = false;
        otmGunner.Gun.SetActive(false);
        otmGunner.otmGunnerAgent.isStopped = true;
        //-------------------------------------------------------

        Collider colliderOfThisEnemy = otmGunner.transform.parent.GetComponent<Collider>(); // collider of this enemy
        colliderOfThisEnemy.enabled = false;
        otmGunner.capturePointByEnemy.OnTriggerExit(colliderOfThisEnemy); //Decrease population
        MonoBehaviour.Destroy(otmGunner.transform.parent.gameObject, 4f); // Delete Ottoman Gunner from the game
    }
}
