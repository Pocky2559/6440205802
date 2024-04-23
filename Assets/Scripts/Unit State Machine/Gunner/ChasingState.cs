using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ChasingState : GunnerBaseState
{
    float lastShotTime = 0.0f;
    public override void EnterState(GunnerStateController gunner)
    {
        gunner.Gunner.SetDestination(gunner.selectedEnemy.transform.position);
    }
    public override void UpdateState (GunnerStateController gunner)
    {
        #region Chasing Logic
        if (gunner.selectedEnemy != null)
        {
            float distanceOfGunnerAndTargetEnemy = Vector3.Distance(gunner.Gunner.transform.position, gunner.selectedEnemy.transform.position); // the distance between gunner and selected enemy

            if (distanceOfGunnerAndTargetEnemy <= 6.8f) // gunner.selectedEnemy.transorm means that game object is still exist in hierarchy
            {
                gunner.Gunner.isStopped = true;  // if the enemy reach firing range it will stop

                #region Shoot
                gunner.transform.parent.LookAt(gunner.selectedEnemy.transform.root); // make gunner face at target enemy

                if (Time.time > lastShotTime + gunner.unitStat.unitAttackSpeed)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(gunner.Gun.transform.position, -gunner.Gun.transform.forward , out hit, Mathf.Infinity, gunner.targetLayerMask)) // cast ray
                    {
                        Debug.Log("Cast Ray");
                        Debug.DrawRay(gunner.Gun.transform.position, -gunner.Gun.transform.forward * hit.distance, Color.red, 2);
                        lastShotTime = Time.time;

                        TargetRecieveDamage(gunner, hit);
                    }
                }
                #endregion
            }

            if (distanceOfGunnerAndTargetEnemy > 6.8f) // if the enemy is far than 9.8 f
            {
                gunner.Gunner.isStopped = false;
                gunner.Gunner.SetDestination(gunner.selectedEnemy.transform.position);
            }

        }
        #endregion

        #region Switch to idel state or select new target enemy
        if (gunner.unitSelection.unitSelected.Contains(gunner.rootGameObject) && Input.GetMouseButtonDown(1)) // if Gunner was selected and we right click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (!(hit.collider.CompareTag("OttomanRecruit")
                      || hit.collider.CompareTag("OttomanGunnerRecruit")
                      || hit.collider.CompareTag("MeleeJanissary")
                      || hit.collider.CompareTag("RangedJanissary")
                      || hit.collider.CompareTag("OttomanCannon"))) // if it is something else that not Enemy
                {
                    gunner.Gunner.isStopped = false; // Gunner can move (to prevent gunner freezing after .isStopped = true)
                    gunner.selectedEnemy = null; // reset all selected enemy
                    Debug.Log("Switching from Chasing state to Moving state");
                    //gunner.selectedPosition = hit.point;
                    gunner.SwitchState(gunner.movingState); // Switch to idel state
                }
                else // if it is enemy
                {
                    gunner.selectedEnemy = hit.collider.gameObject;
                }
            }
        }
        #endregion

        #region Switch to idel state
        if (gunner.selectedEnemy == null) // if the selected enemy is dead
        {
            gunner.Gunner.isStopped = false; // make the gunner stop
            gunner.Gunner.SetDestination(gunner.rootGameObject.transform.position);
            gunner.selectedEnemy = null; // set the selected enemy as null
            Debug.Log("Switch from Chasing state to Idel state");
            gunner.SwitchState(gunner.idelState); // switch to idel state
        }
        #endregion

        #region Switch to ExitState
        if (gunner.unitStat.unitHP <= 0)
        {
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
        gunner.population.PopulationChanges(-1 * gunner.unitStat.unitPopulation); //Decrease population
        MonoBehaviour.Destroy(gunner.transform.parent.gameObject); // Delete Villager from the game
    }
}
