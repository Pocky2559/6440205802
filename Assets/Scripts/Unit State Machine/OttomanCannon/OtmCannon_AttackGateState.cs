using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmCannon_AttackGateState : OtmCannonBaseState
{
    private float lastShotTime;
    private float distanceCannonAndGate;
    public override void EnterState(OtmCannonStateController otmCannon)
    {
        if(otmCannon.gate != null) // if there has a gate
        {
            otmCannon.OtmCannon.SetDestination(otmCannon.gate.transform.position);
            otmCannon.transform.parent.LookAt(otmCannon.gate.transform.position);
        }

        else
        {
            otmCannon.SwitchState(otmCannon.otmCannon_CaptureGroundState);
        }
    }

    public override void UpdateState(OtmCannonStateController otmCannon)
    {
        if (otmCannon.gate != null)
        {
            distanceCannonAndGate = Vector3.Distance(otmCannon.transform.position, otmCannon.gate.transform.position);
            otmCannon.OtmCannon.SetDestination(otmCannon.gate.transform.position);
            if (distanceCannonAndGate <= otmCannon.attackRange.radius)
            {
                otmCannon.OtmCannon.isStopped = true; // Stop moving
                otmCannon.OtmCannon.enabled = false; // disable NavMeshAgent to make it can walk through
                Attack(otmCannon);
            }
        }
    }

    public override void OnTriggerStay(OtmCannonStateController otmCannon, Collider coll)
    {
       // Triggered Stay
    }

    public override void OnTriggerExit(OtmCannonStateController otmCannon, Collider coll)
    {
       // Triggered Exit
    }

    private void Attack(OtmCannonStateController otmCannon)
    {
        if (Time.time > lastShotTime + otmCannon.unitDatabase.unitDetails[4].attackSpeed)
        {
            GameObject cannonball = MonoBehaviour.Instantiate(otmCannon.cannonball, otmCannon.transform.parent.position, Quaternion.identity);
            otmCannon.cannonballFunc = cannonball.GetComponent<CannonBallFunction>();
            lastShotTime = Time.time;
            otmCannon.cannonballFunc.AssignValueOfCannonball(cannonball, otmCannon.gate.transform.position); // CannonBallFunction Script handle the moving of cannonball, damage calculation and more
        }
    }
}