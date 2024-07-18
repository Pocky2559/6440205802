using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmCannon_AttackGateState : OtmCannonBaseState
{
    private float lastShotTime;
    private float distanceCannonAndGate;
    private bool isMoving;
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
                
                //Stop Play Animation//
                otmCannon.leftWheelAnim.SetBool("isMoving", false);
                otmCannon.rightWheelAnim.SetBool("isMoving", false);
                //////////////////
                
                otmCannon.transform.parent.LookAt(otmCannon.gate.transform.position);
                Attack(otmCannon);
            }

            if (distanceCannonAndGate > otmCannon.attackRange.radius)
            {
                otmCannon.OtmCannon.isStopped = false;

                //Play Animation//
                otmCannon.leftWheelAnim.SetBool("isMoving", true);
                otmCannon.rightWheelAnim.SetBool("isMoving", true);
                //////////////////
                    
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
        if (Time.time > lastShotTime + otmCannon.unitDatabase.unitDetails[9].attackSpeed)
        {
            GameObject cannonball = MonoBehaviour.Instantiate(otmCannon.cannonball, otmCannon.transform.parent.position, Quaternion.identity);
            otmCannon.cannonballFunc = cannonball.GetComponent<CannonBallEnemyFunc>();
            lastShotTime = Time.time;
            otmCannon.cannonballFunc.AssignValueOfCannonball(cannonball, otmCannon.gate.transform.position); // CannonBallFunction Script handle the moving of cannonball, damage calculation and more
            otmCannon.firearmsParticle.StartPlayParticle(otmCannon.firePoint.position); //Play Particle
            otmCannon.soundEffectController.PlayCannonFiringSound(); //Play CannonFire Sound
        }
    }
    public override void ExitState(OtmCannonStateController otmCannon)
    {
        otmCannon.cannonDestroyedParticle.StartPlayParticle(otmCannon.transform.position);
        SoundEffectController destroyedSound = MonoBehaviour.Instantiate(otmCannon.soundEffectController);
        destroyedSound.PlayDeleteBuildingSound();
        MonoBehaviour.Destroy(destroyedSound.gameObject,3f);
        MonoBehaviour.Destroy(otmCannon.transform.root.gameObject); // Delete Villager from the game
    }
}