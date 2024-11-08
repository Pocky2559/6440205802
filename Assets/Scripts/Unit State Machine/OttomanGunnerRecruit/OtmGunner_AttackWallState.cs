using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class OtmGunner_AttackWallState : OttomanGunnerRecruitBaseState
{
    float lastShotTime = 0.0f;
    float distanceOfEnemyAndWall;
    bool isDead;
    public override void EnterState(OttomanGunnerRecruitStateController otmGunner)
    {
        //otmGunner.attackRange.radius = otmGunner.originAttackRange;
        if (otmGunner.Wall != null) // if wall is still there
        {
            //=============================
            //Gun Holding Position
            //=============================
            otmGunner.Gun.transform.localPosition = new Vector3(0.254000008f, 1.18599999f, 0.324000001f);
            otmGunner.Gun.transform.localRotation = Quaternion.Euler(357.268738f, 122.092773f, 359.583221f);
            //-------------------------------------------------------------------------------------------

            otmGunner.otmGunnerAgent.SetDestination(otmGunner.Wall.transform.position);
            otmGunner.rootGameObject.transform.LookAt(otmGunner.Wall.transform.position);
            //otmGunner.attackRange.radius += 3.5f;
        }

        else
        {
            //=============================
            //Gun Holding Position
            //=============================
            otmGunner.Gun.transform.localPosition = new Vector3(0.254000008f, 1.18599999f, 0.324000001f);
            otmGunner.Gun.transform.localRotation = Quaternion.Euler(357.268738f, 122.092773f, 359.583221f);
            //-------------------------------------------------------------------------------------------

            otmGunner.SwitchState(otmGunner.otmGunner_CapturePointState);
            //otmGunner.attackRange.radius = otmGunner.originAttackRange;
        }

        otmGunner.soundEffectController.StopPlaySound();//Stop all sound
    }

    public override void UpdateState(OttomanGunnerRecruitStateController otmGunner)
    {
        if (otmGunner.Wall != null && otmGunner.unitStat.unitHP > 0) // if wall is still there
        {
            distanceOfEnemyAndWall = Vector3.Distance(otmGunner.otmGunnerAgent.transform.position, otmGunner.Wall.transform.position);
            if (distanceOfEnemyAndWall <= 4)
            {
                Vector3 positionToAim = otmGunner.Wall.transform.position;
                positionToAim.y = otmGunner.transform.parent.position.y;
                otmGunner.transform.parent.LookAt(positionToAim);

                otmGunner.otmGunnerAgent.isStopped = true;

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

                    // PLay GunFire Sound
                    otmGunner.soundEffectController.PlayGunFireSound();

                    Attack(otmGunner);
                    otmGunner.firearmsParticle.StartPlayParticle(otmGunner.firePoint.position);
                    lastShotTime = Time.time;
            }

                //Play animation Gunner_Reload
            else if (otmGunner.otmGunnerAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("OtmGunner_Shoot"))
            {
               otmGunner.rigBuilder.enabled = false;
               otmGunner.otmGunnerAnimatorController.SetTrigger("Reload");
               otmGunner.Gun.transform.localPosition = new Vector3(0.425000012f, 0.88499999f, 0.593999982f); //Position
               otmGunner.Gun.transform.localRotation = Quaternion.Euler(67.0930557f, 92.2190628f, 273.481873f); //Rotation
            }
            }

            else
            {
                //=============================
                //Play animation otmGunner_Walking
                //=============================
                otmGunner.otmGunnerAnimatorController.SetTrigger("Walk");
                //-------------------------------------------------------

                //Play Walking Sound
                // Check before playing if AudioSource is playing walksound it will not play again
                if(otmGunner.soundEffectController.soundEffect.clip != otmGunner.soundEffectController.soundEffectDatabase.walkSound)
                {
                    otmGunner.soundEffectController.PlayWalkingSound();
                }

                otmGunner.otmGunnerAgent.isStopped = false;
                otmGunner.otmGunnerAgent.SetDestination(otmGunner.Wall.transform.position);
            }
        }

        else if(otmGunner.Wall == null)
        {
            otmGunner.attackRange.radius = otmGunner.originAttackRange;
            otmGunner.SwitchState(otmGunner.otmGunner_CapturePointState);
        }

        #region Switch to ExitState
        if (otmGunner.unitStat.unitHP <= 0 && isDead == false)
        {
            isDead = true;
            otmGunner.soundEffectController.PlayUnitDiedSound();
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
            otmGunner.targetPlayerUnitStat = coll.GetComponentInParent<UnitStat>();
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
        if (Physics.Raycast(otmGunner.transform.position,
            (otmGunner.Wall.transform.position - otmGunner.transform.parent.position).normalized,
            out hit,
            Mathf.Infinity,
            otmGunner.wallLayerMask))
        {
            Debug.DrawRay(otmGunner.transform.position, (otmGunner.Wall.transform.position - otmGunner.transform.parent.position).normalized * hit.distance, Color.red, 0.9f);
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
        //=============================
        //Play animation otmGunner_Death
        //=============================
        otmGunner.otmGunnerAnimatorController.SetTrigger("Death");
        otmGunner.rigBuilder.enabled = false;
        otmGunner.Gun.SetActive(false);
        otmGunner.otmGunnerAgent.isStopped = true;
        otmGunner.otmGunnerAgent.enabled = false;
        //-------------------------------------------------------

        Collider colliderOfThisEnemy = otmGunner.transform.parent.GetComponent<Collider>(); // collider of this enemy
        colliderOfThisEnemy.enabled = false;
        otmGunner.capturePointByEnemy.OnTriggerExit(colliderOfThisEnemy); //Decrease population
        MonoBehaviour.Destroy(otmGunner.transform.parent.gameObject, 4f); // Delete Ottoman Gunner from the game
    }
}
