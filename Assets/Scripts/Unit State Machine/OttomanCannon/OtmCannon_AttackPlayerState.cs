using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtmCannon_AttackPlayerState : OtmCannonBaseState
{
    private float lastShotTime = 0f;
    private Vector3 positionToAim;
    public override void EnterState(OtmCannonStateController otmCannon)
    {
        //positionToAim = otmCannon.targetPlayerUnit.transform.position;
        //positionToAim.y = otmCannon.transform.parent.position.y; //freeze y position
        otmCannon.OtmCannon.isStopped = true;

        //Stop Play Animation//
        otmCannon.leftWheelAnim.SetBool("isMoving", false);
        otmCannon.rightWheelAnim.SetBool("isMoving", false);
        //////////////////
    }
    public override void UpdateState(OtmCannonStateController otmCannon)
    {
        if(otmCannon.targetPlayerUnit != null)
        {
            positionToAim = otmCannon.targetPlayerUnit.transform.position;
            positionToAim.y = otmCannon.transform.parent.position.y; //freeze y position
            otmCannon.transform.parent.LookAt(positionToAim);
            Attack(otmCannon);
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
        //OnTrigger Stay
    }
    public override void OnTriggerExit(OtmCannonStateController otmCannon, Collider coll)
    {
        #region Switch to CaptureGround State
        if (coll.gameObject == otmCannon.targetPlayerUnit)
        {
            otmCannon.SwitchState(otmCannon.otmCannon_CaptureGroundState);
        }
        #endregion
    }
    private void Attack(OtmCannonStateController otmCannon)
    {
        if (Time.time > lastShotTime + otmCannon.unitDatabase.unitDetails[9].attackSpeed)
        {
            GameObject cannonball = MonoBehaviour.Instantiate(otmCannon.cannonball, otmCannon.transform.parent.position, Quaternion.identity);
            otmCannon.cannonballFunc = cannonball.GetComponent<CannonBallEnemyFunc>();
            lastShotTime = Time.time;
            otmCannon.cannonballFunc.AssignValueOfCannonball(cannonball, otmCannon.targetPlayerUnit.transform.position); // CannonBallFunction Script handle the moving of cannonball, damage calculation and more
            otmCannon.firearmsParticle.StartPlayParticle(otmCannon.firePoint.position); //play particle
            otmCannon.soundEffectController.PlayCannonFiringSound(); //Play CannonFire Sound
        }
    }
    public override void ExitState(OtmCannonStateController otmCannon)
    {
        if(otmCannon.isCannonReachDestination == true)
        {
            otmCannon.captureGroundPoint.WaypointStatus(otmCannon.availableWaypoint, true); //make waypoint available
        }
        otmCannon.cannonDestroyedParticle.StartPlayParticle(otmCannon.transform.position);
        SoundEffectController destroyedSound = MonoBehaviour.Instantiate(otmCannon.soundEffectController);
        destroyedSound.PlayDeleteBuildingSound();
        MonoBehaviour.Destroy(destroyedSound.gameObject, 3f);
        MonoBehaviour.Destroy(otmCannon.transform.root.gameObject); // Delete Villager from the game
    }
}
