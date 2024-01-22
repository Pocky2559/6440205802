using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class ShootingState : GunnerBaseState
{
    GameObject targetEnemy;
    float lastShotTime = 0.0f;
    public override void EnterState(GunnerStateController gunner)
    {
        Debug.Log("Shooting Enter");
    }

    public override void UpdateState(GunnerStateController gunner)
    {
        #region Shooting Logic
        if (targetEnemy != null) // if it has target enemy
        {

            gunner.transform.parent.LookAt(targetEnemy.transform); // make gunner face at target enemy

            if (Time.time > lastShotTime + gunner.unitStat.unitAttackSpeed)
            {
                RaycastHit hit;
                if (Physics.Raycast(gunner.Gun.transform.position, -gunner.Gun.transform.right, out hit, 9.8f, gunner.targetLayerMask)) // cast ray
                {
                    Debug.Log("Cast Ray");
                    Debug.DrawRay(gunner.Gun.transform.position, -gunner.Gun.transform.right * hit.distance, Color.red, 0.9f);

                    GameObject shootPTC = GameObject.Instantiate(gunner.shootParticle, gunner.Gun.transform.position, Quaternion.identity); // instantiate particle
                    GameObject hitPTC = GameObject.Instantiate(gunner.hitParticle, hit.collider.transform.position, Quaternion.identity);   // instantiate particle
                    GameObject.Destroy(shootPTC, 1);
                    GameObject.Destroy(hitPTC, 2);

                    lastShotTime = Time.time;

                    TargetRecieveDamage(gunner, hit);
                }
            }
        }
        #endregion

        #region Switch to chasing state & Moving State
        if (gunner.unitSelection.unitSelected.Contains(gunner.rootGameObject) && Input.GetMouseButtonDown(1)) // if we select gunner and then right click at the enemy it will switch to ChasingState
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, gunner.groundLayerMask))
            {
                gunner.selectedPosition = hit.point;
                gunner.SwitchState(gunner.movingState);
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Enemy")) // if right click on the enemy
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
    }

    public void TargetRecieveDamage(GunnerStateController gunner , RaycastHit hit) // calculate reciever hp that recieve damage from attacker
    {
        UnitStat recieverStat = hit.collider.GetComponent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (gunner.unitStat.unitDamage - recieverStat.unitRangedArmor); // HP of reciever = HP of reciever - Damage from attacker  - Ranged Armor of reciever
    }

    public override void OnTriggerStay(GunnerStateController gunner, Collider coll)
    {
        if (coll.CompareTag("Enemy")) // if enemy is in a detection area, gunner will start shooting at him
        {
            targetEnemy = coll.gameObject; // assign the target enemy game object
        }
    }
    public override void OnTriggerExit(GunnerStateController gunner, Collider coll)
    {
        if (coll.CompareTag("Enemy")) // if enemy is outside of a detection area, gunner will enter idelState
        {
            gunner.SwitchState(gunner.idelState); // switch to idelState
        }
    }
}

