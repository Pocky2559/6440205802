using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OttomanGunnerRecruitStateController : MonoBehaviour
{
    public NavMeshAgent otmGunnerAgent;
    public GameObject Wall;
    public UnitStat unitStat;
    public UnitStat targetPlayerUnitStat;
    public LayerMask wallLayerMask;
    public GameObject targetPlayerUnit;
    public GameObject rootGameObject;
    public LayerMask targetLayerMask;
    public CapturePointByEnemy capturePointByEnemy;
    public HouseList population;

    OttomanGunnerRecruitBaseState currentState;
    public OtmGunner_AttackWallState otmGunner_AttackWallState = new();
    public OtmGunner_CapturePointState otmGunner_CapturePointState = new();
    public OtmGunner_AttackPlayerUnitState otmGunner_AttackPlayerUnitState = new();


    private void Awake()
    {
        otmGunnerAgent= GetComponentInParent<NavMeshAgent>();
        Wall = GameObject.FindGameObjectWithTag("PalisadeGate");
        unitStat = GetComponentInParent<UnitStat>();
        rootGameObject = transform.root.gameObject;
        capturePointByEnemy = GameObject.FindGameObjectWithTag("CapturedPoint").GetComponent<CapturePointByEnemy>();
        population = GameObject.FindGameObjectWithTag("PopulationController").GetComponent<HouseList>();
    }

    private void Start()
    {
        currentState = otmGunner_AttackWallState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void OnTriggerStay(Collider otherStay)
    {
        currentState.OnTriggerStay(this, otherStay);
    }

    private void OnTriggerExit(Collider otherExit)
    {
        currentState.OnTriggerExit(this, otherExit);
    }

    public void SwitchState(OttomanGunnerRecruitBaseState stateName)
    {
        currentState = stateName;
        currentState.EnterState(this);
    }
}
