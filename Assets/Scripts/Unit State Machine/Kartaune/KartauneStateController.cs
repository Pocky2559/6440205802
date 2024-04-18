using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartauneStateController : MonoBehaviour
{
    public GameObject targetEnemy;
    public GameObject cannonball;
    public UnitDatabaseSO unitDatabase;
    public CannonBallFunction cannonballFunc;

    KartauneBaseState currentState;
    public Kartaune_IdelState kartaune_IdelState = new();
    public Kartaune_ShootingState kartaune_ShootingState = new();

    private void Start()
    {
        currentState = kartaune_IdelState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdaterState(this);
    }

    public void SwitchState(KartauneBaseState stateName)
    {
        currentState = stateName;
        currentState.EnterState(this);
    }

    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this, other);
    }
}
