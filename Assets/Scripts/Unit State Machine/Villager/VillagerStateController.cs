using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.AI;

public class VillagerStateController : MonoBehaviour
{
    public NavMeshAgent Villager;
    public GameObject resourcesStatusAssign;
    public GameObject unitSelectionAssign;
    public GameObject targetResources;
    public GameObject selectedStoringPoint;
    public LayerMask resorcesLayerMask;
    public LayerMask groundLayerMask;
    public int gatheringAmount;
    public int woodCarryingCapacity;
    public int goldStoneCarryingCapacity;
    public int foodCarryingCapacity;
    public string currentCarryingResource;
    public bool isStoringManual; 
    public Vector3 selectedPosition;
    public ResourcesStatus resourcesStatus; 
    public UnitStat unitStat;
    public UnitSelection unitSelection;
    public VillagerGatheringCapacity villagerGatheringCapacity;
    
    
    //Upgrade 
    public EconomicUpgradeDatabase economicUpgradeDatabase;
    public bool isLumberCampGatheringSpeedUpgrade;
    public bool isLumberCampGatheringCapacityUpgrade;
    public bool isMiningCartGatheringSpeedUpgrade;
    public bool isMiningCartGatheringCapacityUpgrade;
    public bool isWindMillGatheringSpeedUpgrade;
    public bool isWindMillGatheringCapacityUpgrade;

    //States
    private VillagerBaseState currentState;
    public Villager_IdelState vil_IdelState = new();
    public Villager_MovingState vil_MovingState = new();
    public Villager_GatheringState vil_GatheringState = new();
    public Villager_StoringState vil_StoringState = new();

    private void Awake()
    {
        //create game object that contain component
        unitSelectionAssign = GameObject.FindGameObjectWithTag("UnitSelection");
        resourcesStatusAssign = GameObject.FindGameObjectWithTag("ResourcesStatus");

        //Assign compoent
        resourcesStatus = resourcesStatusAssign.GetComponent<ResourcesStatus>();
        unitSelection = unitSelectionAssign.GetComponent<UnitSelection>();
        Villager = GetComponent<NavMeshAgent>();
        unitStat = GetComponent<UnitStat>();

        //Assign Rsources Gathering Capacity
        woodCarryingCapacity = villagerGatheringCapacity.villagerCapacity[0].woodCapacity;
        goldStoneCarryingCapacity = villagerGatheringCapacity.villagerCapacity[0].goldStoneCapacity;
        foodCarryingCapacity = villagerGatheringCapacity.villagerCapacity[0].foodCapacity;
    }

    private void Start()
    {
        currentState = vil_IdelState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
        Debug.Log("Current State i = " + currentState);
        Debug.Log("isStoringManual = " + isStoringManual);
    }

    public void SwitchState(VillagerBaseState statName)
    {
        currentState = statName;
        currentState.EnterState(this);
    }
}
