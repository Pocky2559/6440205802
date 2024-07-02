using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class ShootingState : GunnerBaseState
{
    float lastShotTime = 0.0f;
    AnimatorStateInfo stateInfo;
    public override void EnterState(GunnerStateController gunner)
    {
        //Play animation Gunner_Shoot
        gunner.gunnerAnimatorControlller.SetBool("isShooting",true);
        gunner.gunnerAnimatorControlller.SetBool("isWalking", false);
        gunner.gunnerAnimatorControlller.SetBool("isAmmoOut", false);
        gunner.gunnerAnimatorControlller.SetBool("isMoveWhileReload", false);
        gunner.gunnerAnimatorControlller.SetBool("isEnemyDeadWhileReload", false);
        gunner.Gun.transform.localPosition = new Vector3(0.254000008f, 1.19500005f, 0.437000006f);
        gunner.Gun.transform.localRotation = Quaternion.Euler(357.268799f, 186.659225f, 359.583252f);
        //

        //Aiming Raotaion 
        //gunner.Gun.transform.localRotation = Quaternion.Euler(357.268799f, 186.659225f, 359.583252f);
    }

    public override void UpdateState(GunnerStateController gunner)
    {
        #region Shooting Logic
        if (gunner.selectedEnemy != null && gunner.Gunner.enabled == true) // if it has target enemy
        {
            Vector3 positionToAim = gunner.selectedEnemy.transform.position;
            positionToAim.y = gunner.transform.parent.position.y; //Freeze y position
            gunner.transform.parent.LookAt(positionToAim); // make gunner face at target enemy
                                                                            //Play animation Gunner_Shooting
            if (Time.time > lastShotTime + gunner.unitStat.unitAttackSpeed)
            {  
                RaycastHit hit;
                if (Physics.Raycast(gunner.Gun.transform.position,
                                    (gunner.selectedEnemy.transform.position - gunner.transform.parent.position).normalized,
                                    out hit,
                                    Mathf.Infinity,
                                    gunner.targetLayerMask)) // cast ray
                {
                    //Play animation Gunner_Shoot and Reload
                    gunner.gunnerAnimatorControlller.SetBool("isAmmoOut", false);
                    gunner.rigBuilder.enabled = true;
                    gunner.Gun.transform.localPosition = new Vector3(0.254000008f, 1.19500005f, 0.437000006f);
                    gunner.Gun.transform.localRotation = Quaternion.Euler(357.268799f, 186.659225f, 359.583252f);
                    //

                    //Play Gun fire sound
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
        }
        #endregion

        #region Switch to chasing state & Moving State
        if (gunner.unitSelection.unitSelected.Contains(gunner.rootGameObject) && Input.GetMouseButtonDown(1)) // if we select gunner and then right click at the enemy it will switch to ChasingState
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, gunner.groundLayerMask))
            {
               if (gunner.gunnerAnimatorControlller.GetCurrentAnimatorStateInfo(0).IsName("Gunner_Reload"))
               {
                    //Play animation Gunner_Walking
                      gunner.gunnerAnimatorControlller.SetBool("isMoveWhileReload", true);
                    //
               }
                gunner.SwitchState(gunner.movingState);
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("OttomanRecruit")
                      || hit.collider.CompareTag("OttomanGunnerRecruit")
                      || hit.collider.CompareTag("MeleeJanissary")
                      || hit.collider.CompareTag("RangedJanissary")
                      || hit.collider.CompareTag("OttomanCannon")) // if right click on the enemy
                {
                    gunner.selectedEnemy = hit.collider.gameObject;
                    Debug.Log("Switching from Idel State to Chasing state");
                    gunner.SwitchState(gunner.chasingState);
                }
                else // if right click on something else
                {
                    gunner.selectedEnemy = null; // no switching and set seleced enemy as null
                }
            }
        }
        #endregion

        #region Switch to IdelState if enemy is dead
        if(gunner.selectedEnemyStat.unitHP <= 0)
        {
            if (gunner.gunnerAnimatorControlller.GetCurrentAnimatorStateInfo(0).IsName("Gunner_Reload"))
            {
                //Play animation Gunner_Walking
                gunner.gunnerAnimatorControlller.SetBool("isEnemyDeadWhileReload", true);
                //
            }
            gunner.SwitchState(gunner.idelState);
        }
        #endregion

        #region Switch to ExitState
        if (gunner.unitStat.unitHP <= 0)
        {
            ExitState(gunner);
        }
        #endregion
    }

    public void TargetRecieveDamage(GunnerStateController gunner , RaycastHit hit) // calculate reciever hp that recieve damage from attacker
    {
        UnitStat recieverStat = hit.collider.GetComponent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (gunner.unitStat.unitDamage - recieverStat.unitRangedArmor); // HP of reciever = HP of reciever - Damage from attacker  - Ranged Armor of reciever
    }

    public override void OnTriggerStay(GunnerStateController gunner, Collider coll)
    {
        //if (coll.CompareTag("OttomanRecruit")
        //    || coll.CompareTag("OttomanGunnerRecruit")
        //    || coll.CompareTag("MeleeJanissary")
        //    || coll.CompareTag("RangedJanissary")
        //    || coll.CompareTag("OttomanCannon")
        //    ) // if enemy is in a detection area, gunner will start shooting at him
        //{
        //    targetEnemy = coll.gameObject; // assign the target enemy game object
        //}
    }
    public override void OnTriggerExit(GunnerStateController gunner, Collider coll)
    {
        #region Switch to IdelState if enemy out of ranged
        if (gunner.selectedEnemy == coll.gameObject) // if enemy is outside of a detection area, gunner will enter idelState
        {
            gunner.SwitchState(gunner.idelState); // switch to idelState
        }
        #endregion
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

