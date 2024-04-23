using UnityEngine;

public class MovingState : GunnerBaseState
{
    public override void EnterState(GunnerStateController gunner)
    {
        gunner.Gunner.isStopped = false;
    }
    public override void UpdateState(GunnerStateController gunner)
    {
        Debug.Log("Gunner is moving");
        
        #region Moving Logic & Chasing State
        if (gunner.unitSelection.unitSelected.Contains(gunner.rootGameObject) && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

        // Moving Logic Control By outside script name "UnitFormation"

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("OttomanRecruit")
                      || hit.collider.CompareTag("OttomanGunnerRecruit")
                      || hit.collider.CompareTag("MeleeJanissary")
                      || hit.collider.CompareTag("RangedJanissary")
                      || hit.collider.CompareTag("OttomanCannon")) // if right click on the enemy
                {
                    gunner.selectedEnemy = hit.collider.gameObject;
                    Debug.Log("Switching from Moving state to Chasing state");
                    gunner.SwitchState(gunner.chasingState);
                }
                else // if right click on something else
                {
                    gunner.selectedEnemy = null; // no switching and set seleced enemy as null
                }
            }
        }
        #endregion

        #region Switch to Idel State
        if (gunner.Gunner.pathPending == false) //if Gunner is moving then it will switch to MovingState
        {
            if (Mathf.RoundToInt(gunner.Gunner.remainingDistance) == 0)
            {
                Debug.Log("Switching from Moving state to Idel state");
                Debug.Log("Remaining path gunner = " + Mathf.RoundToInt(gunner.Gunner.remainingDistance));
                gunner.SwitchState(gunner.idelState); // Switch to moving state
            }
        }
        #endregion

        #region Switch to ExitState
        if (gunner.unitStat.unitHP <= 0)
        {
            ExitState(gunner);
        }
        #endregion
    }
    public override void OnTriggerStay(GunnerStateController gunner, Collider coll)
    {
        return;
    }
    public override void OnTriggerExit(GunnerStateController gunner, Collider coll)
    {
        return;
    }
    public override void ExitState(GunnerStateController gunner)
    {
        gunner.population.PopulationChanges(-1 * gunner.unitStat.unitPopulation); //Decrease population
        MonoBehaviour.Destroy(gunner.transform.parent.gameObject); // Delete Villager from the game
    }
}
