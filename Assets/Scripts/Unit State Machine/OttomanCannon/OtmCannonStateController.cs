using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OtmCannonStateController : MonoBehaviour
{
    public NavMeshAgent OtmCannon;
    public GameObject gate;
    public SphereCollider attackRange;
    public UnitDatabaseSO unitDatabase;
    public CannonBallEnemyFunc cannonballFunc;
    public GameObject cannonball;
    public CaptureGroundPoint captureGroundPoint;
    public GameObject targetPlayerUnit;
    public bool isCannonReachDestination;
    public HouseList population;
    public UnitStat unitStat;
    public GameObject availableWaypoint;
    public FirearmsParticle firearmsParticle;
    public Transform firePoint;

    private OtmCannonBaseState currentState;
    public OtmCannon_AttackGateState otmCannon_AttackGateState = new();
    public OtmCannon_CaptureGroundState otmCannon_CaptureGroundState = new();
    public OtmCannon_AttackPlayerState otmCannon_AttackPlayerState = new();

    private void Awake()
    {
        //assign 
        population = GameObject.FindGameObjectWithTag("PopulationController").GetComponent<HouseList>();
        gate = GameObject.FindGameObjectWithTag("PalisadeGate");
        attackRange = GetComponent<SphereCollider>();
        OtmCannon = GetComponentInParent<NavMeshAgent>();
        captureGroundPoint = GameObject.FindGameObjectWithTag("CaptureGroundPoint").GetComponent<CaptureGroundPoint>();
        unitStat = GetComponentInParent<UnitStat>();    
        isCannonReachDestination = false;
        availableWaypoint = null;
        firearmsParticle = GameObject.FindGameObjectWithTag("ParticleController").GetComponent<FirearmsParticle>(); 
    }
    private void Start()
    {
        currentState = otmCannon_AttackGateState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(this,other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this,other);
    }

    public void SwitchState(OtmCannonBaseState stateName)
    {
        currentState = stateName;
        currentState.EnterState(this);
    }
}
