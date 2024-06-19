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

        #region Switch to CaptureGround State
        else
        {
            otmCannon.SwitchState(otmCannon.otmCannon_CaptureGroundState);
        }
        #endregion
    }

    public override void UpdateState(OtmCannonStateController otmCannon)
    {
        if (otmCannon.gate != null)
        {
            distanceCannonAndGate = Vector3.Distance(otmCannon.transform.position, otmCannon.gate.transform.position);
            if (distanceCannonAndGate <= otmCannon.attackRange.radius)
            {
                otmCannon.OtmCannon.isStopped = true; // Stop moving
                otmCannon.transform.parent.LookAt(otmCannon.gate.transform.position);
                Attack(otmCannon);
            }

            if (distanceCannonAndGate > otmCannon.attackRange.radius)
            {
                otmCannon.OtmCannon.isStopped = false;
                otmCannon.OtmCannon.SetDestination(otmCannon.gate.transform.position);
            }
        }

        #region Switch to CaptureGround State
        else
        {
            otmCannon.SwitchState(otmCannon.otmCannon_CaptureGroundState);
        }
        #endregion

        #region Switch to ExitState
        if (otmCannon.unitStat.unitHP <= 0)
        {
            ExitState(otmCannon);
        }
        #endregion
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
            otmCannon.cannonballFunc = cannonball.GetComponent<CannonBallEnemyFunc>();
            lastShotTime = Time.time;
            otmCannon.cannonballFunc.AssignValueOfCannonball(cannonball, otmCannon.gate.transform.position); // CannonBallFunction Script handle the moving of cannonball, damage calculation and more
            otmCannon.firearmsParticle.StartPlayParticle(otmCannon.firePoint.position); //Play Particle
        }
    }
    public override void ExitState(OtmCannonStateController otmCannon)
    {
        MonoBehaviour.Destroy(otmCannon.transform.root.gameObject); // Delete Villager from the game
    }
}