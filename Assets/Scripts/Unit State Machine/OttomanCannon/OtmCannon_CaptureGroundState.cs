using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmCannon_CaptureGroundState : OtmCannonBaseState
{
    //private GameObject avaliableWaypoint = null;
    private bool conditionMet;
    public override void EnterState(OtmCannonStateController otmCannon)
    {
        if(otmCannon.isCannonReachDestination == false)
        {
            otmCannon.OtmCannon.isStopped = false;

            //Play Animation//
            otmCannon.leftWheelAnim.SetBool("isMoving", true);
            otmCannon.rightWheelAnim.SetBool("isMoving", true);
            //////////////////
            
            FindAvailablePoint(otmCannon);
        }
    }

    public override void UpdateState(OtmCannonStateController otmCannon)
    {
        if(otmCannon.availableWaypoint != null && otmCannon.isCannonReachDestination == false)
        {
            float distanceBetweenCannonAndPoint = Vector3.Distance(otmCannon.transform.parent.position, otmCannon.availableWaypoint.transform.position);
            
            if(distanceBetweenCannonAndPoint <= 1)
            {
                otmCannon.isCannonReachDestination = true;

                //Stop Play Animation//
                otmCannon.leftWheelAnim.SetBool("isMoving", false);
                otmCannon.rightWheelAnim.SetBool("isMoving", false);
                //////////////////
            }
        }

        #region Switch to ExitState
        if (otmCannon.unitStat.unitHP <= 0)
        {
            ExitState(otmCannon);
        }
        #endregion
    }

    public override void OnTriggerStay(OtmCannonStateController otmCannon, Collider coll)
    {
        #region Switch to AttackPLayer State
        if (otmCannon.availableWaypoint != null)
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
                    otmCannon.captureGroundPoint.WaypointStatus(otmCannon.availableWaypoint, true); //make that waypoint available
                }

                else //if it reached the destination
                {
                    otmCannon.captureGroundPoint.WaypointStatus(otmCannon.availableWaypoint, false); //make that waypoint unavailable
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
                otmCannon.availableWaypoint = waypoint.Key;
                conditionMet = true;
                break;
            }
        }

        if (conditionMet == true)
        {
            /// Finish Finding the waypoint
            if (otmCannon.captureGroundPoint.waypoints[otmCannon.availableWaypoint] == true)
            {
                otmCannon.captureGroundPoint.WaypointStatus(otmCannon.availableWaypoint, false);
                otmCannon.OtmCannon.SetDestination(otmCannon.availableWaypoint.transform.position);
            }
        }
        else
        {
            otmCannon.SwitchState(otmCannon.otmCannon_CaptureGroundState); // Finding new available waypoint
        }
    }
    public override void ExitState(OtmCannonStateController otmCannon)
    {
        otmCannon.captureGroundPoint.WaypointStatus(otmCannon.availableWaypoint, true); //make waypoint available
        MonoBehaviour.Destroy(otmCannon.transform.root.gameObject); // Delete Villager from the game
    }
}
