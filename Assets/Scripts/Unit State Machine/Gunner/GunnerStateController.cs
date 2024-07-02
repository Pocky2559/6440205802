using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class GunnerStateController : MonoBehaviour //attach this script to the DetectionArea game object
{
    public NavMeshAgent Gunner;
    public GameObject Gun;
    public LayerMask targetLayerMask;
    public UnitStat unitStat;
    public GameObject selectedEnemy;
    public bool isChasing;
    public UnitSelection unitSelection;
    public GameObject unitSelectionAssign;
    public LayerMask groundLayerMask;
    public Vector3 selectedPosition;
    public GameObject rootGameObject;
    public SphereCollider detectionArea;
    public HouseList population;
    public Animator gunnerAnimatorControlller;
    public RigBuilder rigBuilder;
    public UnitStat selectedEnemyStat;
    public SphereCollider attackRange;
    public bool isSelectEnemyManually;
    public CapsuleCollider gunnerCollider;
    public FirearmsParticle firearmsParticle;
    public GameObject firePoint;
    public SoundEffectController soundEffectController;

    GunnerBaseState currentState;
    public IdelState idelState = new();
    public ShootingState shootingState = new();
    public MovingState movingState = new();
    public ChasingState chasingState = new();

    private void Awake()
    {
        //create game object that contains component
        unitSelectionAssign = GameObject.FindGameObjectWithTag("UnitSelection");

        //Assign component
        unitStat = GetComponentInParent<UnitStat>();
        unitSelection = unitSelectionAssign.GetComponent<UnitSelection>();
        detectionArea = GetComponent<SphereCollider>();
        population = GameObject.FindGameObjectWithTag("PopulationController").GetComponent<HouseList>();
        gunnerAnimatorControlller = GetComponentInParent<Animator>();
        rigBuilder = GetComponentInParent<RigBuilder>();
        attackRange = GetComponent<SphereCollider>();
        gunnerCollider = GetComponentInParent<CapsuleCollider>();
        firearmsParticle = GameObject.FindGameObjectWithTag("ParticleController").GetComponent<FirearmsParticle>();
       
        //Find the root game object (because this script attach the child)
        rootGameObject = transform.root.gameObject;
    }

    private void Start()
    {
        currentState = idelState; // the start state machine is idelState
        Gunner = GetComponentInParent<NavMeshAgent>();
        currentState.EnterState(this); // "this" means GunnerStateController
    }

    private void Update()
    {
        currentState.UpdateState(this); // send all the data to UpdateState
    }

    public void OnTriggerStay(Collider otherEnter) // Detection the Enemy with collider
    {
            currentState.OnTriggerStay(this, otherEnter);
    }

    private void OnTriggerExit(Collider otherExit)
    {
            currentState.OnTriggerExit(this, otherExit);
    }

    public void SwitchState(GunnerBaseState stateName)
    {
        currentState = stateName;
        currentState.EnterState(this);
    }
}
