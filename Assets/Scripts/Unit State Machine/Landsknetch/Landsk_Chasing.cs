using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Landsk_Chasing : LandsknetchBaseState
{
    private float lastShotTime = 0.0f;
    public override void EnterState(LandsknetchStateController landsknetch)
    {
        landsknetch.landsknetchAgent.isStopped = false;
        landsknetch.landsknetchAgent.SetDestination(landsknetch.targetEnemy.transform.position);
    }

    public override void UpdaterState(LandsknetchStateController landsknetch)
    {
        Debug.Log("Land Chasing State");
        
        if (landsknetch.targetEnemy != null)
        {
            float distanceOfLandsknetchAndTargetEnemy = Vector3.Distance(landsknetch.landsknetchAgent.transform.position, landsknetch.targetEnemy.transform.position);
            if (distanceOfLandsknetchAndTargetEnemy <= 1.7f)
            {
                landsknetch.landsknetchAgent.isStopped = true;

                #region Attack
                landsknetch.transform.parent.LookAt(landsknetch.targetEnemy.transform); // make Landsknetch face the enemy
                if (Time.time > lastShotTime + landsknetch.unitStat.unitAttackSpeed)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(landsknetch.transform.position, landsknetch.transform.forward, out hit, 2.0f, landsknetch.targetLayerMask)) // cast ray
                    {
                        Debug.DrawRay(landsknetch.transform.position, landsknetch.transform.forward * hit.distance, Color.red, 0.9f);
                        lastShotTime = Time.time;
                        Debug.Log("Attacking");

                        TargetRecieveDamage(landsknetch, hit);
                    }
                }
                #endregion
            }
            if (distanceOfLandsknetchAndTargetEnemy > 1.7f) // if the enemy is far than 9.8 f
            {
                landsknetch.landsknetchAgent.isStopped = false;
                landsknetch.landsknetchAgent.SetDestination(landsknetch.targetEnemy.transform.position);
            }
        }

        if (landsknetch.targetEnemy == null)
        {
            landsknetch.landsknetchAgent.isStopped = false;
            landsknetch.targetEnemy = null;
            landsknetch.landsknetchAgent.SetDestination(landsknetch.rootGameObject.transform.position);
            landsknetch.SwitchState(landsknetch.land_IdelState);
        }

        #region Switch to idel state or select new target enemy
        if (landsknetch.unitSelection.unitSelected.Contains(landsknetch.rootGameObject) && Input.GetMouseButtonDown(1)) // if Gunner was selected and we right click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (!hit.collider.CompareTag("Enemy")) // if it is something else that not Enemy
                {
                    landsknetch.landsknetchAgent.isStopped = false; // Gunner can move (to prevent gunner freezing after .isStopped = true)
                    landsknetch.targetEnemy = null; // reset all selected enemy
                    Debug.Log("Switching from Chasing state to Moving state");
                    landsknetch.selectedPosition = hit.point;
                    landsknetch.SwitchState(landsknetch.land_MovingState); // Switch to idel state
                }
                else // if it is enemy
                {
                    landsknetch.targetEnemy = hit.collider.gameObject;
                }
            }
        }
        #endregion
    }

    public void TargetRecieveDamage(LandsknetchStateController landsknetch, RaycastHit hit) // calculate reciever hp that recieve damage from attacker
    {
        UnitStat recieverStat = hit.collider.GetComponent<UnitStat>();
        recieverStat.unitHP = recieverStat.unitHP - (landsknetch.unitStat.unitDamage - recieverStat.unitRangedArmor); // HP of reciever = HP of reciever - Damage from attacker  - Ranged Armor of reciever
    }

    public override void OnTriggerStay(LandsknetchStateController landsknetch, Collider coll)
    {
        Debug.Log("");
    }

    public override void OnTriggerExit(LandsknetchStateController landsknetch, Collider coll)
    {
        Debug.Log("");
    }
}
