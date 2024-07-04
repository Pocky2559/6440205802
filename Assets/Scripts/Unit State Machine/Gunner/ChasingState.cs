using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ChasingState : GunnerBaseState
{
    float lastShotTime = 0.0f;
    float distanceOfGunnerAndTargetEnemy;
    bool isDead;
    public override void EnterState(GunnerStateController gunner)
    {
        //Play Animation Gunner_Walking
          gunner.gunnerAnimatorControlller.SetBool("isWalking", true);
          gunner.gunnerAnimatorControlller.SetBool("isFollowing", false);
          gunner.gunnerAnimatorControlller.SetBool("isShooting", true);
          gunner.gunnerAnimatorControlller.SetBool("isMoveWhileReload", false);
          gunner.gunnerAnimatorControlller.SetBool("isEnemyDeadWhileReload", false);
        //
    }
    public override void UpdateState (GunnerStateController gunner)
    {
        #region Chasing Logic
        if (gunner.selectedEnemy != null && gunner.Gunner.enabled == true)
        {
            distanceOfGunnerAndTargetEnemy = Vector3.Distance(gunner.transform.position, gunner.selectedEnemy.transform.position); // the distance between gunner and selected enemy

            if (distanceOfGunnerAndTargetEnemy <= gunner.attackRange.radius * Mathf.Max(
                gunner.transform.parent.localScale.x,
                gunner.transform.parent.localScale.y,
                gunner.transform.parent.localScale.z)) //if enemy is in ranged
            {           
                gunner.Gunner.isStopped = true;  // if the enemy reach firing range it will stop

                #region Shoot
                Vector3 positionToAim = gunner.selectedEnemy.transform.position;
                positionToAim.y = gunner.transform.parent.position.y; //Freeze position y
                gunner.transform.parent.LookAt(positionToAim); // make gunner face at target enemy

                if (Time.time > lastShotTime + gunner.unitStat.unitAttackSpeed)
                {
                    RaycastHit hit;
                   
                    if (Physics.Raycast(gunner.Gun.transform.position,
                        (gunner.selectedEnemy.transform.position - gunner.transform.parent.position).normalized,
                        out hit,
                        Mathf.Infinity,
                        gunner.targetLayerMask)) // cast ray
                    {
                        //Play animation Shooting
                          gunner.gunnerAnimatorControlller.SetBool("isFollowing", true);
                          gunner.gunnerAnimatorControlller.SetBool("isAmmoOut", false);
                          gunner.gunnerAnimatorControlller.SetBool("isMoveWhileReload", false);
                          gunner.Gun.transform.localPosition = new Vector3(0.254000008f, 1.19500005f, 0.437000006f);
                          gunner.Gun.transform.localRotation = Quaternion.Euler(357.268799f, 186.659225f, 359.583252f);
                          gunner.rigBuilder.enabled = true;
                        //

                        // Play GunFire Sound
                        gunner.soundEffectController.PlayGunFireSound();

                        gunner.firearmsParticle.StartPlayParticle(gunner.firePoint.transform.position);
                        Debug.DrawRay(gunner.Gun.transform.position, (gunner.selectedEnemy.transform.position - gunner.transform.parent.position).normalized * hit.distance, Color.red, 0.2f);
                        lastShotTime = Time.time;
                        TargetRecieveDamage(gunner, hit);
                    }
                }

                //Play animation Gunner_Reload
                if (gunner.gunnerAnimatorControlller.GetCurrentAnimatorStateInfo(0).IsName("Gunner_Shoot"))
                {
                    gunner.gunnerAnimatorControlller.SetBool("isAmmoOut", true);
                    gunner.rigBuilder.enabled = false;
                    gunner.Gun.transform.localPosition = new Vector3(0.254000008f, 0.833999991f, 0.437000006f);
                    gunner.Gun.transform.localRotation = Quaternion.Euler(56.6684723f, 84.1026611f, 351.425751f);
                }
                //
                #endregion
            }

            if (distanceOfGunnerAndTargetEnemy > gunner.attackRange.radius * Mathf.Max(
                gunner.transform.parent.localScale.x,
                gunner.transform.parent.localScale.y,
                gunner.transform.parent.localScale.z)) //if it out of attack ranged
            {
                //Play animation Gunner_Walking
                gunner.gunnerAnimatorControlller.SetBool("isFollowing", false);
                if (gunner.gunnerAnimatorControlller.GetCurrentAnimatorStateInfo(0).IsName("Gunner_Reload"))
                {
                    gunner.gunnerAnimatorControlller.SetBool("isMoveWhileReload", true);
                }
                gunner.Gun.transform.localPosition = new Vector3(0.254000008f, 1.18599999f, 0.324000001f);
                gunner.Gun.transform.localRotation = Quaternion.Euler(357.268738f, 122.092773f, 359.583221f);
                //

                //Play Walking Sound
                // check if AudioSource is playing walksound, it will not play it again
                if (gunner.soundEffectController.soundEffect.clip != gunner.soundEffectController.soundEffectDatabase.walkSound)
                {
                    gunner.soundEffectController.PlayWalkingSound();//Play Walking Sound
                }
                
                gunner.Gunner.isStopped = false;
                gunner.Gunner.SetDestination(gunner.selectedEnemy.transform.position);
            }
        }
        #endregion

        #region Switch to Moving state or select new target enemy
        if (gunner.unitSelection.unitSelected.Contains(gunner.rootGameObject) && Input.GetMouseButtonDown(1)) // if Gunner was selected and we right click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, gunner.groundLayerMask))
            {
                if ( !(hit.collider.CompareTag("OttomanRecruit")
                      || hit.collider.CompareTag("OttomanGunnerRecruit")
                      || hit.collider.CompareTag("MeleeJanissary")
                      || hit.collider.CompareTag("RangedJanissary")
                      || hit.collider.CompareTag("OttomanCannon"))) // if it is something else that not Enemy
                {
                    //Play animation Gunner_Walking
                    gunner.gunnerAnimatorControlller.SetBool("isMoveWhileReload", true);
                    //
                    gunner.Gunner.isStopped = false; // Gunner can move (to prevent gunner freezing after .isStopped = true)
                    gunner.selectedEnemy = null; // reset all selected enemy
                    Debug.Log("Switching from Chasing state to Moving state");
                    gunner.SwitchState(gunner.movingState); // Switch to idel state
                }
            }

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, gunner.targetLayerMask))
            {
                gunner.selectedEnemy = hit.collider.gameObject;
            }
        }
        #endregion

        #region Switch to idel state
        if (gunner.selectedEnemyStat.unitHP <= 0) // if the selected enemy is dead or stop target it
        {
           //Play animation Gunner_Idle
             gunner.gunnerAnimatorControlller.SetBool("isEnemyDeadWhileReload", true);
           //

            gunner.Gunner.isStopped = false; // make the gunner stop
            gunner.Gunner.SetDestination(gunner.rootGameObject.transform.position);
            gunner.selectedEnemy = null; // set the selected enemy as null
            Debug.Log("Switch from Chasing state to Idel state");
            gunner.SwitchState(gunner.idelState); // switch to idel state
        }
        #endregion

        #region Switch to ExitState
        if (gunner.unitStat.unitHP <= 0 && isDead == false)
        {
            isDead = true;
            gunner.soundEffectController.PlayUnitDiedSound();//PLay UnitDie Sound
            ExitState(gunner);
        }
        #endregion
    }

    public void TargetRecieveDamage(GunnerStateController gunner, RaycastHit hit) // calculate reciever hp that recieve damage from attacker
    {
        UnitStat recieverStat = hit.collider.GetComponent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (gunner.unitStat.unitDamage - recieverStat.unitRangedArmor); // HP of reciever = HP of reciever - Damage from attacker  - Ranged Armor of reciever
    }

    public override void OnTriggerStay(GunnerStateController gunner, Collider coll)
    {
        return;
    }
    public override void OnTriggerExit(GunnerStateController gunner, Collider coll)
    {
        return;
    }
    public override void ExitState(GunnerStateController gunner)
    {
        //Play animation Gunner_Death
        gunner.Gunner.enabled = false;
        gunner.gunnerAnimatorControlller.SetBool("isDead", true);
        gunner.Gun.SetActive(false);
        gunner.rigBuilder.enabled = false;
        gunner.gunnerCollider.enabled = false;
        //
        gunner.population.PopulationChanges(-1 * gunner.unitStat.unitPopulation); //Decrease population
        MonoBehaviour.Destroy(gunner.transform.parent.gameObject, 4); // Delete Villager from the game
    }
}
