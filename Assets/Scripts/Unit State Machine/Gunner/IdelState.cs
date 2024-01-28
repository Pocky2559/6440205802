using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class IdelState : GunnerBaseState
{
    public override void EnterState(GunnerStateController gunner)
    {
        gunner.Gunner.isStopped = false;
    }

    public override void UpdateState(GunnerStateController gunner)
    {

        #region Switch to Moving State & Chasing State

        if (gunner.unitSelection.unitSelected.Contains(gunner.rootGameObject) && Input.GetMouseButtonDown(1)) //if Gunner is moving then it will switch to MovingState
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit , Mathf.Infinity, gunner.groundLayerMask))
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
                /*else // if right click on something else
                {
                    gunner.selectedEnemy = null; // no switching and set seleced enemy as null
                }*/
            }
        }
        #endregion
    }

    public override void OnTriggerStay(GunnerStateController gunner, Collider coll)
    {
        Collider targetCollider = coll;
        #region Switch to Shooting State
        if (targetCollider.CompareTag("Enemy")) // if the enemy is in a detection area
        {
            gunner.SwitchState(gunner.shootingState); // switch to shooting state
        }
        #endregion
    }
    public override void OnTriggerExit(GunnerStateController gunner, Collider coll)
    {
        return;
    }

}
