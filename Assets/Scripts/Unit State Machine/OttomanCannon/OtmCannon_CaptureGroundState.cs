using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmCannon_CaptureGroundState : OtmCannonBaseState
{
    private GameObject avaliableWaypoint = null;
    private bool conditionMet;
    public override void EnterState(OtmCannonStateController otmCannon)
    {
        if(otmCannon.isCannonReachDestination == false)
        {
            otmCannon.OtmCannon.isStopped = false;
            FindAvailablePoint(otmCannon);
        }
    }

    public override void UpdateState(OtmCannonStateController otmCannon)
    {
        if(avaliableWaypoint!= null && otmCannon.isCannonReachDestination == false)
        {
            float distanceBetweenCannonAndPoint = Vector3.Distance(otmCannon.transform.parent.position, avaliableWaypoint.transform.position);
            
            if(distanceBetweenCannonAndPoint <= 1)
            {
                otmCannon.isCannonReachDestination = true;
            }
        }
    }

    public override void OnTriggerStay(OtmCannonStateController otmCannon, Collider coll)
    {
        #region Switch to AttackPLayer State
        if (avaliableWaypoint != null)
        {
            if (coll.CompareTag("Villager")
                       || coll.CompareTag("Landsknecht")
                       || coll.CompareTag("Gunner")
                       || coll.CompareTag("Captain")
                       || coll.CompareTag("Kartouwe"))
            {
                otmCannon.targetPlayerUnit = coll.gameObject;

                if(otmCannon.isCannonReachDestination == false) // if cannon doesn't reach destination
                {
                    otmCannon.captureGroundPoint.WaypointStatus(avaliableWaypoint, true); //make that waypoint available
                }

                else //if it reached the destination
                {
                    otmCannon.captureGroundPoint.WaypointStatus(avaliableWaypoint, false); //make that waypoint unavailable
                }
                otmCannon.SwitchState(otmCannon.otmCannon_AttackPlayerState);
            }
        }
        #endregion
    }

    public override void OnTriggerExit(OtmCannonStateController otmCannon, Collider coll)
    {
        // Triggered Ex
    }

    private void FindAvailablePoint(OtmCannonStateController otmCannon)
    {
        foreach (KeyValuePair<GameObject, bool> waypoint in otmCannon.captureGroundPoint.waypoints)
        {
            if (waypoint.Value == true)
            {
                avaliableWaypoint = waypoint.Key;
                conditionMet = true;
                break;
            }
        }

        if (conditionMet == true)
        {
            /// Finish Finding the waypoint
            if (otmCannon.captureGroundPoint.waypoints[avaliableWaypoint] == true)
            {
                otmCannon.captureGroundPoint.WaypointStatus(avaliableWaypoint, false);
                otmCannon.OtmCannon.SetDestination(avaliableWaypoint.transform.position);
            }
        }
        //else
        //{
        //    otmCannon.SwitchState(otmCannon.otmCannon_CaptureGroundState); // Finding new available waypoint
        //}
    }
}
