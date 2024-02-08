using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class OttomanRecruitStateController : MonoBehaviour
{
    public GameObject Wall;
    public NavMeshAgent otmRecruitAgent;
    public UnitStat unitStat;
    public LayerMask wallLayerMask;
    public GameObject targetPlayerUnit;
    public LayerMask targetLayerMask;
    public GameObject rootGameObject;

    OttomanRecruitBaseState currentState;
    public OtmRecruit_AttackWallState otmRecruit_AttackWallState = new();
    public OtmRecruit_CapturePointState otmRecruit_CapturePointState = new();
    public OtmRecruit_AttackPlayerUnit otmRecruit_AttackPlayerUnitState = new();

    private void Awake()
    {
        Wall = GameObject.FindGameObjectWithTag("PalisadeGate");

        //Component
        otmRecruitAgent = GetComponentInParent<NavMeshAgent>();
        unitStat = GetComponentInParent<UnitStat>();

        //Find the root game object (because this script attach the child)
        rootGameObject = transform.root.gameObject;
    }

    private void Start()
    {
        currentState = otmRecruit_AttackWallState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdaterState(this);
    }

    public void OnTriggerStay(Collider otherStay)
    {
        currentState.OnTriggerStay(this, otherStay);
    }

    public void OnTriggerExit(Collider otherExit)
    {
        currentState.OnTriggerExit(this, otherExit);
    }

    public void SwitchState(OttomanRecruitBaseState stateName)
    {
        currentState = stateName;
        currentState.EnterState(this);
    }
}
