using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Kartaune_IdelState : KartauneBaseState
{
    public override void EnterState(KartauneStateController kartaune)
    {
        //Kartaune Enter Idel State
        kartaune.transform.parent.rotation = kartaune.originRotation;
    }
    public override void UpdaterState(KartauneStateController kartaune)
    {
        #region Switch to ExitState
        if (kartaune.unitStat.unitHP <= 0)
        {
            ExitState(kartaune);
        }
        #endregion
    }
    public override void OnTriggerStay(KartauneStateController kartaune, Collider coll)
    {
        if(coll.CompareTag("OttomanRecruit")
           || coll.CompareTag("OttomanGunnerRecruit")
           || coll.CompareTag("MeleeJanissary")
           || coll.CompareTag("RangedJanissary")
           || coll.CompareTag("OttomanCannon"))
        {
            kartaune.targetEnemy = coll.gameObject;
            kartaune.SwitchState(kartaune.kartaune_ShootingState);
        }
    }
    public override void OnTriggerExit(KartauneStateController kartaune, Collider coll)
    {
        // Debug
    }
    public override void ExitState(KartauneStateController kartaune)
    {
        kartaune.buildCannonOnWall.positionToPlace.SetActive(true); //show the point that can build a cannon
        kartaune.buildCannonOnWall.icon.SetActive(true); //show icon of building cannon on wall
        kartaune.population.PopulationChanges(-1 * kartaune.unitStat.unitPopulation); //Decrease population
        MonoBehaviour.Destroy(kartaune.transform.parent.gameObject); // Delete Villager from the game
    }
}
