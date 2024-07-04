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
    bool isDead;
    public override void EnterState(OttomanRecruitStateController otmRecruit)
    {

        if (otmRecruit.Wall != null) // if wall is still there
        {
            //===============================
            //Play animation otmRecruit_Walking
            //===============================
            otmRecruit.otmRecruitAnimatorController.SetTrigger("Walk");
            //-------------------------------

            otmRecruit.otmRecruitAgent.SetDestination(otmRecruit.Wall.transform.position);
        }

        else
        {
            //===============================
            //Play animation otmRecruit_Walking
            //===============================
            otmRecruit.otmRecruitAnimatorController.SetTrigger("Walk");
            //-------------------------------

            otmRecruit.SwitchState(otmRecruit.otmRecruit_CapturePointState);
        }

    }

    public override void UpdaterState(OttomanRecruitStateController otmRecruit)
    {
        if (otmRecruit.Wall != null && otmRecruit.unitStat.unitHP > 0) // if wall is still there
        {
            distanceOfEnemyAndWall = Vector3.Distance(otmRecruit.otmRecruitAgent.transform.position, otmRecruit.Wall.transform.position);

            Vector3 positionToAim = otmRecruit.Wall.transform.position;
            positionToAim.y = otmRecruit.transform.parent.position.y;
            otmRecruit.transform.parent.LookAt(positionToAim);

            if (distanceOfEnemyAndWall <= 2)
            {
                otmRecruit.otmRecruitAgent.isStopped = true;
                //===============================
                //Play animation otmRecruit_Attacking
                //===============================
                otmRecruit.otmRecruitAnimatorController.SetTrigger("Attack");
                //-------------------------------

                if (Time.time > lastShotTime + otmRecruit.unitStat.unitAttackSpeed)
                {
                    Attack(otmRecruit);

                    //Play SwordHit Sound
                    otmRecruit.soundEffectController.PlayPlayerSwordHitSound();

                    lastShotTime = Time.time;
                }
            }

            else
            {
                //Play SwordHit Sound
                // check Is AudioSource is playing walk sound to prevent it replay and cannot hearable
                if (otmRecruit.soundEffectController.soundEffect.clip != otmRecruit.soundEffectController.soundEffectDatabase.walkSound)
                {
                    otmRecruit.soundEffectController.PlayWalkingSound();
                }
            }
        }
        else
        {
            //===============================
            //Play animation otmRecruit_Walking
            //===============================
            otmRecruit.otmRecruitAnimatorController.ResetTrigger("Attack");
            otmRecruit.otmRecruitAnimatorController.SetTrigger("Walk");
            //-------------------------------
            otmRecruit.SwitchState(otmRecruit.otmRecruit_CapturePointState);
        }

        #region Switch to ExitState
        if(otmRecruit.unitStat.unitHP <= 0 && isDead == false)
        {
            isDead = true;
            otmRecruit.soundEffectController.PlayUnitDiedSound(); //Play UnitDie Sound
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
        return;
    }

    public void Attack(OttomanRecruitStateController otmRecruit)
    {
        RaycastHit hit;
        if (Physics.Raycast(otmRecruit.transform.position, (otmRecruit.Wall.transform.position - otmRecruit.transform.position).normalized, out hit, Mathf.Infinity, otmRecruit.wallLayerMask))
        {
            Debug.DrawRay(otmRecruit.transform.position, otmRecruit.transform.forward * hit.distance, Color.red, 0.1f);
            TargetRecieveDamage(otmRecruit, hit);
        }
    }

    public void TargetRecieveDamage(OttomanRecruitStateController otmRecruit, RaycastHit hit) // calculate reciever hp that recieve damage from attacker
    {
        UnitStat recieverStat = hit.collider.GetComponent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (otmRecruit.unitStat.unitDamage - recieverStat.unitMeleeArmor); // HP of reciever = HP of reciever - Damage from attacker  - Ranged Armor of reciever
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
        otmRecruit.capturePointByEnemy.OnTriggerExit(colliderOfThisEnemy);
        MonoBehaviour.Destroy(otmRecruit.transform.parent.gameObject, 4); // Delete Villager from the game
    }
}
