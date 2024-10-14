using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Landsk_Chasing : LandsknetchBaseState
{
    private float lastShotTime = 0.0f;
    bool isDead;
    public override void EnterState(LandsknetchStateController landsknetch)
    {
        landsknetch.landsknetchAgent.isStopped = false;
    }

    public override void UpdaterState(LandsknetchStateController landsknetch)
    {
        if ((landsknetch.enemyStat.unitHP > 0 || landsknetch.targetEnemy != null) && landsknetch.unitStat.unitHP > 0)
        {
            float distanceOfLandsknetchAndTargetEnemy = Vector3.Distance(landsknetch.landsknetchAgent.transform.position, landsknetch.targetEnemy.transform.position);
            
            if (distanceOfLandsknetchAndTargetEnemy <= 1f) //if enemy is in ranged
            {
                landsknetch.landsknetchAgent.isStopped = true;
                landsknetch.neutralSword.SetActive(false);
                landsknetch.attackedSword.SetActive(true);

                #region Attack
                Vector3 positionToAim = landsknetch.targetEnemy.transform.position;
                positionToAim.y = landsknetch.transform.parent.position.y; //Freeze position y
                landsknetch.transform.parent.LookAt(positionToAim); // make Landsknetch face the enemy

                if (Time.time > lastShotTime + landsknetch.unitStat.unitAttackSpeed)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(
                        landsknetch.transform.parent.position,
                        (landsknetch.targetEnemy.transform.position - landsknetch.transform.parent.position).normalized,
                        out hit,
                        Mathf.Infinity,
                        landsknetch.targetLayerMask)) // cast ray
                    {
                        //===================================
                        //Play animation Landsknecht_Attacking
                        //===================================
                        landsknetch.landskAnimatorControlller.ResetTrigger("Walk");
                        landsknetch.landskAnimatorControlller.SetTrigger("Attack");
                        //-----------------------------------

                        //Play PlayerSwordHit Sound
                        landsknetch.soundEffectController.PlayPlayerSwordHitSound();
                        

                        Debug.DrawRay(landsknetch.transform.position, (landsknetch.targetEnemy.transform.position - landsknetch.transform.parent.position).normalized * hit.distance, Color.green, 0.9f);
                        lastShotTime = Time.time;

                        TargetRecieveDamage(landsknetch, hit);
                        //Play PlayerSwordHit Sound
                        landsknetch.soundEffectController.PlayPlayerSwordHitSound();
                    }
                }
                #endregion
            }
            else if (distanceOfLandsknetchAndTargetEnemy > 1f) // if the enemy is out of ranged
            {
                landsknetch.neutralSword.SetActive(true);
                landsknetch.attackedSword.SetActive(false);
                //===================================
                //Play animation Landsknecht_Walking
                //===================================                
                landsknetch.landskAnimatorControlller.SetTrigger("Walk");
                //-----------------------------------

                //Play Walking Sound 
                //Check before playing if AudioSource is playing walksound it will not play it again 
                if(landsknetch.soundEffectController.soundEffect.clip != landsknetch.soundEffectController.soundEffectDatabase.walkSound)
                {
                    landsknetch.soundEffectController.PlayWalkingSound();//Play Walking Sound
                }

                landsknetch.landsknetchAgent.isStopped = false;
                landsknetch.landsknetchAgent.SetDestination(landsknetch.targetEnemy.transform.position);
            }
        }

        if (landsknetch.enemyStat.unitHP <= 0 /*|| landsknetch.targetEnemy == null*/)
        {
            //===================================
            //Play animation Landsknecht_Idle
            //===================================
            landsknetch.landskAnimatorControlller.ResetTrigger("Attack");
            landsknetch.landskAnimatorControlller.ResetTrigger("Walk");
            landsknetch.landskAnimatorControlller.SetTrigger("Idle");
            //-----------------------------------

            landsknetch.landsknetchAgent.isStopped = false;
            landsknetch.targetEnemy = null;
            landsknetch.landsknetchAgent.SetDestination(landsknetch.rootGameObject.transform.position);
            landsknetch.SwitchState(landsknetch.land_IdelState);
        }

        #region Switch to Moving state or select new target enemy
        if (landsknetch.unitSelection.unitSelected.Contains(landsknetch.rootGameObject) && Input.GetMouseButtonDown(1)) // if Gunner was selected and we right click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (!(hit.collider.CompareTag("OttomanRecruit")
                      || hit.collider.CompareTag("OttomanGunnerRecruit")
                      || hit.collider.CompareTag("MeleeJanissary")
                      || hit.collider.CompareTag("RangedJanissary")
                      || hit.collider.CompareTag("OttomanCannon"))) // if it is something else that not Enemy
                {
                    //===================================
                    //Play animation Landsknecht_Walking
                    //===================================
                    landsknetch.landskAnimatorControlller.SetTrigger("Walk");
                    //-----------------------------------

                    landsknetch.landsknetchAgent.isStopped = false; // Gunner can move (to prevent gunner freezing after .isStopped = true)
                    landsknetch.targetEnemy = null; // reset all selected enemy
                    Debug.Log("Switching from Chasing state to Moving state");
                    landsknetch.selectedPosition = hit.point;
                    landsknetch.SwitchState(landsknetch.land_MovingState); // Switch to idel state
                }
                else // if it is enemy
                {
                    //===================================
                    //Play animation Landsknecht_Walking
                    //===================================
                    landsknetch.landskAnimatorControlller.SetTrigger("Walk");
                    //-----------------------------------

                    landsknetch.targetEnemy = hit.collider.gameObject;
                }
            }
        }
        #endregion

        #region Switch to ExitState
        if (landsknetch.unitStat.unitHP <= 0 && isDead == false)
        {
            isDead = true;
            landsknetch.soundEffectController.PlayUnitDiedSound(); //PLay UnitDie Sound
            ExitState(landsknetch);
        }
        #endregion

    }

    public void TargetRecieveDamage(LandsknetchStateController landsknetch, RaycastHit hit) // calculate reciever hp that recieve damage from attacker
    {
        UnitStat recieverStat = hit.collider.GetComponent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (landsknetch.unitStat.unitDamage - recieverStat.unitMeleeArmor); // HP of reciever = HP of reciever - Damage from attacker  - Ranged Armor of reciever
    }

    public override void OnTriggerStay(LandsknetchStateController landsknetch, Collider coll)
    {
        return;
    }

    public override void OnTriggerExit(LandsknetchStateController landsknetch, Collider coll)
    {
        return;
    }

    public override void ExitState(LandsknetchStateController landsknecht)
    { 
        //===================================
        //Play animation Landsknecht_Walking
        //===================================
        landsknecht.landskAnimatorControlller.SetTrigger("Death");
        //-----------------------------------

        landsknecht.landsknetchAgent.isStopped = true;
        landsknecht.landskCollider.enabled = false;
        landsknecht.population.PopulationChanges(-1 * landsknecht.unitStat.unitPopulation); //Decrease population
        MonoBehaviour.Destroy(landsknecht.transform.parent.gameObject,4); // Delete Villager from the game
    }
}
