using UnityEngine;

public class MovingState : GunnerBaseState
{
    public override void EnterState(GunnerStateController gunner)
    {
        gunner.Gunner.isStopped = false;
        gunner.Gunner.SetDestination(gunner.selectedPosition);
    }
    public override void UpdateState(GunnerStateController gunner)
    {
        #region Moving Logic & Chasing State
        if(gunner.unitSelection.unitSelected.Contains(gunner.rootGameObject) && Input.GetMouseButtonDown(1)) 
        { 
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;

            if (Physics.Raycast(ray, out hit ,Mathf.Infinity, gunner.groundLayerMask))
            {
               gunner.Gunner.SetDestination(hit.point);
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Enemy")) // if right click on the enemy
                {
                    gunner.selectedEnemy = hit.collider.gameObject;
                    Debug.Log("Switching from Moving state to Chasing state");
                    gunner.SwitchState(gunner.chasingState);
                }
                else // if right click on something else
                {
                    gunner.selectedEnemy = null; // no switching and set seleced enemy as null
                }
            }
        }
        #endregion

        #region Switch to Idel State
        if (gunner.Gunner.remainingDistance == 0) //if Gunner is moving then it will switch to MovingState
        {
            Debug.Log("Switching from MOving state to Idel state");
            gunner.SwitchState(gunner.idelState); // Switch to moving state
        }
        #endregion
    }
    public override void OnTriggerStay(GunnerStateController gunner, Collider coll)
    {
        return;
    }
    public override void OnTriggerExit(GunnerStateController gunner, Collider coll)
    {
        return;
    }
}
