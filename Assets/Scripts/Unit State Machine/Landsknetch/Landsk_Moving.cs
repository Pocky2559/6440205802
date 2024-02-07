using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Landsk_Moving : LandsknetchBaseState
{
    public override void EnterState(LandsknetchStateController landsknetch)
    {
        landsknetch.landsknetchAgent.isStopped = false;
        landsknetch.landsknetchAgent.SetDestination(landsknetch.selectedPosition);
    }

    public override void UpdaterState(LandsknetchStateController landsknetch)
    {
        #region Switch to Idel State
        if (landsknetch.landsknetchAgent.remainingDistance == 0) //if Gunner is moving then it will switch to MovingState
        {
            Debug.Log("Switching from Moving state to Idel state");
            landsknetch.SwitchState(landsknetch.land_IdelState); // Switch to moving state
        }
        #endregion

        #region Change moving
        if (landsknetch.unitSelection.unitSelected.Contains(landsknetch.rootGameObject) && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, landsknetch.groundLayerMask))
            {
                landsknetch.landsknetchAgent.SetDestination(hit.point);
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
        Debug.Log("");
    }

    public override void OnTriggerExit(LandsknetchStateController landsknetch, Collider coll)
    {
        Debug.Log("");
    }
}
