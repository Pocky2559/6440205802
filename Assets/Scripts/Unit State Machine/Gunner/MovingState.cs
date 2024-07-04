using UnityEngine;

public class MovingState : GunnerBaseState
{
    bool isDead;
    public override void EnterState(GunnerStateController gunner)
    {
        gunner.Gunner.isStopped = false;

        //Play animation Gunner_Walking
        gunner.gunnerAnimatorControlller.SetBool("isWalking", true);
        gunner.gunnerAnimatorControlller.SetBool("isFollowing", false);
        gunner.Gun.transform.localPosition = new Vector3(0.254000008f, 1.18599999f, 0.324000001f);
        gunner.Gun.transform.localRotation = Quaternion.Euler(357.268738f, 122.092773f, 359.583221f);
        gunner.rigBuilder.enabled = true;
        //gunner.gunnerAnimatorControlller.SetBool("isMoveWhileReload", false);

        //Play Walking Sound
        gunner.soundEffectController.PlayWalkingSound();

            //Normal Holding Gun
            //gunner.Gun.transform.localPosition = new Vector3(0.254000008f, 1.18599999f, 0.324000001f);
            //gunner.Gun.transform.localRotation = Quaternion.Euler(357.268738f, 122.092773f, 359.583221f);
    }
    public override void UpdateState(GunnerStateController gunner)
    {
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
                    gunner.selectedEnemyStat = hit.collider.GetComponentInParent<UnitStat>();
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
                //Stop Playing Sound
                gunner.SwitchState(gunner.idelState); // Switch to moving state
            }
        }
        #endregion

        #region Switch to ExitState
        if (gunner.unitStat.unitHP <= 0 && isDead == false)
        {
            isDead = true;
            gunner.soundEffectController.PlayUnitDiedSound();// Play UnitDie Sound
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
        //Play animation Gunner_Death
        gunner.Gunner.enabled = false;
        gunner.gunnerAnimatorControlller.SetBool("isDead", true);
        gunner.Gun.SetActive(false);
        gunner.rigBuilder.enabled = false;
        gunner.gunnerCollider.enabled = false;
        //
        gunner.population.PopulationChanges(-1 * gunner.unitStat.unitPopulation); //Decrease population
        MonoBehaviour.Destroy(gunner.transform.parent.gameObject, 4); // Delete Villager from the game
    }
}
