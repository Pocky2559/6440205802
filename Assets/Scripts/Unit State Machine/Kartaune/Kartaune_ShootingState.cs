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
        Debug.Log(kartaune.transform.parent.rotation.y);

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

        if (kartaune.transform.parent.rotation.y < 0.92) //Kartaune Should then rotate more than this value
        {
            kartaune.targetEnemy = null;
            kartaune.SwitchState(kartaune.kartaune_IdelState);
        }
        #region Switch to ExitState
        if (kartaune.unitStat.unitHP <= 0)
        {
            ExitState(kartaune);
        }
        #endregion
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
    public override void ExitState(KartauneStateController kartaune)
    {
        kartaune.buildCannonOnWall.positionToPlace.SetActive(true); //show the point that can build a cannon
        kartaune.buildCannonOnWall.icon.SetActive(true); //show icon of building cannon on wall
        kartaune.population.PopulationChanges(-1 * kartaune.unitStat.unitPopulation); //Decrease population
        MonoBehaviour.Destroy(kartaune.transform.parent.gameObject); // Delete Villager from the game
    }
}
