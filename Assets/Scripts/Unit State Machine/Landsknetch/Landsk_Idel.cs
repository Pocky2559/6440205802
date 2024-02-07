using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Landsk_Idel : LandsknetchBaseState
{
    public override void EnterState(LandsknetchStateController landsknetch)
    {
        
        landsknetch.landsknetchAgent.isStopped = false;
        Debug.Log("Landsk Idel Enter");
    }

    public override void UpdaterState(LandsknetchStateController landsknetch)
    {
        #region Switch to Moving State
        if (landsknetch.unitSelection.unitSelected.Contains(landsknetch.rootGameObject) && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, landsknetch.groundLayerMask))
            {
               landsknetch.selectedPosition = hit.point;
               landsknetch.SwitchState(landsknetch.land_MovingState);
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, landsknetch.targetLayerMask))
            {
               landsknetch.targetEnemy = hit.collider.gameObject;
               landsknetch.SwitchState(landsknetch.land_ChasingState);
            }
        }
        #endregion
    }

    public override void OnTriggerStay(LandsknetchStateController landsknetch, Collider coll)
    {
        Collider targetCollider = coll;
        #region Switch to Chasing State
        if (targetCollider.CompareTag("Enemy")) // if the enemy is in a detection area
        {
            landsknetch.targetEnemy = coll.gameObject;
            landsknetch.SwitchState(landsknetch.land_ChasingState); // switch to chasing state
        }
        #endregion
    }

    public override void OnTriggerExit(LandsknetchStateController landsknetch, Collider coll)
    {
        Debug.Log("Enter");
    }
}
