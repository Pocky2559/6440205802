using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class LandsknetchStateController : MonoBehaviour // Attach this to the DetectionArea Game Object of Landsknetch
{
    public NavMeshAgent landsknetchAgent;
    public GameObject rootGameObject;
    public GameObject unitSelectionAssign;
    public UnitSelection unitSelection;
    LandsknetchBaseState currentState;
    public LayerMask groundLayerMask;
    public Vector3 selectedPosition;
    public UnitStat unitStat;
    public GameObject targetEnemy;
    public LayerMask targetLayerMask;
    public HouseList population;
    public Animator landskAnimatorControlller;
    public UnitStat enemyStat;
    public CapsuleCollider landskCollider;
    public GameObject neutralSword;
    public GameObject attackedSword;
    public SoundEffectController soundEffectController;

    public Landsk_Idel land_IdelState = new();
    public Landsk_Moving land_MovingState = new();
    public Landsk_Chasing land_ChasingState = new();

    private void Awake()
    {  
        //create game object that contains component
        unitSelectionAssign = GameObject.FindGameObjectWithTag("UnitSelection");

        //Assign component
        unitSelection = unitSelectionAssign.GetComponent<UnitSelection>();
        unitStat = GetComponentInParent<UnitStat>();
        landsknetchAgent = GetComponentInParent<NavMeshAgent>(); 
        rootGameObject = transform.root.gameObject;
        population = GameObject.FindGameObjectWithTag("PopulationController").GetComponent<HouseList>();
        landskAnimatorControlller = GetComponentInParent<Animator>();
        landskCollider = GetComponentInParent<CapsuleCollider>();
    }

    private void Start()
    {
        currentState = land_IdelState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdaterState(this); 
    }

    private void OnTriggerStay(Collider otherEnter)
    {
        currentState.OnTriggerStay(this, otherEnter);
    }

    private void OnTriggerExit(Collider otherExit)
    {
        currentState.OnTriggerStay(this, otherExit);
    }

    public void SwitchState(LandsknetchBaseState stateName)
    {
        currentState = stateName;
        currentState.EnterState(this);
    }
}
