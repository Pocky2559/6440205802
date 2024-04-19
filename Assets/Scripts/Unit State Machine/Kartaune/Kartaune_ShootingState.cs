using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Kartaune_ShootingState : KartauneBaseState
{
    float lastShotTime = 0;
    public override void EnterState(KartauneStateController kartaune)
    {
        // Kartaune Enter Shooting State
    }
    public override void UpdaterState(KartauneStateController kartaune)
    {
       if(kartaune.targetEnemy != null)
       {
          Vector3 positionToAim = kartaune.targetEnemy.transform.position;
          positionToAim.y = kartaune.transform.parent.position.y; //freeze y position
          kartaune.transform.parent.LookAt(positionToAim);
          Shooting(kartaune);
       }

       if (kartaune.targetEnemy == null || kartaune.targetEnemy.activeSelf == false)
       {
          kartaune.targetEnemy = null;
          kartaune.SwitchState(kartaune.kartaune_IdelState);
       }
    }

    private void Shooting(KartauneStateController kartaune)
    {
        if (Time.time > lastShotTime + kartaune.unitDatabase.unitDetails[4].attackSpeed)
        {
            GameObject cannonball = MonoBehaviour.Instantiate(kartaune.cannonball, kartaune.transform.parent.position, Quaternion.identity);
            kartaune.cannonballFunc = cannonball.GetComponent<CannonBallFunction>();
            lastShotTime = Time.time;
            kartaune.cannonballFunc.AssignValueOfCannonball(cannonball, kartaune.targetEnemy.transform.position); // CannonBallFunction Script handle the moving of cannonball, damage calculation and more
        }
    }

    public override void OnTriggerStay(KartauneStateController kartaune, Collider coll)
    {
        // Debug
    }
    public override void OnTriggerExit(KartauneStateController kartaune, Collider coll)
    {
        #region Switch to Idle State
        if (coll.gameObject == kartaune.targetEnemy)
        {
            kartaune.targetEnemy = null;
            kartaune.SwitchState(kartaune.kartaune_IdelState);
        }
        #endregion
    }
}
